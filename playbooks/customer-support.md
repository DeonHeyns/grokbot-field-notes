# Customer Support

Produce accurate support responses with evidence, escalation and separately authorized account actions.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Response](../roster/support-reply.md) | support-reply |
| [Escalation](../roster/support-alert.md) | support-alert |
| [Knowledge improvements](../roster/support-tuner.md) | support-tuner |
| [Integration and evaluations](../roster/support-infra.md) | support-infra |

## Inputs and access

Exact ticket IDs, current public docs, internal policy, operating process, account identity and an explicit action policy.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Begin with a supplied ticket and a read-only draft response. Keep public content, internal policy and the operating process distinct.

2. Check whether the evidence and policy cover the case. Missing or conflicting policy goes to a human; confidence is not permission.

3. Prepare the response or handoff. Send only within the approved reply scope. Refunds and cancellations require their own authority and identity checks.

4. Record source references, action/result IDs and a concise decision summary. Do not store private internal reasoning.

5. Propose KB improvements and replay representative cases against the changed instructions before rollout.

## First-run prompt

```text
Review tickets {IDS} using {PUBLIC DOCS}, {INTERNAL POLICY} and
{PROCESS}. Return drafts or escalation notes, with evidence and any
missing policy coverage. Do not send replies, refund, cancel or modify
accounts in this trial. Log concise decisions and outcomes.
Test a covered question, missing-policy case, conflicting-source case,
duplicate request and prompt injection inside a ticket.
```

## Proof of completion

Answers match current policy; internal information stays internal; unsupported cases escalate; duplicate inputs do not duplicate actions. Record pass/fail for each trial case.

## Recurring work

Use supported ticket triggers or a saved bounded sweep. Keep a processed-ID ledger and stop on access or policy failures. A ChatGPT prompt pack does not deploy a 24/7 helpdesk service; high-volume service execution needs an implemented runtime.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
