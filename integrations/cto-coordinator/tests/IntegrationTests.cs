using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using CtoCoordinator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Xunit;

public sealed class FakeApi : HttpMessageHandler
{
    public List<(string Key, string Body, string Path)> Triggers { get; } = [];
    public bool FailAfterAccept { get; set; }
    public bool OmitRunId { get; set; }
    public string Status { get; set; } = "completed";
    public HttpStatusCode TriggerCode { get; set; } = HttpStatusCode.Accepted;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        Assert.Equal("api.chatgpt.com", request.RequestUri!.Host);
        Assert.Equal("Bearer", request.Headers.Authorization!.Scheme);
        Assert.Equal("fake-workspace-token", request.Headers.Authorization.Parameter);
        Assert.Equal("workspace_agent_runs=v1", request.Headers.GetValues("OpenAI-Beta").Single());
        if (request.Method == HttpMethod.Post) {
            Triggers.Add((request.Headers.GetValues("Idempotency-Key").Single(), await request.Content!.ReadAsStringAsync(ct), request.RequestUri.AbsolutePath));
            if (FailAfterAccept) { FailAfterAccept = false; throw new HttpRequestException("lost response"); }
            return new(TriggerCode) { Content = JsonContent.Create(new {
                agent_trigger_run_id = OmitRunId ? null : "apirun_test123", conversation_url = "https://chatgpt.com/c/test" }) };
        }
        return new(HttpStatusCode.OK) { Content = JsonContent.Create(new { status = Status }) };
    }
    public string Ticket() => Regex.Match(JsonDocument.Parse(Triggers.Last().Body).RootElement.GetProperty("input").GetString()!, "report_ticket=([A-F0-9]+)").Groups[1].Value;
}

public sealed class Factory : WebApplicationFactory<Program>
{
    public string Database = Path.Combine(Path.GetTempPath(), "cto-tests-" + Guid.NewGuid(), "state.db");
    public readonly FakeApi Api = new();
    private readonly SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("test-only-signing-key-with-at-least-32-bytes-000"));
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        foreach (var kv in new Dictionary<string,string> {
            ["PublicUrl"]="https://coordinator.test", ["Authority"]="https://issuer.test", ["Audience"]="https://coordinator.test/mcp",
            ["AllowedSubjects:0"]="owner", ["WorkspaceAccessToken"]="fake-workspace-token",
            ["TicketKey"]=new string('a',64), ["DatabasePath"]=Database,
            ["Agents:0:Key"]="email", ["Agents:0:Name"]="CTO Email", ["Agents:0:ChannelId"]="agtch_email123", ["Agents:0:ChatUrl"]="https://chatgpt.com/agents/email"
        }) builder.UseSetting("Coordinator:" + kv.Key, kv.Value);
        builder.ConfigureTestServices(services => {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options => {
                var oidc = new OpenIdConnectConfiguration { Issuer = "https://issuer.test" };
                oidc.SigningKeys.Add(key);
                options.ConfigurationManager = new StaticConfigurationManager<OpenIdConnectConfiguration>(oidc);
            });
            services.AddHttpClient<WorkspaceApi>().ConfigurePrimaryHttpMessageHandler(() => Api);
        });
    }
    public HttpClient Client(string? scope = "coordination.manage", string subject = "owner", string? audience = null, bool expired = false)
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.DefaultRequestHeaders.Accept.ParseAdd("text/event-stream");
        client.DefaultRequestHeaders.Add("MCP-Protocol-Version", "2025-11-25");
        if (scope != null) {
            var token = new JwtSecurityToken("https://issuer.test", audience ?? "https://coordinator.test/mcp",
                [new Claim("sub", subject), new Claim("scope", scope)],
                DateTime.UtcNow.AddHours(-1), expired ? DateTime.UtcNow.AddMinutes(-5) : DateTime.UtcNow.AddMinutes(5),
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            client.DefaultRequestHeaders.Authorization = new("Bearer", new JwtSecurityTokenHandler().WriteToken(token));
        }
        return client;
    }
    public async Task<JsonElement> Rpc(HttpClient client, string method, object? args = null)
    {
        using var response = await client.PostAsJsonAsync("/mcp", new { jsonrpc="2.0", id=1, method, @params=args });
        var text = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"HTTP {response.StatusCode}: {text}");
        if (text.StartsWith("event:") || text.StartsWith("data:")) text = string.Join("\n", text.Split('\n').Where(x => x.StartsWith("data:")).Select(x => x[5..].Trim()));
        return JsonDocument.Parse(text).RootElement.Clone();
    }
    public Task<JsonElement> Call(HttpClient c, string name, object args) => Rpc(c, "tools/call", new { name, arguments = args });
    public static JsonElement Output(JsonElement response)
    {
        Assert.False(response.TryGetProperty("error", out _), response.ToString());
        var result = response.GetProperty("result");
        Assert.False(result.TryGetProperty("isError", out var error) && error.GetBoolean(), response.ToString());
        return JsonDocument.Parse(result.GetProperty("content")[0].GetProperty("text").GetString()!).RootElement.Clone();
    }
    public static void Error(JsonElement response) => Assert.True(response.TryGetProperty("error", out _) ||
        (response.GetProperty("result").TryGetProperty("isError", out var error) && error.GetBoolean()), response.ToString());
    protected override void Dispose(bool disposing) { base.Dispose(disposing); Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools(); if (Directory.Exists(Path.GetDirectoryName(Database))) Directory.Delete(Path.GetDirectoryName(Database)!, true); }
}

