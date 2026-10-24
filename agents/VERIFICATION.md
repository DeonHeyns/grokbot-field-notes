# Verification

Define what proves the outcome before starting. Check the actual artifact or
system state, not the confidence of the final message.

## Evidence by task

| Task | Evidence |
|---|---|
| Research | Accessible sources, dates, claim-to-source match, explicit gaps |
| Analysis | Reconciled totals, units, population, date window and formulas |
| Document or presentation | Render inspected for clipping, legibility and facts |
| Code fix | Reproduced failure, relevant passing tests, exercised behavior |
| Performance improvement | Comparable before/after measurements |
| App update | Correct target ID, confirmed tool result, read-back if supported |
| Scheduled workflow | Saved configuration, completed run, artifact and delivery |
| Documentation | Valid links, runnable examples, current claims, generated files |

An unrun check is **not run**, not passed. Report the reason and remaining
uncertainty. Complete the checks the environment supports.

## Coding workflow

1. Inspect the task, applicable AGENTS.md and working tree.
2. Reproduce the bug when possible; identify relevant existing tests.
3. Make the smallest coherent fix and exercise the affected path.
4. Run required checks. For UI, inspect the actual rendered result.
5. Attach evidence, residual risks and the PR or artifact link.

Use project-specific commands. These names are **illustrative**, not commands
supplied by ChatGPT or this repository:

```text
./verify seed
./verify run checkout
./verify screenshot checkout
./verify check
```

Verification scripts need deterministic setup, stable output, nonzero failure
exit codes and safe repeat behavior. Test writes against disposable data.
Read-only production observation must not turn into a write test.

## Describe important flows

```yaml
checkout:
  path: /checkout
  environment: disposable-test
  flows:
    - successful purchase
    - declined payment
    - double submission
  invariants:
    - total equals sum of line items
    - retry does not create a duplicate order
```

Keep the feature map with the application. It is not a substitute for tests.
A screenshot proves appearance at one moment, not all interactions or permissions.

## Review remains a distinct step

Check the diff and evidence. A second agent can review when available and
authorized, but agreement is not ground truth. Green checks do not authorize
merging, deploying, moving money or changing access. Use the
[PR template](../.github/PULL_REQUEST_TEMPLATE.md) to make that separation visible.

For reusable workflows, retain representative cases: normal input, missing
source, conflicting sources, denied permission, hostile instructions in a
document, duplicate event and transient failure. Recheck relevant cases after
changing instructions or tools. Record concise decision summaries and evidence,
not private internal reasoning.
