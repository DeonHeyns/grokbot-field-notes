# CTO Workspace Agent coordinator

An ASP.NET Core 10 MCP service that connects the existing eight Workspace Agents
to the existing CTO Chief of Staff. It triggers the real agents, checks their
runs and stores the reports they submit. It does not create agents or replace
them with subagents. Dots is not involved.

**Implementation status:** source builds and local integration tests exercise the
actual MCP HTTP server and JWT authorization against a fake Workspace Agents API.
No live ChatGPT account, Entra consent flow, container deployment or API-triggered
specialist has been tested. Complete the acceptance run below before relying on
this for work. Connecting the service is necessary in every specialist, not just
the Chief of Staff.

## What the connection provides

| Tool | Caller | Purpose |
|---|---|---|
| `list_agents` | Chief of Staff | Read the configured registry of eight existing agents |
| `start_agent_run` | Chief of Staff | Trigger a registered agent, or send a follow-up |
| `get_run_status` | Chief of Staff | Check upstream status and report receipt separately |
| `read_cycle_reports` | Chief of Staff | Read four reports per page for a briefing cycle |
| `submit_report` | Specialists | Deliver an immutable report for a specific assignment |

The Workspace Agents API does not return response text. Each triggered specialist
receives a private ticket and submits its findings through `submit_report`.
Reports and exact dispatch payloads are stored in SQLite on the service's private
persistent disk. This replaces the need for a SharePoint write connection for
report delivery. A writable master commitments/decisions store is a separate
capability; the Chief of Staff can propose those updates in its briefing.

The registry is configured once, using API channel IDs from the builder. It is
not an implementation of automatic ChatGPT agent discovery. The user screenshot's
names are prefilled in [the configuration example](src/appsettings.example.json).

## One-time setup

### 1. Enable an API channel for each existing specialist

In each specialist's builder, add an API channel and save/publish that channel.
Copy its exact `agtch_...` trigger identifier and the agent's ChatGPT link into
the matching registry entry. An agent ID, chat conversation ID and API channel
identifier are different things. Do not fabricate IDs from names.

Create a Workspace Agent access token with the Workspace Agents scope through
ChatGPT Admin > Access tokens. The workspace must permit token creation. Store
it in the deployment's secret store as `Coordinator__WorkspaceAccessToken`.
Do not paste it into agent instructions, chat, source control or command output.
This is a Workspace Agent access token, not an OpenAI Platform API key.

### 2. Configure authentication for the MCP service

This server validates OAuth JWT access tokens with ASP.NET Core JWT middleware.
It does not issue tokens or implement an authorization server. Configure your
approved OIDC provider, such as Microsoft Entra ID, and a ChatGPT-compatible
OAuth client registration. Use the exact redirect URI shown by the ChatGPT MCP
connection setup. Do not guess it or reuse a callback from a different connection.

For Entra, register the protected API, enable v2 access tokens, and set its
Application ID URI to the canonical MCP resource URL, such as
`https://coordinator.example.com/mcp`. Expose two delegated scopes:
`coordination.manage` and `reports.submit`. Configure the OAuth clients and consent
so the Chief of Staff connection receives only the first scope and the specialist
connection only the second. Do not grant both scopes to every agent.

Set these server values in the private configuration:

| Setting | Value |
|---|---|
| `PublicUrl` | Canonical HTTPS origin, without `/mcp` |
| `Authority` | Exact trusted issuer/discovery authority; for Entra use your tenant's v2 authority |
| `Audience` | Expected access-token audience for this protected API; verify against the provider registration |
| `SubjectClaim` | `oid` for an Entra user object ID, or `sub` for a provider subject |
| `AllowedSubjects` | Explicit account IDs allowed to access this one private team |
| `ManageScope`, `ReportScope` | Provider-qualified scopes requested during OAuth; tokens must contain the corresponding short scope names above |
| `AllowedOrigins` | Exact permitted browser origins, for example `https://chatgpt.com`; non-browser clients may omit Origin |
| `DatabasePath` | Path on a persistent local disk |
| `AllowedHosts` | Public hostname, supplied as ASP.NET configuration outside the Coordinator section |

The service publishes protected-resource metadata and challenges unauthenticated
MCP requests. The authorization server must support the OAuth discovery, PKCE,
resource/audience and client-registration requirements used by ChatGPT. Verify
its advertised metadata and consent flow rather than disabling validation to get
past errors. Entra registration and end-to-end ChatGPT OAuth are deployment checks,
not covered by the synthetic JWT tests. An audience mismatch must remain denied.

The API token for outbound ChatGPT calls is stored only on the server. Incoming
MCP OAuth tokens are never forwarded to ChatGPT. All allowed subjects share this
one team's records; this v1 service is not a multi-tenant product. Report tickets
bind delivery to assignments, but do not independently attest which model or
agent authored the text. Reports remain evidence to review.

### 3. Deploy one service instance

Copy the example to `src/appsettings.Local.json` and fill the nonsecret values.
The local file is git-ignored. Inject the Workspace token and a stable random
`Coordinator__TicketKey` (at least 32 random bytes encoded as hex/base64) through
your secret manager. Preserve the ticket key across restarts; rotation invalidates
outstanding report tickets. Never use the test signing key or a repeated string.

