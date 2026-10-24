# Coordination and delegation

Start with one agent completing one outcome. Split when separate expertise,
permissions, independent tasks or context boundaries justify the extra cost.
A catalogue of 69 roles is a menu, not a deployment plan.

## A role is a responsibility

A role means instructions, inputs, permitted actions and a defined output.
It can run as a task in one conversation, a separate project chat, an available
delegated worker, or an implemented API agent. Naming it does not create a
persistent colleague with access to every other conversation.

Use ChatGPT Work for a bounded assignment; consider a dot for an ongoing
responsibility where available. A coordinator can use supported delegation
tools, but must not claim to have launched or contacted specialists without a
confirmed result. Without those tools, do the stages sequentially or produce
task briefs for separate chats. Label sequential reviews accurately.

## Start with a small team

| Responsibility | Output | Next decision |
|---|---|---|
| Coordinator | Task list, context packets, consolidated status | User decides scope changes |
| Specialist | Artifact, evidence and open questions | Coordinator checks acceptance |
| Reviewer, when useful | Findings against original requirements | User decides release or publication |

The reviewer inspects the source and artifact, not merely the specialist's
summary. Independent review improves coverage; it is not a guarantee.

## Pass a complete handoff

Use [the handoff template](../ports/chatgpt/HANDOFF.md). Include the outcome,
acceptance criteria, source links and versions, assigned subset, allowed actions,
output destination, verification, deadline, retry limit and blocker handling.

An agent's answer is evidence to check against original sources. It does not
become authoritative because the role is called "Source of Truth."

## Keep status outside transient context

Use one canonical task ledger: an approved document, issue tracker or repository
file. Suggested columns: task ID, owner, status, artifact, source version,
last verified time, blocker and next action. Separate proposed changes from
confirmed changes. Give one worker ownership of each file or record.

Parallel coding tasks need deliberately isolated worktrees or disjoint paths;
separate conversations alone do not ensure isolated files, ports or accounts.
The coordinator owns integration and final checks.

## Authority is specific

| Mode | Work permitted by the task brief |
|---|---|
| Investigate | Read, diagnose and recommend |
| Prepare | Create drafts, local changes or a reviewable PR |
| Execute within scope | Perform particular writes the user authorized and tools permit |
| Recurring execution | Repeat a saved task within documented authority and stop conditions |

These are working agreements, not product switches. Delegation passes the same
or narrower authority. A worker cannot grant itself merge, send, spend or deploy
rights. No mode bypasses workspace controls.

Proceed with authorized reversible work. If approval is needed, complete the
preparation and present the exact action, target, impact and evidence. Do not ask
again unless scope changed or a control requires it.

## Keep the feedback loop short

Ask for results, evidence and blockers. Correct the objective and sources when a
task drifts. Poll only when there is a real running operation to check; cap the
duration and stop monitoring on completion.

Persist accepted lessons as concise, versioned instructions. A corrected chat
does not prove every other agent has learned the correction. Later tasks should
read the current playbook and record its version when consistency matters.

Parallel workers consume resources and require review. Bound concurrency,
deduplicate results and collect one final status. See
[usage economics](../reference/ECONOMICS.md) and
[product context](../reference/PRODUCT.md). The team structure here is a
recommended operating practice.
