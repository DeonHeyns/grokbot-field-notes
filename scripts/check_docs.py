#!/usr/bin/env python3
"""Check Markdown fences, relative links, role coverage and playbook count."""

from pathlib import Path
import re
import sys
from urllib.parse import unquote, urlsplit

ROOT = Path(__file__).resolve().parents[1]


def prose_lines(text):
    fence = None
    for number, line in enumerate(text.splitlines(), 1):
        match = re.match(r"^\s*(`{3,}|~{3,})", line)
        if match:
            token = match.group(1)
            if fence is None:
                fence = token
            elif token[0] == fence[0] and len(token) >= len(fence):
                fence = None
            continue
        if fence is None:
            yield number, line
    if fence:
        raise ValueError("unclosed code fence")


def local_link_errors(path, text, root):
    errors = []
    try:
        for number, line in prose_lines(text):
            for match in re.finditer(r"!?\[[^\]]*\]\(([^)]+)\)", line):
                target = match.group(1).strip().strip("<>")
                # This pack uses inline links without optional title strings.
                parsed = urlsplit(target)
                if parsed.scheme or parsed.netloc or not parsed.path:
                    continue
                resolved = (path.parent / unquote(parsed.path)).resolve()
                if not resolved.is_relative_to(root.resolve()) or not resolved.exists():
                    errors.append(f"{path.relative_to(root)}:{number}: missing local target {target}")
    except ValueError as exc:
        errors.append(f"{path.relative_to(root)}: {exc}")
    return errors


def main():
    errors = []
    paths = sorted(p for p in ROOT.rglob("*.md") if ".git" not in p.parts)
    for path in paths:
        errors.extend(local_link_errors(path, path.read_text(encoding="utf-8"), ROOT))
    roles = {p.stem for p in (ROOT / "roster").glob("*.md") if p.name != "README.md"}
    if len(roles) != 69:
        errors.append(f"Expected 69 role cards, found {len(roles)}; update documentation deliberately")
    playbooks = [p for p in (ROOT / "playbooks").glob("*.md") if p.name != "README.md"]
    if len(playbooks) != 9:
        errors.append(f"Expected 9 playbooks, found {len(playbooks)}")
    index = (ROOT / "roster/README.md").read_text(encoding="utf-8")
    for role in sorted(roles):
        if f"]({role}.md)" not in index:
            errors.append(f"Roster index missing {role}")
    if errors:
        print("\n".join(errors), file=sys.stderr)
        return 1
    print(f"Verified local links and fences in {len(paths)} Markdown files; 69 roles and 9 playbooks")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
