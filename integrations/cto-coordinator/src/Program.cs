using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ModelContextProtocol.AspNetCore;
using CtoCoordinator;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true).AddEnvironmentVariables();
var settings = builder.Configuration.GetSection("Coordinator").Get<Settings>() ?? new();
settings.Validate();
builder.Services.AddSingleton(settings);
builder.Services.AddSingleton<Store>();
builder.Services.AddSingleton<Coordinator>();
builder.Services.AddHttpClient<WorkspaceApi>(client => {
    client.BaseAddress = new Uri("https://api.chatgpt.com/v1/workspace_agents/");
    client.Timeout = TimeSpan.FromSeconds(30);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o => {
    o.Authority = settings.Authority;
    o.Audience = settings.Audience;
    o.MapInboundClaims = false;
    o.RequireHttpsMetadata = true;
    o.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true, ValidateAudience = true, ValidateLifetime = true,
        RequireSignedTokens = true, ClockSkew = TimeSpan.FromSeconds(30)
    };
    o.Events = new JwtBearerEvents {
        OnChallenge = ctx => {
            ctx.Response.Headers.WWWAuthenticate = "Bearer resource_metadata=\"" + settings.PublicUrl + "/.well-known/oauth-protected-resource\"";
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization(o => {
    o.AddPolicy("member", p => p.RequireAuthenticatedUser().RequireAssertion(c =>
        settings.AllowedSubjects.Contains(c.User.FindFirstValue(settings.SubjectClaim) ?? "")));
    foreach (var scope in new[] { "coordination.manage", "reports.submit" })
        o.AddPolicy(scope, p => p.RequireAuthenticatedUser().RequireAssertion(c =>
            settings.AllowedSubjects.Contains(c.User.FindFirstValue(settings.SubjectClaim) ?? "") &&
            c.User.FindAll("scope").Concat(c.User.FindAll("scp"))
                .SelectMany(x => x.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries)).Contains(scope)));
});
builder.Services.AddMcpServer()
    .WithHttpTransport(o => o.SessionMode = HttpServerSessionMode.Stateless)
    .WithTools<CoordinationTools>().AddAuthorizationFilters();
builder.WebHost.ConfigureKestrel(o => o.Limits.MaxRequestBodySize = 256 * 1024);
var app = builder.Build();
app.Use(async (context, next) => {
    // Validate browser origins. Non-browser MCP clients may legitimately omit Origin.
    if (context.Request.Headers.TryGetValue("Origin", out var origins) &&
        (origins.Count != 1 || !settings.AllowedOrigins.Contains(origins[0] ?? ""))) {
        context.Response.StatusCode = 403; return;
    }
    context.Response.Headers.CacheControl = "no-store";
    await next();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));
app.MapGet("/.well-known/oauth-protected-resource", () => Results.Json(new {
    resource = settings.PublicUrl + "/mcp",
    authorization_servers = new[] { settings.Authority },
    scopes_supported = new[] { settings.ManageScope, settings.ReportScope }
}));
app.MapGet("/.well-known/oauth-protected-resource/mcp", () => Results.Json(new {
    resource = settings.PublicUrl + "/mcp", authorization_servers = new[] { settings.Authority },
    scopes_supported = new[] { settings.ManageScope, settings.ReportScope }
}));
app.MapMcp("/mcp").RequireAuthorization("member");
app.Run();
public partial class Program { }
