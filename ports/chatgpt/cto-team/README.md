# Create a visible CTO agent team in ChatGPT

Create eight specialist Workspace Agents first, then create a ninth Workspace
Agent named CTO Chief of Staff. All nine are visible in ChatGPT's Agents area.
Dots has no required role in this setup. Files supply instructions; they do not
install agents, connect accounts or establish inter-agent communication.

## 1. Create the individual agents

Use your approved work workspace. For each row below:

1. Open **Agents > Create > Start blank** (or use the conversational builder).
2. Set the listed name and use that file's full contents as the instructions.
   If needed, attach the file and explicitly tell the builder to use it.
3. Add the required available source tools and files. Verify the account and
   scope. Keep message sending and source edits approval-controlled.
4. Configure a private report destination and authorize only the intended
   report writes. If unavailable, use manual report transfer during testing.
5. Under **Channels > ChatGPT**, start with **Private to me**.
6. Select **Preview**, run one bounded assignment, and inspect sources and report
   saving. Select **Create**, then record the actual agent link in the directory.

| Agent name | Instructions | Sources to configure |
|---|---|---|
| CTO Email | [Email](01-email.md) | Approved mailbox and sent mail |
| CTO Meetings | [Meetings](02-meetings.md) | Calendar, meeting records |
| CTO GitHub | [GitHub](03-github.md) | Selected repositories, PRs, issues and checks |
| CTO Industry Intelligence | [Industry](04-industry-news.md) | Web search and industry source guidance |
| CTO Technology Intelligence | [Technology](05-technology-news.md) | Web search and confirmed stack |
| CTO AI Opportunities | [AI](06-ai-opportunities.md) | Web search, priorities and opportunity backlog |
| CTO Strategy | [Strategy](07-strategy.md) | Priorities, decisions and specialist reports |
| CTO Project Management | [Projects](08-project-management.md) | Plans, boards and specialist reports |

First test: use the assignment's baseline window and request a source-linked
report without external messages or source edits. Check an email reply, a
meeting timezone, a PR status and a news source against originals. For Strategy
and Project Management, supply the existing reports explicitly. A private agent
listing does not itself verify the permissions of the connected report storage.

## 2. Create a Chief of Staff agent

1. Open **Agents > Create > Start blank** again.
2. Name it **CTO Chief of Staff** and load
   [the manager instructions](00-chief-of-staff.md).
3. Complete [the private configuration and agent directory](CONFIGURATION.md)
   in your work workspace. Give the manager the actual links, report locations,
   priorities and source scope. Do not commit filled configuration to this repo.
4. Add a connection that can read the designated specialist reports and, when
   authorized, write the master commitments and decisions records.
5. Start in **shared-report mode**. The specialists run manually or on separately
   confirmed schedules; the manager reads their reports. Agent names and links
   do not grant a built-in ability to call them.
6. Preview the request below, then select **Create** when its result is verified.

```text
Act as CTO Chief of Staff using your configured instructions and private agent
directory. Read the reports from my eight Workspace Agents. Check timestamps,
coverage and evidence. Return one briefing with priorities, decisions and
recommendations, commitments at risk, meeting preparation, external developments
and missing information. Include each specialist's report link and status.

Reconcile duplicates and conflicting findings. Propose master-record updates;
save them only within my configured authority. Prepare follow-up requests for
named specialists, labelled not dispatched unless an actual configured tool
successfully sent them. Do not replace the specialists with subagents.
Do not create schedules or send messages to people during this test.
```

### Enable active coordination when an integration is ready

If the manager must request runs itself, configure and test a separate
integration. Add an API channel to each specialist and expose approved trigger
and status operations to the Chief of Staff through a supported tool, such as
a custom MCP. Store tokens in the integration's secret store, not these files.
Record API channel identifiers separately from ChatGPT agent links.

The documented API can trigger published agents and poll run status (beta), but
cannot retrieve response text. Each specialist must therefore write its report
to a location the manager can read, or use another verified result mechanism.
A successful trigger is not a successful report. Test dispatch, source access,
report persistence, manager readback, failures and follow-ups end to end before
claiming autonomous coordination. The [ASP.NET Core coordination service](../../../integrations/cto-coordinator/README.md) implements that bridge, including report submission and retrieval. It must be deployed, connected and tested with your actual agents.

The intended sequence is six independent source/research agents, then Project
Management, then Strategy, then the Chief of Staff's consolidated briefing.
Use a cycle ID and freshness checks. Without the bridge, perform handoffs
manually; schedule offsets alone do not enforce this dependency order.

## 3. Dots: optional, outside the core setup

No dot is needed to create or run this team. Your Chief of Staff is the ninth
Workspace Agent. You can talk to it directly in ChatGPT. A personal dot could
later be an optional interface to approved summaries if access is configured,
but it is not the coordinator, does not own the specialists and gains no access
from their names. Keep private work information in the approved work workspace.
Do not create a duplicate Chief of Staff in dots as part of this tutorial.

## Maintenance and validation

[TEAM-PACK.md](TEAM-PACK.md) is a reference bundle of all nine instruction sets,
not a bulk import or dot activation file. Use individual files in the builder.
Rebuild it with `python3 scripts/build_cto_pack.py` after source edits.

Reviewed 25 October 2026 against
[Workspace Agent setup](https://help.openai.com/en/articles/20001143-chatgpt-workspace-agents-for-enterprise-and-business)
and [API triggers](https://learn.chatgpt.com/workspace-agents/trigger-runs).
These document creation and API capabilities; shared-report contracts and the
orchestration bridge are proposed implementation choices. Document checks do
not verify live accounts, agent creation, report writes or orchestration.
