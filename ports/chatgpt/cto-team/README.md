# CTO team: load all eight specialists

This folder contains ready-to-load instructions for a Chief of Staff dot and
all eight requested specialists. Instructions are built; connected access,
actual delegation, record persistence and recurring execution need validation
in your work workspace. No live agents or schedules are installed by these files.

## Setup tutorial

1. Open your Chief of Staff dot in your approved work workspace.
2. Download the [single-file team pack](TEAM-PACK.md) and attach it there. This
   contains the manager, shared boundaries and all eight complete assignments.
3. Fill the private configuration section there, or let the dot ask for missing
   inputs. Keep confidential configuration and results out of this repository.
4. Paste the activation request below. It authorizes one coordinated baseline,
   not recurring schedules or external messages.
5. Review the returned task references, specialist statuses, briefing and record
   locations. Verify an email response, a meeting timezone, a PR/check state and
   a news source against the original. Check proposed commitments are not being
   recorded as confirmed decisions and that partial retrieval stays visible.
6. Once useful, tell the dot your preferred reporting times and timezone. Have
   it inspect existing schedules, propose consolidation and confirm any saved
   schedules. Role names and connected apps alone do not create monitoring.

## Activation request

```text
Read the attached CTO team pack in full. Act as my Chief of Staff and configure
all eight specialist responsibilities defined there: Email, Meetings, GitHub,
Industry News, Global Technology News, AI News and Business Uses, Strategy,
and Project Management.

This is for my work as CTO of Nephila Capital. Confirm which instructions you
read and which actual delegation/source capabilities are available. Ask in one
batch for missing private configuration needed to proceed. Do not invent access.

Use actual delegated tasks when supported. Give each worker its full assignment
and the relevant context. Start the six independent specialists, then use their
verified findings for Project Management and Strategy. Queue tasks within actual
capacity. If a source is unavailable, mark that specialist blocked or partial
and continue useful independent work.

Run one baseline and return one consolidated briefing, supporting specialist
results, task references where available, proposed commitments and decisions,
and the location/status of saved records. Keep private records in the agreed
work location. Do not send messages, change source systems, modify code or
create schedules. Tell me clearly whether actual delegation worked.
```

## Individual instructions

- [Email](01-email.md)
- [Meetings](02-meetings.md)
- [GitHub](03-github.md)
- [Industry News](04-industry-news.md)
- [Global Technology News](05-technology-news.md)
- [AI News and Business Uses](06-ai-opportunities.md)
- [Strategy](07-strategy.md)
- [Project Management](08-project-management.md)

- [Chief of Staff management instructions](00-chief-of-staff.md)
- [Private configuration template](CONFIGURATION.md)

The manager waits for delivery evidence before asking Strategy to assess it.
News specialists provide bounded research, with dates, source links and business
implications. Each specialist reports coverage and uncertainty; the manager owns
reconciliation and is the single writer of the master records.

## Sources and validation

Product guidance reviewed 24 October 2026:
[Dots: tasks and memory](https://learn.chatgpt.com/docs/dots/tasks-and-memory)
and [Subagents](https://learn.chatgpt.com/docs/agent-configuration/subagents).
These support real delegation, explicit task context and saved recurrence;
availability must be verified in the actual workspace. Workflow choices in
this pack are recommendations. Repository checks validate documents, not live
mailbox, calendar, GitHub or background execution behavior.

Maintain the assignment files here for this CTO workflow. General role cards
remain in [the roster](../../../roster/README.md). Rebuild the combined pack with
`python3 scripts/build_cto_pack.py` after editing its source files.
