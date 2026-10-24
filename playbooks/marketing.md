# Marketing

Carry a campaign from source-backed positioning to reviewed assets and an evidence-based experiment readout.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Research](../roster/market-researcher.md) | market-researcher |
| [Positioning and copy](../roster/product-marketer.md) | product-marketer |
| [Page implementation](../roster/website-ops.md) | website-ops |
| [Paused campaign](../roster/performance-marketer.md) | performance-marketer |
| [Results](../roster/marketing-analyst.md) | marketing-analyst |
| [Coordination](../roster/project-manager.md) | project-manager |

## Inputs and access

Approved product claims, audience brief, brand assets, current research, campaign data and explicitly scoped platform access.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Research the market and customer problem, then draft a positioning brief with evidence and assumptions.

2. Create copy variants with a distinct hypothesis for each. Review product claims and landing-page destinations.

3. Build a page preview or PR. Inspect desktop/mobile behavior and render the assets.

4. Prepare a campaign plan, or create a paused draft only if the tools and authority support it. Verify it cannot spend.

5. Publish or launch only with specific authorization. Later, reconcile experiment data and recommend changes; do not invent a winner from weak evidence.

## First-run prompt

```text
Build a reviewable campaign pack for {PRODUCT / AUDIENCE} using
{APPROVED CLAIMS}, {BRAND} and {RESEARCH}. Return positioning, three
copy hypotheses, a landing-page outline or preview, and a campaign
plan with measurement criteria. Do not publish, launch ads or spend.
If a paused draft is specifically authorized, verify its saved state
and return its ID; otherwise keep the plan as a document.
```

## Proof of completion

Claims match sources, assets render correctly, URLs and tracking assumptions are checked, and draft/paused/live status is explicit. Experiment conclusions show sample and attribution limits.

## Recurring work

Save a weekly readout after validating the analysis. Campaign updates, budget changes and launches stay separate from reporting. Share reusable instructions and assets, not assumed memory or credentials.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
