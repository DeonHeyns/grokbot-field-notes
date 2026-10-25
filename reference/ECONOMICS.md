# Usage and operating cost

Measure the cost of a **verified outcome**, including review and rework.
Historical Grok costs are retained in [the source notes](../notes/grok-economics.md);
they are not ChatGPT prices or forecasts.

ChatGPT subscription limits and API usage are different accounting surfaces.
Use your actual account usage and the billing model of the chosen runtime.
This pack deliberately pins no model, price or plan quota.

## A practical budget

| Measure | Why it matters |
|---|---|
| Runs attempted and outcomes accepted | Detect retries and false completion |
| Tool calls and data scanned | Find expensive broad retrieval |
| Tokens or credits, when exposed | Compare work within the same billing surface |
| External service charges | Include enrichment, warehouse and paid API costs |
| Human review minutes | See whether delegation actually saves attention |
| Error, duplicate and escalation rates | Detect costs hidden by successful-looking outputs |
| Elapsed time | Check the workflow meets its deadline |

For an API workflow, estimate model usage plus tools, storage, external services
and hosting using current pricing. Add human review and rework to compare it
with the previous process. For a ChatGPT workflow, monitor its actual allowance
and usage rather than inventing a dollar-per-role conversion.

## Frequency is a multiplier

A 15-minute schedule runs **96 times per 24-hour day**. Three such schedules run
288 times, or 8,640 times in a 30-day period before retries. This arithmetic is
an illustration, not a claim about supported scheduling limits.

Start with the slowest cadence that meets the business need. Use supported
events when appropriate. Cap batch size and concurrency, checkpoint progress,
deduplicate and stop obsolete schedules. A quiet output can still consume work.

Prefer precise identifiers and incremental queries over rereading entire
mailboxes or repositories. Reuse tested scripts for deterministic calculations.
Use supported connectors where suitable. Parallelism and extra reviewers are
resources to budget, not free speed.

Review weekly: keep workflows that produce accepted outcomes, fix the noisy
ones, and remove or pause those nobody uses.
