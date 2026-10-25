using System.Text.RegularExpressions;
namespace CtoCoordinator;

public sealed class Settings
{
    public string PublicUrl { get; set; } = "";
    public string Authority { get; set; } = "";
    public string Audience { get; set; } = "";
    public string SubjectClaim { get; set; } = "sub";
    public string[] AllowedSubjects { get; set; } = [];
    public string[] AllowedOrigins { get; set; } = [];
    public string ManageScope { get; set; } = "coordination.manage";
    public string ReportScope { get; set; } = "reports.submit";
    public string WorkspaceAccessToken { get; set; } = "";
    public string TicketKey { get; set; } = "";
    public string DatabasePath { get; set; } = "/data/coordinator.db";
    public List<Agent> Agents { get; set; } = [];
    public void Validate()
    {
        foreach (var url in new[] { PublicUrl, Authority })
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || uri.Scheme != "https" || !string.IsNullOrEmpty(uri.Query))
                throw new InvalidOperationException("PublicUrl and Authority must be canonical HTTPS URLs.");
        PublicUrl = PublicUrl.TrimEnd('/');
        if (string.IsNullOrWhiteSpace(Audience) || AllowedSubjects.Length == 0 ||
            string.IsNullOrWhiteSpace(WorkspaceAccessToken) || TicketKey.Length < 43)
            throw new InvalidOperationException("Configure audience, allowed subjects, Workspace token and a random ticket key of at least 43 characters.");
        if (Agents.Select(a => a.Key).Distinct().Count() != Agents.Count ||
            Agents.Select(a => a.ChannelId).Distinct().Count() != Agents.Count)
            throw new InvalidOperationException("Agent keys and API channels must be unique.");
        foreach (var a in Agents)
            if (!Regex.IsMatch(a.Key, "^[a-z][a-z0-9-]{0,39}$") || string.IsNullOrWhiteSpace(a.Name) ||
                !Regex.IsMatch(a.ChannelId, "^agtch_[A-Za-z0-9]+$") ||
                !Uri.TryCreate(a.ChatUrl, UriKind.Absolute, out var u) || u.Scheme != "https" || u.Host != "chatgpt.com")
                throw new InvalidOperationException("Each agent needs a valid key, name, agtch API channel ID and HTTPS chatgpt.com link.");
    }
}
public sealed record Agent(string Key, string Name, string ChannelId, string ChatUrl);
public sealed record Run(string RequestId, string AgentKey, string ChannelId, string CycleId,
    string Instructions, string Payload, string CreatedAt, string DispatchStatus = "pending",
    string? UpstreamRunId = null, string? ConversationUrl = null, string? RunStatus = null,
    string? StatusCheckedAt = null);
public sealed record Report(string RequestId, string Outcome, string Markdown, string AsOf,
    string ReceivedAt, string SubmittedBy);
public sealed class CoordinationException(string message) : Exception(message);
