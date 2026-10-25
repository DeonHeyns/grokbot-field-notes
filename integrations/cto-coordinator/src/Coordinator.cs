using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace CtoCoordinator;

public sealed class Coordinator(Settings settings, Store store, WorkspaceApi api)
{
    // v1 uses one replica with a persistent local SQLite volume. Serialize submissions
    // to prevent two local callers racing before the persisted request is visible.
    private readonly SemaphoreSlim dispatchGate = new(1, 1);
    public object Registry() => settings.Agents.Select(a => new { a.Key, a.Name, a.ChatUrl,
        configured = true, discovery = "configured registry; not ChatGPT directory discovery" }).ToArray();
    public async Task<object> Start(string agentKey, string cycleId, string requestId, string instructions, CancellationToken ct)
    {
        ValidateId(requestId); ValidateCycle(cycleId);
        if (string.IsNullOrWhiteSpace(instructions) || instructions.Length > 16000)
            throw new CoordinationException("Provide instructions between 1 and 16000 characters.");
        var agent = settings.Agents.SingleOrDefault(a => a.Key == agentKey)
            ?? throw new CoordinationException("Agent is not registered.");
        await dispatchGate.WaitAsync(ct);
        try {
            var run = store.GetRun(requestId);
            if (run != null && (run.AgentKey != agentKey || run.CycleId != cycleId || run.Instructions != instructions))
                throw new CoordinationException("request_id is already bound to a different assignment.");
            if (run?.DispatchStatus == "accepted") return View(run);
            if (run == null) {
                if (store.ListRuns(cycleId).Count >= 32) throw new CoordinationException("Cycle run limit reached (32). Review the cycle before requesting more work.");
                var input = instructions + "\n\nREPORT DELIVERY CONTRACT\n" +
                    "Use the connected submit_report MCP tool after completing this task, including blocked/partial outcomes. " +
                    "request_id=" + requestId + "; report_ticket=" + Ticket(requestId) + "; cycle_id=" + cycleId + ". " +
                    "Include source links, coverage and gaps in markdown. Provide as_of as an ISO 8601 timestamp with offset. " +
                    "Never put the report ticket in the report body or send it to another destination. " +
                    "If submit_report is unavailable, state that delivery is blocked in this conversation. " +
                    "Do not claim the Chief of Staff received your report without a successful tool response.";
                run = new Run(requestId, agentKey, agent.ChannelId, cycleId, instructions,
                    JsonSerializer.Serialize(new { input, conversation_key = $"cto-{agentKey}-{cycleId}" }), Now());
                store.SaveRun(run); // Persist exact payload BEFORE sending. Retries reuse it.
            }
            try {
                var result = await api.Trigger(run, ct);
                run = run with { DispatchStatus = "accepted", UpstreamRunId = result.RunId, ConversationUrl = result.Url };
                store.SaveRun(run);
            } catch (Exception e) when (e is HttpRequestException or TaskCanceledException or JsonException) {
                throw new CoordinationException("Dispatch outcome is unknown. Retry with the SAME request_id, agent, cycle and instructions; do not issue a new request.");
            }
            return View(run);
        } finally { dispatchGate.Release(); }
    }
    public async Task<object> Status(string requestId, CancellationToken ct)
    {
        ValidateId(requestId);
        await dispatchGate.WaitAsync(ct);
        try {
            var run = store.GetRun(requestId) ?? throw new CoordinationException("Unknown request_id.");
            if (run.UpstreamRunId != null) {
                try { run = run with { RunStatus = await api.Status(run, ct), StatusCheckedAt = Now() }; store.SaveRun(run); }
                catch (Exception e) when (e is HttpRequestException or TaskCanceledException or JsonException) {
                    throw new CoordinationException("Status temporarily unavailable; no new completion was verified.");
                }
            }
            return View(run);
        } finally { dispatchGate.Release(); }
    }
    public object Reports(string cycleId, int offset = 0)
    {
        ValidateCycle(cycleId);
        if (offset < 0 || offset > 32) throw new CoordinationException("Invalid offset.");
        var runs = store.ListRuns(cycleId);
        return new { items = runs.Skip(offset).Take(4).Select(r => new { run = View(r), report = store.GetReport(r.RequestId) }).ToArray(),
            nextOffset = offset + 4 < runs.Count ? (int?)(offset + 4) : null, total = runs.Count };
    }
    public object Submit(string requestId, string ticket, string outcome, string markdown, string asOf, string subject)
    {
        ValidateId(requestId);
        var expected = Encoding.UTF8.GetBytes(Ticket(requestId));
        if (!CryptographicOperations.FixedTimeEquals(expected, Encoding.UTF8.GetBytes(ticket)))
            throw new CoordinationException("Invalid report ticket.");
        var run = store.GetRun(requestId) ?? throw new CoordinationException("Unknown request_id.");
        if (DateTimeOffset.UtcNow - DateTimeOffset.Parse(run.CreatedAt) > TimeSpan.FromDays(7))
            throw new CoordinationException("Report ticket expired; request a new run.");
        if (outcome is not ("complete" or "partial" or "blocked")) throw new CoordinationException("Invalid outcome.");
        if (string.IsNullOrWhiteSpace(markdown) || markdown.Length > 60000 || markdown.Contains(ticket, StringComparison.Ordinal))
            throw new CoordinationException("Report must be 1..60000 characters and must not contain its ticket.");
        if (!Regex.IsMatch(asOf, "(Z|[+-][0-9]{2}:[0-9]{2})$") ||
            !DateTimeOffset.TryParse(asOf, out var time) || time > DateTimeOffset.UtcNow.AddMinutes(5))
            throw new CoordinationException("as_of must be an ISO 8601 timestamp with timezone, not in the future.");
        var saved = store.SaveReport(new Report(requestId, outcome, markdown, time.ToUniversalTime().ToString("O"), Now(), subject));
        return new { saved = true, requestId, receivedAt = saved.ReceivedAt, outcome = saved.Outcome };
    }
    private object View(Run r) => new { r.RequestId, r.AgentKey, r.CycleId, r.CreatedAt, r.DispatchStatus,
        r.UpstreamRunId, r.ConversationUrl, r.RunStatus, r.StatusCheckedAt,
        reportReceived = store.GetReport(r.RequestId) != null,
        pollingAvailable = r.UpstreamRunId != null };
    private string Ticket(string id) => Convert.ToHexString(HMACSHA256.HashData(Encoding.UTF8.GetBytes(settings.TicketKey), Encoding.UTF8.GetBytes(id)));
    private static string Now() => DateTimeOffset.UtcNow.ToString("O");
    private static void ValidateId(string id) { if (!Guid.TryParseExact(id, "D", out _) || id != id.ToLowerInvariant()) throw new CoordinationException("request_id must be a lowercase UUID."); }
    private static void ValidateCycle(string id) { if (!Regex.IsMatch(id, "^[a-zA-Z0-9][a-zA-Z0-9-]{0,79}$")) throw new CoordinationException("Invalid cycle_id (letters, digits and hyphens, max 80)."); }
}