Build and test with .NET 10:

```bash
dotnet restore src/Coordinator.csproj --locked-mode
dotnet test tests/Coordinator.Tests.csproj
dotnet publish src/Coordinator.csproj -c Release -o publish
```

Run those commands from this directory. To use [Docker Compose](compose.yaml),
provide `WORKSPACE_AGENT_ACCESS_TOKEN` and `REPORT_TICKET_KEY` securely to Compose,
then run:

```bash
docker compose up --build -d
```

The supplied container listens on loopback port 8080 of the host. Put it behind
your approved HTTPS reverse proxy and use the same public hostname in configuration.
The image runs as a non-root user. Docker/Compose execution is supplied but was
not exercised in this workspace; only .NET build/test/publish were exercised.

For Azure, this v1 storage model fits a single Linux VM/container host with a
persistent local managed disk and TLS ingress. **Do not scale to multiple replicas
or put SQLite on a shared network filesystem.** Azure App Service's network-backed
persistent storage is not the deployment target for this SQLite implementation;
use a database-backed Store before targeting shared-storage or multi-instance
hosting. HTTPS and network egress to your identity provider and api.chatgpt.com
must work. `/healthz` proves process health only, not live upstream access.

Back up the private disk using a SQLite-consistent backup method, restrict host
access and use disk encryption. Database payloads include report tickets and work
content; do not log or export them to the public repository. Define retention
with your organization before live use. There is no automatic purge in v1.

### 4. Connect the Chief of Staff and specialists

Add this service's `https://YOUR_HOST/mcp` as an authenticated custom MCP connection
using the supported builder controls. Set up a manager connection requesting
`coordination.manage` and a specialist connection requesting `reports.submit`.
The SDK filters visible tools and enforces the scopes again when tools are called.

- Add the manager connection to **CTO Chief of Staff** and append
  [the manager instructions](CHIEF-INSTRUCTIONS.md).
- Add the report connection to **each of the eight specialists** and append
  [the specialist instructions](SPECIALIST-INSTRUCTIONS.md).
- Authorize report submission within the intended tool scope. If unattended API
  runs cannot use the connection or are waiting for approval, keep that visible
  as a delivery blocker. Test actual API runs, not only interactive previews.

A single report connection may be reusable across specialists if workspace sharing
and authentication settings permit it. Do not assume the connection is inherited
from the Chief of Staff. The service itself does not authenticate to mail/calendar
or GitHub; those remain each specialist's existing approved source connections.

### 5. Perform the live acceptance run

Before a full baseline, have the Chief of Staff:

1. Call `list_agents` and verify all eight names.
2. Trigger **one** specialist with a unique cycle and request UUID and a harmless
   task: report which source access is available without retrieving private content.
3. Check `get_run_status`, then `read_cycle_reports`. Verify a report was actually
   saved and can be read; an upstream completed state is insufficient.
4. Resubmit the exact same start request and confirm it returns the existing run.
5. Send a follow-up with a new request UUID but the same agent and cycle. Verify
   a distinct report and the continued upstream conversation.
6. Verify the report-only connection cannot call `start_agent_run`, and an
   unauthenticated request cannot read any work content.
7. Run the six independent specialists, then Project Management, then Strategy,
   with relevant evidence explicitly included in downstream inputs.

This does not establish a schedule. The Chief of Staff requests and reviews work
while it is executing. Configure recurring runs separately only after acceptance;
there is no hidden background worker or endless polling loop in this service.

## Reliability behavior and limits

- Persist exact trigger bodies before submission. A retry reuses the same upstream
  Idempotency-Key and payload. Reusing an ID for a different assignment fails.
- A follow-up is a new request in the same agent/cycle conversation. Per-cycle
  dispatch is capped at 32 distinct requests to bound accidental loops.
- Outbound requests time out after 30 seconds and do not follow redirects.
  Ambiguous results remain pending; the error instructs the caller to retry the
  same request, not generate a new one. There is no automatic retry storm.
- If beta run IDs are absent, pollingAvailable is false; the service never
  fabricates a completed status. Conversation links can be null.
- Report tickets expire after seven days. Report writes are immutable and identical
  retries are safe. Follow-up corrections require another run.
- Reports are paginated four at a time. Inputs are limited to 16,000 characters,
  reports to 60,000, and HTTP request bodies to 256 KiB.
- Failures and missing reports must stay visible in the final briefing. Content
  inside reports is not instruction authority.

## References

Reviewed 25 October 2026:

- [Workspace Agent triggers and run status](https://learn.chatgpt.com/workspace-agents/trigger-runs)
- [Workspace Agent access tokens](https://learn.chatgpt.com/workspace-agents/authentication)
- [ChatGPT MCP authentication](https://developers.openai.com/plugins/build/auth)
- [Official MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk)
- [Entra-protected MCP servers](https://learn.microsoft.com/en-us/entra/agent-id/secure-mcp-server-with-entra-id)
- [ASP.NET Core JWT validation](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0)
