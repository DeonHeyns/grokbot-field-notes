# Sales Development

Produce a small, verified and ranked prospect batch with useful outreach drafts.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Target definition](../roster/icp-researcher.md) | icp-researcher |
| [External signals](../roster/signal-scanner.md) | signal-scanner |
| [Product signals](../roster/usage-signals.md) | usage-signals |
| [Business contacts](../roster/enrichment.md) | enrichment |
| [Ranked drafts](../roster/prospector.md) | prospector |
| [Customer evidence](../roster/voice-of-customer.md) | voice-of-customer |

## Inputs and access

Approved ICP document, CRM and suppression list, business-contact research sources, usage exports and a defined batch/date window.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Read the current ICP and suppression rules. Select a bounded batch with stable account IDs.

2. Collect external signals and approved product usage evidence. Record dates and distinguish no change from missing access.

3. Enrich business contact details using available approved tools. Label verification status; exclude unverified addresses from send-ready output.

4. Rank by documented fit and evidence. Draft concise messages using actual public or authorized context.

5. Review the batch, deduplicate against prior work and return drafts. Sending or sequencing needs its own authorized scope.

## First-run prompt

```text
Research at most {N} accounts from {LIST} against {ICP DOCUMENT}.
Use {SUPPRESSION LIST} and {LOOKUP BUDGET}. Return account, business
contact, fit evidence, recent signal, verification status, source and
rank. Draft outreach for the top {K}. Do not send or enroll contacts.
If using workers is authorized and available, give each disjoint IDs;
otherwise process sequentially.
```

## Proof of completion

Each row has an account ID, dated sources and explicit contact-verification status. Rankings explain fit. Drafts contain no fabricated observations.

## Recurring work

After a manual trial, save a bounded daily or weekly task. Persist processed IDs, include a lookback overlap for late updates and avoid duplicate sequences. More workers do not remove lookup costs.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
