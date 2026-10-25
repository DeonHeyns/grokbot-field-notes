# Task prompts for ChatGPT

These are adaptations, not stream quotations. Start with a complete
[role prompt](../ports/chatgpt/README.md), then add a task brief.
Replace placeholders. Never paste passwords, tokens or session cookies.

## Give a clear outcome

```text
Create {DELIVERABLE} for {AUDIENCE} using {SOURCES}.
It is complete when {ACCEPTANCE CRITERIA}.
You may {AUTHORIZED ACTIONS}. Bring me {DECISIONS THAT NEED ME}.
Use available tools, state material access gaps, and proceed with reasonable
assumptions for reversible details. Finish with evidence and the artifact link.
```

## Structure a voice dump

```text
Organize these notes into the goal, facts, decisions, questions and next actions.
Keep links to the original evidence and flag contradictions. Then complete
{FIRST OUTCOME}. Ask only about missing information that materially changes the
result; continue independent work while it is unresolved.
```

## Investigate without changing anything

```text
Investigate {SYMPTOM} in {SYSTEM / REPO}. Evidence: {LINKS / LOGS}.
Read-only investigation: do not change code, open a PR or modify production.
Return the likely cause, evidence, competing explanations and the next check
that would distinguish them.
```

## Fix a repository issue

```text
Fix {ISSUE} in {REPO}. Reproduce it first if the environment allows, implement
the fix on a branch, and run the relevant checks from AGENTS.md. Open a PR with
the reproduction, proof and remaining gaps. Leave merge and deploy for review.
```

## Coordinate bounded parallel work

```text
Produce {OUTCOME}. You may delegate these independent parts: {TASKS}.
Use at most {N} workers if delegation tools are available. Give each a complete
handoff, acceptance criteria and disjoint file ownership. Review every result
against the original sources and synthesize one answer. If delegation is
unavailable, work sequentially and say so.
```

## Research with a recommendation

```text
Compare {OPTIONS} against {CRITERIA} for {DECISION}. Use current primary sources,
record dates, distinguish facts from inferences and show material gaps.
Finish with a recommendation, the strongest objection and the evidence that
would change it. Do not invent prices, access or capabilities.
```

## Schedule a tested workflow

```text
The manual run met the criteria. Create a scheduled task repeating {WORKFLOW}
every {CADENCE} in {IANA TIME ZONE}, through {END DATE / STOP RULE}.
Use {SOURCES} and deliver to {DESTINATION}. Report access failures separately
from no-change results. Deduplicate with {CHECKPOINT LOCATION}.
Do not send to anyone else. Confirm the saved task and next run.
```

## Carry a correction forward

```text
The result should have {CORRECTION}. Fix it. Propose the general rule that
would prevent recurrence, then update {INSTRUCTION FILE} if within this task.
Show the change. Do not assume other chats received it; include the updated
source in future handoffs.
```

## Restart with explicit context

```text
Continue {TASK ID}. Read {CURRENT PLAN}, {ARTIFACT} and {DECISION LOG}.
Verify the last completed step against the artifact. Resume from {NEXT STEP},
preserving {CONSTRAINTS}. Current source files are authoritative; old summaries
are context. Report contradictions before relying on them.
```
