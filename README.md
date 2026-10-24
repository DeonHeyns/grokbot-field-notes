# ChatGPT Agent Field Notes

Practical operating rules, 69 role cards, and nine workflows for doing real work
with **ChatGPT Work and Codex**. Adapted from the original Grok Bot field notes;
the original stream notes and PDFs remain available as historical sources.

This is an instruction pack, not a running agent platform. Copying a prompt does
not create a background worker, connect an account, grant permissions, or save a
schedule. Start with one role and one task you can verify.

## Start here

1. Open [the ChatGPT quickstart](ports/chatgpt/README.md).
2. Choose one [role](roster/README.md), for example
   [Chief of Staff](ports/chatgpt/roles/chief-of-staff.md),
   [Daily Digest](ports/chatgpt/roles/daily-digest.md), or
   [PR Reviewer](ports/chatgpt/roles/pr-reviewer.md).
3. Copy its complete instruction block into a ChatGPT Work conversation or
   project instructions. Fill the placeholders and attach its source material.
4. Run one real, bounded task. Check the result against the acceptance criteria.
5. Only then add a [saved schedule](agents/SKILLS-AND-ROUTINES.md) if it needs to repeat.

For coding work, use Codex with this repository's [AGENTS.md](AGENTS.md). When
adopting it in another repository, merge the relevant rules into the existing
file and replace the verification commands with that project's commands.
Do not overwrite an existing AGENTS.md with a download.

## Choose the right setup

| Need | Start with |
|---|---|
| Research, analysis, documents, operational work | ChatGPT Work, one role prompt, relevant files and plugins |
| Related tasks using the same sources and standards | A ChatGPT project with shared instructions and separate task chats |
| Ongoing responsibility with follow-up between conversations | Your dot, where available, with explicit sources and boundaries |
| Code changes, tests, repository review | Codex, AGENTS.md, a scoped branch and a verification command |
| A repeatable process | A tested workflow, then a skill and/or saved scheduled task |
| An application that controls its own agent execution | An OpenAI API runtime, implemented separately from this prompt pack |

Feature access varies by account, platform and workspace. See the dated
[product mapping and official sources](reference/PRODUCT.md). Named roles are
responsibilities, not a promise that ChatGPT creates 69 permanent agents.

## What's here

| Path | Purpose |
|---|---|
| [AGENTS.md](AGENTS.md) | Repository instructions for Codex and compatible coding agents |
| [agents/](agents/README.md) | Verification, coordination, prompts, skills and scheduling |
| [roster/](roster/README.md) | 69 adapted role cards: scope, sources, permissions, triggers and outputs |
| [playbooks/](playbooks/README.md) | Nine ChatGPT workflows with a first-run prompt and proof requirements |
| [ports/chatgpt/](ports/chatgpt/README.md) | Complete prompts generated from role cards and shared operating rules |
| [ports/cursor/](ports/cursor/README.md) | Optional generated Cursor rules from the same source |
| [ANTIPATTERNS.md](ANTIPATTERNS.md) | Common failures when applying these patterns to ChatGPT |
| [reference/](reference/PRODUCT.md) | Product mapping, migration decisions, sources and usage budgeting |
| [.github/PULL_REQUEST_TEMPLATE.md](.github/PULL_REQUEST_TEMPLATE.md) | Review form with evidence and explicit merge authority |
| [guide/](guide/README.md), [notes/](notes/README.md) | Original Grok PDFs and historical material, not ChatGPT instructions |

## Maintain this pack

Edit `roster/*.md` and `ports/chatgpt/BASE.md`, then regenerate both ports:

```bash
python3 scripts/build_ports.py
python3 scripts/build_ports.py --check
python3 scripts/check_docs.py
python3 -m unittest discover -s tests
```

Python 3.10+; no third-party packages or API keys required. These checks validate
documentation and generated prompts. They do not prove a connected account,
scheduled task or external workflow has been deployed or tested.

## Attribution

Based on [unicodef1wn/grokbot-field-notes](https://github.com/unicodef1wn/grokbot-field-notes).
The original MIT copyright notice is retained in [LICENSE](LICENSE).
See [provenance](PROVENANCE.md) for the source revision and the boundary between
historical observations and this adaptation. This is an independent adaptation,
not official OpenAI documentation or an endorsement.
