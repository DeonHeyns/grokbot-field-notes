# Post-Sales and Account Operations

Keep account plans and commitments current, with reviewable follow-ups after customer conversations.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Consolidation](../roster/chief-of-staff.md) | chief-of-staff |
| [Account plan](../roster/account-specialist.md) | account-specialist |
| [Transcript analysis](../roster/meeting-attendee.md) | meeting-attendee |
| [Draft pack](../roster/follow-up-desk.md) | follow-up-desk |
| [Open commitments](../roster/commitment-tracker.md) | commitment-tracker |
| [Verified policy](../roster/source-of-truth.md) | source-of-truth |

## Inputs and access

Current account plan, meeting transcript, authorized customer threads, internal product documentation and commitment ledger.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Read the actual meeting record. An invitation does not prove attendance, recording or access to the discussion.

2. Extract decisions, promises, owners and due dates. Mark ambiguous attribution rather than inventing it.

3. Update the authorized account-plan fields or prepare the exact proposed changes.

4. Draft customer follow-ups and promised materials, checked against product facts and the approved writing style.

5. Record open commitments and return one consolidated pack. Route outbound messages only under explicit authorization.

## First-run prompt

```text
Prepare the post-call pack for {ACCOUNT} from {TRANSCRIPT} and
{ACCOUNT PLAN}. Return decisions, commitments with owners/dates,
proposed account-plan edits, a customer email draft and promised
materials. Cite the meeting record and flag uncertainty.
Do not join a live meeting or send messages. Update only {AUTHORIZED
LEDGER FIELDS}; leave other changes as proposals.
```

## Proof of completion

Commitments trace to the source record, account updates are accurate, attachments open correctly and all sends remain clearly distinguished from drafts.

## Recurring work

A saved daily commitment check can flag overdue items. Confirm completion from current sources before nudging. Weekly workflow review should propose one useful improvement rather than an expanding roster.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
