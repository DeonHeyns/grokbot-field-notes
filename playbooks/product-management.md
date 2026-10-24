# Product Management

Turn evidence into a testable product decision, specification and implementation handoff.

Adapted for ChatGPT Work and Codex. Start with one role; these responsibilities
can be stages in one task, not necessarily separate agents.

## Roles

| Responsibility | Role card |
|---|---|
| [Evidence](../roster/data-scientist.md) | data-scientist |
| [Requirements](../roster/spec-writer.md) | spec-writer |
| [Options](../roster/designer.md) | designer |
| [Review](../roster/critic.md) | critic |
| [Implementation handoff](../roster/engineering-manager.md) | engineering-manager |

## Inputs and access

Customer evidence, current product decisions, read-only usage data or exports, design system and product constraints.

Check the actual read/write operations available. Use the
[complete role prompts](../ports/chatgpt/README.md) and
[shared operating instructions](../ports/chatgpt/BASE.md). Missing access is a
reported limitation, not a reason to invent results.

## Workflow

1. Frame the decision and validate the data population, metric definitions and date window.

2. Identify the user problem and alternative explanations. Separate observed patterns from causal claims.

3. Draft a specification with testable P0/P1/P2 requirements, assumptions, exclusions and acceptance criteria.

4. Create design options using the actual design system. Review empty, loading, error and permission states.

5. After the product decision, hand the approved spec and assets to engineering with source versions. Do not represent a mock as a shipped feature.

## First-run prompt

```text
Using {CUSTOMER EVIDENCE} and {DATA}, investigate {PRODUCT PROBLEM}.
Return the evidence, competing explanations, recommended decision and a
short draft spec with testable acceptance criteria. Include two design
directions if useful. Flag decisions needed before engineering begins.
Do not change production or commit the roadmap.
```

## Proof of completion

Trace each material requirement to evidence or an explicitly labeled decision. Reconcile analysis, inspect mockups, and check acceptance criteria can be observed.

## Recurring work

A weekly product review can refresh the evidence after a successful trial. Save its actual schedule and state location. Re-read the current roadmap rather than assuming an old conversation is authoritative.

Use the [automation brief](../ports/chatgpt/AUTOMATION.md) only after the trial.
No schedule or integration is created by this document.

Product claims: [current mapping and sources](../reference/PRODUCT.md).
Original workshop narrative: [source revision and attribution](../PROVENANCE.md).
