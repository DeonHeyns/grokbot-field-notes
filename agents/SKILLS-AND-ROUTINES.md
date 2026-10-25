# Skills and scheduled tasks

A skill describes **how** to do a job. A scheduled task determines **when** it
runs. A role describes **who owns the outcome**. Keep these separate.

First run the workflow manually. Verify a normal case, a missing-input case and
a failure case. Then save the reusable instructions.

## Make the workflow reusable

Write the trigger, inputs, authoritative sources, steps, output, verification and
boundaries. Store changing facts in a canonical document; put the method for
using those facts in the workflow. Save accepted corrections as principles.

ChatGPT and Codex support skills, subject to the available surface. A skill uses
a `SKILL.md` with `name` and `description` metadata and may include resources or
scripts. Local Codex skills can live under `.agents/skills`; distribution across
ChatGPT surfaces uses supported plugin packaging. Follow the current
[official skill guide](https://learn.chatgpt.com/docs/build-skills).

This repository's role files are **prompt templates**, not installed skills.
Use them directly first. If packaging a skill, use your environment's skill
creator and verify the installed skill can be selected. Copying a Markdown file
into a project does not prove installation.

## Save a real schedule

After a successful manual run, ask ChatGPT to create the scheduled task using
[the automation brief](../ports/chatgpt/AUTOMATION.md). Supply the time zone,
sources, destination, failure handling and stop condition. Confirm the saved
task and next run, then inspect it in Scheduled.

"Every morning" in a role description is not a saved schedule. Connecting a
plugin does not start monitoring. Use supported event triggers only when exposed
by the service; otherwise use a supported schedule or a separate integration.

Choose whether each run uses an existing chat or starts independently.
Unattended work cannot rely on you being present to resolve an interactive
approval or login prompt. Record the blocker and leave the action pending.
See [official scheduling guidance](https://learn.chatgpt.com/docs/automations).

## Operating contract for recurring work

| Field | Example or rule |
|---|---|
| Inputs | Exact folders, queries, ticket IDs or feeds |
| Time | Weekdays 08:00, Europe/London; confirm daylight-saving behavior |
| State | Last successful checkpoint and processed item IDs |
| Output | One brief in this chat, with source links |
| No change | Suppress substantive alerts if supported; retain run status |
| Missing source | Report incomplete coverage, never call it "nothing new" |
| Retry | Bounded retries for transient errors; stop after the limit |
| Writes | Check for an existing result before retrying a side effect |
| Expiry | Completion, end date or defined failure limit |
| Owner | Human responsible for failures and scope changes |

Use an overlap window and stable IDs to catch late updates without duplicate
posts or changes. A failed run must not advance the successful checkpoint.

## Review the first runs

Check saved configuration, completed run, artifact and delivery. "Run completed"
is not enough. Review cadence and usefulness weekly. Prefer supported events
where timely reaction matters; daily or weekly checks often suffice. Fast
polling needs a time limit and a concrete reason.
