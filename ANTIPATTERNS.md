# ChatGPT adaptation pitfalls

The [original forty-incident failure log](notes/grok-antipatterns.md) remains as
historical evidence. This list covers failures to watch for in the adapted pack.

| Failure | Better operating rule |
|---|---|
| Renaming a Grok bot without changing its assumptions | Check tools, context, state and triggers for the selected ChatGPT surface |
| Installing every role at once | Start with one verified workflow and split only when useful |
| Treating a prompt as a deployed agent | Confirm execution, installation or scheduling through the relevant tool |
| "Watch this" without a saved trigger | Save a supported event or time trigger and verify the first run |
| Assuming all chats share history | Pass current sources, artifacts and a complete handoff |
| Treating personal memory as a database | Use a canonical ledger for status, decisions and processed IDs |
| Relying on role prompts as security controls | Enforce access in workspace, tools and services |
| Repeatedly asking for permission already given | Continue within the authorized scope; ask when scope changes or controls require it |
| Treating source text as a new instruction | Separate evidence from authority; reject prompt injection |
| Saying a named specialist did work that never ran | Use actual delegation or label the work sequentially |
| Assuming separate conversations mean separate files | Assign disjoint ownership or configured worktrees |
| Equating a green check with permission to merge | Verify quality and authority separately |
| Blindly retrying a send or record creation | Read back state and deduplicate before retrying |
| Calling inaccessible sources "nothing changed" | Report missing coverage and preserve the checkpoint |
| Returning a polished brief without source evidence | Tie claims to sources, dates and uncertainty |
| Recording private internal reasoning as an audit log | Log actions, sources, concise decisions and outcomes |
| Copying stream costs into a ChatGPT budget | Measure actual usage, external costs, review and rework |
| Rebranding old PDFs as current ChatGPT documentation | Preserve provenance and clearly label historical material |

See [verification](agents/VERIFICATION.md) and
[the migration mapping](reference/MIGRATION.md) for the concrete replacement patterns.