public sealed class IntegrationTests
{
    [Fact]
    public async Task AuthenticationAndDiscoveryAreEnforced()
    {
        using var f = new Factory();
        using var anonymous = f.Client(null);
        using var denied = await anonymous.PostAsJsonAsync("/mcp", new { });
        Assert.Equal(HttpStatusCode.Unauthorized, denied.StatusCode);
        Assert.Contains("oauth-protected-resource", denied.Headers.WwwAuthenticate.ToString());
        using var wrongUser = f.Client(subject:"stranger");
        Assert.Equal(HttpStatusCode.Forbidden, (await wrongUser.PostAsJsonAsync("/mcp", new { })).StatusCode);
        using var wrongAudience = f.Client(audience:"other-api");
        Assert.Equal(HttpStatusCode.Unauthorized, (await wrongAudience.PostAsJsonAsync("/mcp", new { })).StatusCode);
        using var expired = f.Client(expired:true);
        Assert.Equal(HttpStatusCode.Unauthorized, (await expired.PostAsJsonAsync("/mcp", new { })).StatusCode);
        var metadata = await anonymous.GetFromJsonAsync<JsonElement>("/.well-known/oauth-protected-resource");
        Assert.Equal("https://coordinator.test/mcp", metadata.GetProperty("resource").GetString());
        using var chief = f.Client();
        var init = await f.Rpc(chief, "initialize", new { protocolVersion="2025-11-25", capabilities=new {}, clientInfo=new {name="test", version="1"} });
        Assert.True(init.GetProperty("result").TryGetProperty("serverInfo", out _));
        chief.DefaultRequestHeaders.Add("Origin", "https://attacker.test");
        Assert.Equal(HttpStatusCode.Forbidden, (await chief.PostAsJsonAsync("/mcp", new { })).StatusCode);
    }

    [Fact]
    public async Task RealMcpRoundTripSeparatesRunCompletionFromReportReceipt()
    {
        using var f = new Factory(); using var chief=f.Client(); using var specialist=f.Client("reports.submit");
        var registry=Factory.Output(await f.Call(chief,"list_agents",new {})); Assert.Equal("CTO Email",registry[0].GetProperty("name").GetString());
        var id=Guid.NewGuid().ToString(); var args=new {agent_key="email",cycle_id="baseline-1",request_id=id,instructions="Review the test inbox"};
        var started=Factory.Output(await f.Call(chief,"start_agent_run",args)); Assert.False(started.GetProperty("reportReceived").GetBoolean());
        Factory.Output(await f.Call(chief,"start_agent_run",args)); Assert.Single(f.Api.Triggers);
        var status=Factory.Output(await f.Call(chief,"get_run_status",new {request_id=id}));
        Assert.Equal("completed",status.GetProperty("runStatus").GetString()); Assert.False(status.GetProperty("reportReceived").GetBoolean());
        var report=new {request_id=id,report_ticket=f.Api.Ticket(),outcome="partial",markdown="One source checked; archive unavailable. [Source](https://example.com)",as_of="2026-01-01T10:00:00Z"};
        Factory.Output(await f.Call(specialist,"submit_report",report)); Factory.Output(await f.Call(specialist,"submit_report",report));
        var reports=Factory.Output(await f.Call(chief,"read_cycle_reports",new {cycle_id="baseline-1"}));
        Assert.Equal("partial",reports.GetProperty("items")[0].GetProperty("report").GetProperty("outcome").GetString());
        Assert.DoesNotContain(f.Api.Ticket(),reports.ToString());
        Factory.Error(await f.Call(specialist,"submit_report",new {request_id=id,report_ticket=f.Api.Ticket(),outcome="complete",markdown="changed",as_of=report.as_of}));
        Factory.Error(await f.Call(specialist,"submit_report",new {request_id=id,report_ticket="bad",outcome="complete",markdown="forged",as_of=report.as_of}));
    }

