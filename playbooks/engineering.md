# Engineering

Turn a reported issue or approved specification into a verified pull request.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Coordinator](../roster/engineering-manager.md) | engineering-manager |
| [Implementation](../roster/domain-engineer.md) | domain-engineer |
| [Behavior checks](../roster/playtester.md) | playtester |
| [Review](../roster/pr-reviewer.md) | pr-reviewer |
| [Standards](../roster/playbook-owner.md) | playbook-owner |

## Inputs and access

Repository and AGENTS.md, issue or spec, build/test commands, disposable test data, current CI result and the team's merge policy.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Read the issue, repository instructions and current revision. Define the reproduction or acceptance criteria.

2. Use one Codex task for a small change. For larger independent work, delegate only if authorized and supported; assign disjoint files or configured worktrees.

3. Implement on a branch, run relevant checks and exercise the changed behavior. Keep unrun checks visible.

4. Review the diff and proof against the original task. Open a PR with the evidence and any remaining gaps.

5. Merge and deploy only under explicit authority and repository controls. Verify the released behavior if release is part of the request.

## First-run prompt

```text
Fix {ISSUE} in {REPO} using Codex and the repository's instructions.
Reproduce the failure where possible, implement a scoped fix on a branch,
run {CHECKS}, and inspect the affected behavior. Open a PR with proof.
Leave merge and deployment for review. If access or reproduction is blocked,
complete the useful investigation and state the exact limitation.
```

## Proof of completion

A source-linked reproduction, meaningful checks at the current commit, and a reviewable diff. UI work includes captured visual evidence. Performance claims include comparable measurements.

## Recurring work

Optional CI event handling or bounded overnight audits require supported triggers or saved schedules. Use incident severity policy for escalation; never delay a critical page because an agent wants another repair attempt.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
