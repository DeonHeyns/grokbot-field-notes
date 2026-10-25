# Optional Cursor port

The 69 role cards are also exported as `.mdc` rules. This is a compatibility
port; [ChatGPT](../chatgpt/README.md) is the primary setup for this adaptation.

Each generated rule contains shared operating boundaries, the role assignment,
ownership, sources, approval rules, triggers and outputs. Keeping these fields
together avoids losing important context when copying one rule.

Copy only the rules you need into your project's `.cursor/rules/` directory.
They declare `alwaysApply: false`; inspect your installed Cursor version's rule
selection behavior rather than assuming copying a file invokes it. Adapt existing
project guidance without overwriting it.

From the repository root:

```bash
python3 scripts/build_ports.py
python3 scripts/build_ports.py --check
```

The existing entry point remains available:

```bash
python3 ports/cursor/build.py
python3 ports/cursor/build.py --check
```

Edit `roster/*.md` and `ports/chatgpt/BASE.md`, not generated rules. No connector,
worker or scheduled task is installed by these files.
