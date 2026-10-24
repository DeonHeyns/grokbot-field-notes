# ChatGPT product mapping

Documentation reviewed **24 October 2026**. Availability depends on account,
platform, region, rollout and workspace settings. This is a mapping for this
pack, not a promise that every feature is enabled. [Official sources](SOURCES.md).

## Choose a surface

| Surface | Appropriate use | Setup this pack provides |
|---|---|---|
| ChatGPT Chat | Questions, drafting and short analysis | One role prompt when useful |
| ChatGPT Work | Carry a task through tools to a reviewable result | Role prompt, task brief and sources |
| ChatGPT project | Organize related chats, instructions and files | Shared operating instructions; separate outcome chats |
| Dot, where available | Ongoing responsibility and follow-up | Role, authoritative sources, boundaries and notification rules |
| Codex | Repository implementation, debugging and review | AGENTS.md and scoped task instructions |
| OpenAI API runtime | Embed agents in your own application | Design guidance only; no deployed service or SDK code |

Source: [Use ChatGPT](https://learn.chatgpt.com/docs/use-chatgpt).

## Instructions and context

Project instructions apply across that project's chats. Upload or connect the
sources needed; a ChatGPT project does not itself expose a local folder.
For Codex, repository AGENTS.md supplies project guidance; nested guidance can
refine it. Putting a file called AGENTS.md in an ordinary chat is not the same
as Codex discovering repository instructions.

Sources: [Projects](https://learn.chatgpt.com/docs/projects),
[AGENTS.md](https://learn.chatgpt.com/docs/agent-configuration/agents-md).

Memory is useful context, not the task ledger. A dot can maintain continuity and
delegate work, but a new delegated task does not automatically receive every
conversation. Use explicit source links, artifacts and handoffs. Never promise
that a correction is remembered forever, or that all role chats can read each
other. Source: [Tasks and memory](https://learn.chatgpt.com/docs/dots/tasks-and-memory).

## Tools, schedules and skills

Check the actual tools available for each operation. A connected service may
support reading without editing, or lack a particular trigger. Use approved
authentication flows; copying a role does not transfer credentials or app access.

Recurring work needs a saved scheduled task. Supported event triggers are
service-specific. A tested skill can define the workflow that the task runs.
Prompt templates in this repo do not install either one.

Sources: [Scheduled tasks](https://learn.chatgpt.com/docs/automations),
[Skills](https://learn.chatgpt.com/docs/build-skills),
[Browser](https://learn.chatgpt.com/docs/browser).

## For application developers

These are separate OpenAI runtime options, not interchangeable names for
ChatGPT Work:

| Runtime | Who manages execution |
|---|---|
| Agents API | OpenAI-managed Codex harness and session progress |
| Agents SDK | Your application, with SDK agent-loop and handoff support |
| Responses API | Your integration controls model calls and orchestration |

A ChatGPT project, an API session and your application's database are distinct
resources. Implement identity, storage, retries, approval handling and observability
for the runtime you select. This pack does not provision those components.
Source: [Agents overview](https://developers.openai.com/api/docs/guides/agents).

## Permissions remain real

Role instructions guide behavior. Workspace controls, sandbox settings and
service permissions enforce access. Test the actual operation, not just whether
the service appears connected. Role separation does not guarantee separate VMs,
filesystems or accounts. Do not change permission settings merely to make a
template work. Source: [Permissions](https://learn.chatgpt.com/docs/permissions).
