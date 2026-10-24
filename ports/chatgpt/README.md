# Use the pack with ChatGPT

Start with one role and a bounded task. You do not need to build an API service
or create a fleet of agents to use these instructions.

## Five-minute first run

1. Choose ChatGPT Work for a task with a finished deliverable.
2. Open a generated prompt, such as [Chief of Staff](roles/chief-of-staff.md),
   [Daily Digest](roles/daily-digest.md) or [PR Reviewer](roles/pr-reviewer.md).
3. Copy the **whole instruction block** into your conversation. For recurring
   work in a project, put the durable rules in its project instructions and
   keep individual task requests in separate chats.
4. Fill placeholders with real source links, identifiers, boundaries and output
   requirements. Attach or connect the sources. Verify the required operations
   are available; do not assume a named app is installed or writable.
5. Run one example and inspect the output against the acceptance criteria.

If a field limit prevents pasting a full pack, attach the file and explicitly
ask ChatGPT to read it for the task. Keep the shared boundaries and role scope
in the active instructions. Ask it to report missing access before claiming it
can perform an action.

Find all 69 roles in the [roster](../../roster/README.md). Start with one to three,
not all 69. These Markdown prompts are not a custom GPT import file, a plugin,
an installed skill, a saved automation or a multi-agent runtime.

## First task example

After copying the Chief of Staff pack:

```text
For this run, use only the attached project plan and issue export.
Prepare my weekly operating brief: decisions needed, commitments due, material
risks and the three priorities for the week. Cite the source row or issue for
each item. Flag stale or missing evidence. Keep it under one page.
Do this yourself in this conversation. Do not send messages or create a schedule.
```

## Add continuity deliberately

Keep instructions and accepted decisions in current source documents. A project
helps organize context; it does not turn every role into an independently running
worker. For an ongoing dot responsibility, use the same role contract and verify
its actual sources and tools. Use [handoffs](HANDOFF.md) between tasks.

After a verified manual run, use [the automation brief](AUTOMATION.md) to request
a real schedule. Confirm it was saved and review its first execution.

## Coding tasks

Use Codex with the target repository and its applicable AGENTS.md. Give it a
specific task and, if helpful, one role prompt. Merge useful rules from this
pack into the target's instructions without overwriting its build or test rules.
Opening a normal ChatGPT conversation does not automatically load a repository's
AGENTS.md.

## Share or maintain

Share reviewed instructions and necessary source material. Remove private
examples before sharing; recipients connect their own approved accounts and
verify their own permissions. Do not assume memory, credentials or schedules
transfer with a prompt.

Generated files live in `roles/`. Edit `roster/*.md` or [BASE.md](BASE.md), then:

```bash
python3 scripts/build_ports.py
python3 scripts/build_ports.py --check
```

Run from the repository root. See [product mapping](../../reference/PRODUCT.md)
for official documentation and the last review date.
