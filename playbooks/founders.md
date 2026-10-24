# Founders

Protect focus by combining customer preparation, product changes and market evidence into a short decision brief.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Coordination](../roster/chief-of-staff.md) | chief-of-staff |
| [Customer context](../roster/call-prep.md) | call-prep |
| [Release reality](../roster/product-changes-tracker.md) | product-changes-tracker |
| [Market changes](../roster/competitive-intel.md) | competitive-intel |
| [Selected fixes](../roster/feedback-to-pr.md) | feedback-to-pr |

## Inputs and access

Current priorities, calendar or meeting list, customer notes, repository/release records, product access and a bounded competitor list.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Choose one outcome that consumes attention today. Start with one role rather than a full staff.

2. Collect the current customer commitments, verified product changes and material market evidence.

3. Return a concise brief ranked by decision urgency, with source links and unresolved gaps.

4. Select which feedback deserves implementation; hand only approved work to the coding workflow.

5. Save accepted decisions and useful operating lessons in a current document. Review the first recurring runs before expanding scope.

## First-run prompt

```text
Prepare my operating brief from {PRIORITIES}, {CUSTOMER RECORDS} and
{RELEASE SOURCES}. Show decisions needed, commitments due, material risks
and the three priorities that should lead the week. Cite each item.
Prepare drafts or task briefs for next actions. Do not send messages,
create accounts or launch recurring work in this first run.
```

## Proof of completion

The brief matches current sources, distinguishes shipped from planned work and makes each proposed next action concrete.

## Recurring work

Daily or weekly briefs and occasional competitor checks are candidates for saved tasks. Use approved sign-in and documented integrations, never copied cookies. Monitor review time as well as tool usage.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
