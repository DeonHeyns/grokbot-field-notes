# Official source register

Reviewed: **24 October 2026**. Recheck these pages before making capability,
availability or product-configuration changes. These are documentation sources;
no account-specific feature availability was inferred from them.

| Source | Claims supported |
|---|---|
| [Use ChatGPT](https://learn.chatgpt.com/docs/use-chatgpt) | Chat, Work and Codex purposes; feature availability varies |
| [Get started with Work](https://learn.chatgpt.com/docs/get-started-with-work) | Delegate a defined outcome and review the result |
| [Projects and chats](https://learn.chatgpt.com/docs/projects) | Shared project instructions/files; distinct chats; local context |
| [AGENTS.md](https://learn.chatgpt.com/docs/agent-configuration/agents-md) | Codex discovers repository guidance with scoped precedence |
| [Build skills](https://learn.chatgpt.com/docs/build-skills) | Skill metadata, local discovery and plugin distribution |
| [Scheduled tasks](https://learn.chatgpt.com/docs/automations) | Saved schedules, independent/chat runs, supported triggers and testing |
| [Dots: tasks and memory](https://learn.chatgpt.com/docs/dots/tasks-and-memory) | Ongoing work, delegated context boundaries and saved recurrence |
| [Subagents](https://learn.chatgpt.com/docs/agent-configuration/subagents) | Actual delegation, worker context and orchestration boundaries |
| [Browser](https://learn.chatgpt.com/docs/browser) | Supported browser work and authentication/control boundaries |
| [Permissions](https://learn.chatgpt.com/docs/permissions) | Enforced filesystem/network access and managed controls |
| [Agents overview](https://developers.openai.com/api/docs/guides/agents) | Agents API, Agents SDK and Responses API are distinct runtimes |

The role boundaries, evidence checklists, handoff schema, suggested cadence and
retry policies are this pack's recommended practices, not claims of built-in
OpenAI defaults. No model price, quota, universal connector or latency guarantee
is asserted here.

## CTO Workspace Agent deployment update

Reviewed **25 October 2026**:
- [Workspace Agents](https://help.openai.com/en/articles/20001143-chatgpt-workspace-agents-for-enterprise-and-business): visible agents, builder, tools, preview, access and channels.
- [Workspace Agent triggers](https://learn.chatgpt.com/workspace-agents/trigger-runs): published-agent triggering, beta status polling and no response-text retrieval through the API.

The CTO tutorial uses nine Workspace Agents, with dots optional and outside the
core setup. Shared reports and an optional orchestration integration are design
choices; no native agent-to-agent connection or deployed bridge is asserted.