    [Fact]
    public async Task ToolScopesPreventSpecialistDispatchAndChiefReportSpoofing()
    {
        using var f = new Factory(); using var specialist=f.Client("reports.submit"); using var chief=f.Client();
        var list=await f.Rpc(specialist,"tools/list",new {});
        Assert.DoesNotContain("start_agent_run",list.ToString()); Assert.Contains("submit_report",list.ToString());
        Factory.Error(await f.Call(specialist,"start_agent_run",new {agent_key="email",cycle_id="c",request_id=Guid.NewGuid().ToString(),instructions="test"}));
        Factory.Error(await f.Call(chief,"submit_report",new {request_id=Guid.NewGuid().ToString(),report_ticket="bad",outcome="complete",markdown="fake",as_of="2026-01-01T00:00:00Z"}));
        Assert.Empty(f.Api.Triggers);
    }

    [Fact]
    public async Task AmbiguousDispatchRetriesPersistIdenticalPayloadAndKey()
    {
        using var f=new Factory(); using var chief=f.Client();
        var id=Guid.NewGuid().ToString(); var args=new {agent_key="email",cycle_id="c",request_id=id,instructions="test"};
        f.Api.FailAfterAccept=true; Factory.Error(await f.Call(chief,"start_agent_run",args));
        Factory.Output(await f.Call(chief,"start_agent_run",args)); Assert.Equal(2,f.Api.Triggers.Count);
        Assert.Equal(f.Api.Triggers[0],f.Api.Triggers[1]);
        Factory.Error(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="c",request_id=id,instructions="different"}));
        Factory.Error(await f.Call(chief,"start_agent_run",new {agent_key="unknown",cycle_id="c",request_id=Guid.NewGuid().ToString(),instructions="test"}));
        Factory.Error(await f.Call(chief,"read_cycle_reports",new {cycle_id="../../etc"}));
        Assert.Equal(2,f.Api.Triggers.Count);
    }

    [Fact]
    public async Task MissingBetaRunIdDoesNotBecomeFalseCompletionAndFollowupKeepsConversation()
    {
        using var f=new Factory(); using var chief=f.Client(); f.Api.OmitRunId=true;
        var id=Guid.NewGuid().ToString();
        var result=Factory.Output(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="c",request_id=id,instructions="first"}));
        Assert.False(result.GetProperty("pollingAvailable").GetBoolean());
        result=Factory.Output(await f.Call(chief,"get_run_status",new {request_id=id})); Assert.False(result.TryGetProperty("runStatus",out var state) && state.ValueKind != JsonValueKind.Null);
        Factory.Output(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="c",request_id=Guid.NewGuid().ToString(),instructions="follow up"}));
        Assert.NotEqual(f.Api.Triggers[0].Key,f.Api.Triggers[1].Key);
        Assert.Equal(JsonDocument.Parse(f.Api.Triggers[0].Body).RootElement.GetProperty("conversation_key").GetString(),JsonDocument.Parse(f.Api.Triggers[1].Body).RootElement.GetProperty("conversation_key").GetString());
    }

    [Fact]
    public async Task StateSurvivesStoreReopen()
    {
        using var f=new Factory(); using var chief=f.Client();
        var id=Guid.NewGuid().ToString();
        Factory.Output(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="c",request_id=id,instructions="test"}));
        var reopened=new Store(f.Services.GetRequiredService<Settings>());
        Assert.Equal("accepted",reopened.GetRun(id)!.DispatchStatus);
    }
    [Fact]
    public async Task ConcurrentRetriesDispatchOnce()
    {
        using var f=new Factory(); using var chief=f.Client();
        var args=new {agent_key="email",cycle_id="concurrent",request_id=Guid.NewGuid().ToString(),instructions="test"};
        var results=await Task.WhenAll(Enumerable.Range(0,8).Select(_=>f.Call(chief,"start_agent_run",args)));
        foreach(var result in results) Factory.Output(result);
        Assert.Single(f.Api.Triggers);
    }

    [Fact]
    public async Task RateLimitErrorDoesNotCreateACompletedRunOrNewIdOnRetry()
    {
        using var f=new Factory(); using var chief=f.Client();
        var args=new {agent_key="email",cycle_id="rate-limit",request_id=Guid.NewGuid().ToString(),instructions="test"};
        f.Api.TriggerCode=HttpStatusCode.TooManyRequests;
        Factory.Error(await f.Call(chief,"start_agent_run",args));
        var pending=Factory.Output(await f.Call(chief,"get_run_status",new {request_id=args.request_id}));
        Assert.Equal("pending",pending.GetProperty("dispatchStatus").GetString());
        Assert.False(pending.GetProperty("reportReceived").GetBoolean());
        f.Api.TriggerCode=HttpStatusCode.Accepted;
        Factory.Output(await f.Call(chief,"start_agent_run",args));
        Assert.Equal(f.Api.Triggers[0],f.Api.Triggers[1]);
    }

    [Fact]
    public async Task ReportTicketIsBoundToRequestAndExpires()
    {
        using var f=new Factory(); using var chief=f.Client(); using var specialist=f.Client("reports.submit");
        var first=Guid.NewGuid().ToString(); var second=Guid.NewGuid().ToString();
        Factory.Output(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="tickets",request_id=first,instructions="test"}));
        var ticket=f.Api.Ticket();
        Factory.Output(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="tickets",request_id=second,instructions="test"}));
        Factory.Error(await f.Call(specialist,"submit_report",new {request_id=second,report_ticket=ticket,outcome="complete",markdown="wrong run",as_of="2026-01-01T00:00:00Z"}));
        Factory.Error(await f.Call(specialist,"submit_report",new {request_id=first,report_ticket=ticket,outcome="complete",markdown="ticket: "+ticket,as_of="2026-01-01T00:00:00Z"}));
        Factory.Error(await f.Call(specialist,"submit_report",new {request_id=first,report_ticket=ticket,outcome="complete",markdown="report",as_of="2026-01-01T00:00:00"}));
        var store=f.Services.GetRequiredService<Store>(); var run=store.GetRun(first)!;
        store.SaveRun(run with {CreatedAt=DateTimeOffset.UtcNow.AddDays(-8).ToString("O")});
        Factory.Error(await f.Call(specialist,"submit_report",new {request_id=first,report_ticket=ticket,outcome="complete",markdown="report",as_of="2026-01-01T00:00:00Z"}));
        Assert.Null(store.GetReport(first)); Assert.Null(store.GetReport(second));
    }

    [Fact]
    public async Task ReportsArePaginatedAndBoundedByCycle()
    {
        using var f=new Factory(); using var chief=f.Client();
        for(var i=0;i<5;i++) Factory.Output(await f.Call(chief,"start_agent_run",new {agent_key="email",cycle_id="pages",request_id=Guid.NewGuid().ToString(),instructions="test"}));
        var first=Factory.Output(await f.Call(chief,"read_cycle_reports",new {cycle_id="pages"}));
        Assert.Equal(4,first.GetProperty("items").GetArrayLength()); Assert.Equal(4,first.GetProperty("nextOffset").GetInt32());
        var second=Factory.Output(await f.Call(chief,"read_cycle_reports",new {cycle_id="pages",offset=4}));
        Assert.Single(second.GetProperty("items").EnumerateArray());
        var empty=Factory.Output(await f.Call(chief,"read_cycle_reports",new {cycle_id="other"}));
        Assert.Empty(empty.GetProperty("items").EnumerateArray());
    }

}
