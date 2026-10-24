# What changed from Grok Bot

| Original pattern or portability trap | ChatGPT adaptation | Where |
|---|---|---|
| A bot is a persistent named colleague with its own memory and computer | A role is a responsibility; choose Work, a project chat, a dot or an API runtime explicitly | PRODUCT.md; roster |
| Copy a description into a bot's "system prompt" | Copy a complete instruction pack into the selected surface; host instructions still apply | ports/chatgpt |
| Read all bots' conversations | Use supplied transcripts, authorized retrieval and an explicit task ledger | Chief of Staff; Knowledge Base Manager |
| Bots message each other in a shared huddle | Use actual delegation tools when available; otherwise sequential work and explicit handoffs | ORCHESTRATION.md |
| Parallelism is free and each worker gets a separate computer | Bound concurrency; use disjoint file ownership or configured worktrees | ORCHESTRATION.md |
| "Every morning" starts a routine | Save and verify a scheduled task, with time zone, delivery and stop rules | SKILLS-AND-ROUTINES.md |
| Every service supports webhook monitoring | Check service-specific triggers; use a supported schedule or built integration | AUTOMATION.md |
| Grok-specific duplicate/share semantics and per-bot memory | Share reviewed instructions and sources; verify context and reconnect accounts separately | ChatGPT quickstart |
| A meeting attendee always joins and listens | Transcript-first notes; live joining only with a verified integration and authorization | meeting-attendee.md |
| A slash command such as /verify exists | Name real repository commands or explicitly installed skills | VERIFICATION.md |
| Green checks permit autonomous merge | Verification and merge authority are separate | AGENTS.md; PR Reviewer |
| Internal Slack posts need no authorization | Posting is a send; authorize destination and content scope | Shared operating rules |
| Confidence alone authorizes a support reply/refund | Evidence, policy coverage and separate action permission | customer-support.md |
| Inspect browser traffic and import cookies to automate | Supported connectors, documented APIs and approved sign-in flows | Shared operating rules |
| Grok stream costs predict ChatGPT costs | Preserve them as history; measure actual usage and review effort | ECONOMICS.md |

The 69 role filenames and nine playbook filenames remain stable. Role bodies
and workflows are adapted; generated ChatGPT and Cursor packs derive from the
same role source. Original narratives remain in the
[source revision](../PROVENANCE.md). The historical PDFs are deliberately labeled,
not rebranded as ChatGPT guides.
