using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace CtoCoordinator;

public sealed class WorkspaceApi(HttpClient http, Settings settings)
{
    public async Task<(string? RunId, string? Url)> Trigger(Run run, CancellationToken ct)
    {
        using var request = Request(HttpMethod.Post, run.ChannelId + "/trigger");
        request.Headers.Add("OpenAI-Beta", "workspace_agent_runs=v1");
        request.Headers.Add("Idempotency-Key", run.RequestId);
        request.Content = new StringContent(run.Payload, Encoding.UTF8, "application/json");
        using var response = await http.SendAsync(request, ct);
        if (response.StatusCode != HttpStatusCode.Accepted)
            throw new CoordinationException($"Trigger returned HTTP {(int)response.StatusCode}. Reuse the same request_id and payload when retrying; do not create a fresh request.");
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));
        var id = String(json.RootElement, "agent_trigger_run_id");
        if (id != null && !System.Text.RegularExpressions.Regex.IsMatch(id, "^apirun_[A-Za-z0-9]+$"))
            throw new CoordinationException("Unexpected upstream run identifier; retry the same request_id.");
        return (id, String(json.RootElement, "conversation_url"));
    }
    public async Task<string> Status(Run run, CancellationToken ct)
    {
        using var request = Request(HttpMethod.Get, run.ChannelId + "/runs/" + run.UpstreamRunId);
        request.Headers.Add("OpenAI-Beta", "workspace_agent_runs=v1");
        using var response = await http.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode)
            throw new CoordinationException($"Status check returned HTTP {(int)response.StatusCode}; completion is unverified.");
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));
        var status = String(json.RootElement, "status");
        if (status is not ("queued" or "in_progress" or "suspended" or "completed" or "failed"))
            throw new CoordinationException("Unknown upstream status; completion is unverified.");
        return status;
    }
    private HttpRequestMessage Request(HttpMethod method, string path)
    {
        var request = new HttpRequestMessage(method, path);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", settings.WorkspaceAccessToken);
        return request;
    }
    private static string? String(JsonElement element, string key) =>
        element.TryGetProperty(key, out var value) && value.ValueKind == JsonValueKind.String ? value.GetString() : null;
}
