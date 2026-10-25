# Sales

Turn current account evidence into useful call preparation, tailored drafts and accurate CRM updates.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Account context](../roster/account-specialist.md) | account-specialist |
| [Meeting brief](../roster/call-prep.md) | call-prep |
| [Outreach draft](../roster/prospector.md) | prospector |
| [Writing style](../roster/voice.md) | voice |
| [Record update](../roster/crm-updater.md) | crm-updater |

## Inputs and access

Account IDs, CRM records, authorized business-contact sources, meeting transcript, current product facts and selected writing samples.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Confirm account and contact identity, stage and existing relationship before drafting.

2. Collect relevant prior commitments, usage evidence and public context. Do not manufacture personal hooks.

3. Prepare a short call brief or outreach draft in the approved style with a clear reason for contacting this person.

4. After the call, extract actual outcomes and agreed next steps from the transcript.

5. Show the CRM field changes and draft follow-up. Apply writes or send only within explicitly authorized scope.

## First-run prompt

```text
For account {ACCOUNT ID}, read {CRM RECORD}, {LAST CALL} and
{CURRENT USAGE}. Prepare a one-page call brief and a follow-up draft.
Then propose next-step updates for {CRM FIELDS}, with the evidence for
each. Keep outreach as drafts and show CRM changes before applying them.
```

## Proof of completion

Correct recipient/account identity, source-backed claims, no invented commitments, and read-back of any authorized CRM write.

## Recurring work

A saved daily preparation task can use an available calendar source. Do not assume a calendar connection schedules a separate alert before every meeting. Check tool support and actual delivery.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
