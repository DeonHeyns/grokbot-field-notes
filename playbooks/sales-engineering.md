# Sales Engineering

Prepare a defensible technical answer, demo and sales material for a specific customer.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Product facts](../roster/technical-expert.md) | technical-expert |
| [External evidence](../roster/competitive-intel.md) | competitive-intel |
| [Comparison](../roster/battle-card-writer.md) | battle-card-writer |
| [Demo flow](../roster/demo-scripter.md) | demo-scripter |
| [Slides](../roster/case-study-curator.md) | case-study-curator |

## Inputs and access

Customer requirements, current repository/docs and deployment version, approved case studies, competitor public sources and deck template.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Translate the customer question into concrete capability checks and identify the version being discussed.

2. Inspect code and current product documentation. Mark claims not verified in the deployed product.

3. Compare competitor evidence using the same criteria. Do not assume a separate reviewer has read the same sources.

4. Create a demo script with real click paths and a customer-safe explanation; keep internal details in a separate internal note.

5. Build the required slides from approved evidence and render them. Deliver the pack for review before sending.

## First-run prompt

```text
Prepare an SE pack for {CUSTOMER QUESTION}. Use {REPOS / DOCS},
{DEPLOYED VERSION} and {APPROVED CASE STUDIES}. Return the technical answer,
a customer-safe draft, a demo script and {N} slides in {TEMPLATE}.
Cite evidence and flag unshipped or unverified capabilities. Do not send.
```

## Proof of completion

Product claims have source locations and version context; demo steps are exercised when access permits; slide figures and quotations match their sources.

## Recurring work

Refresh battle cards when a supported source trigger fires, or on a modest saved cadence. Date every comparison and retire stale claims.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
