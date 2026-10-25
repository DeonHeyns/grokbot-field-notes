import json
from pathlib import Path
import sys
import tempfile
import unittest
from contextlib import redirect_stdout
from io import StringIO
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1] / "scripts"))
from build_ports import expected_outputs, parse_role, stale_paths
import build_ports
from check_docs import local_link_errors


ROLE = """# Reviewer: \"strict\"

**Category:** Engineering

Checks facts: uses evidence.

## Owns

Review.

## Does not own

Publishing.

## Source of truth

The exact revision.

## Needs approval for

Posting reviews.

## Triggers

A supplied PR.

## Outputs

Findings.

## Role description

```text
Review the supplied artifact.
## This heading belongs to the prompt
Never merge from green checks alone.
```
"""


class PortTests(unittest.TestCase):
    def test_check_detects_drift_without_rewriting_and_bad_source_blocks_build(self):
        with tempfile.TemporaryDirectory() as folder:
            root = Path(folder)
            (root / "roster").mkdir()
            (root / "ports/chatgpt").mkdir(parents=True)
            source = root / "roster/reviewer.md"
            source.write_text(ROLE)
            (root / "ports/chatgpt/BASE.md").write_text("Respect authorization.\n")
            with patch.object(build_ports, "ROOT", root), redirect_stdout(StringIO()):
                self.assertEqual(build_ports.main([]), 0)
                self.assertEqual(build_ports.main(["--check"]), 0)
                generated = root / "ports/chatgpt/roles/reviewer.md"
                generated.write_text("local edit")
                self.assertEqual(build_ports.main(["--check"]), 1)
                self.assertEqual(generated.read_text(), "local edit")
                source.write_text(ROLE.replace("## Needs approval for", "## Wrong"))
                with patch("sys.stderr", new=StringIO()):
                    self.assertEqual(build_ports.main([]), 1)
                self.assertEqual(generated.read_text(), "local edit")

    def test_prompt_heading_does_not_split_sections(self):
        role = parse_role(ROLE, "fixture.md")
        self.assertIn("## This heading belongs to the prompt", role["prompt"])
        self.assertEqual(role["sections"]["Needs approval for"], "Posting reviews.")

    def test_missing_boundaries_and_malformed_prompt_fail(self):
        for malformed in (
            ROLE.replace("## Needs approval for", "## Renamed boundary"),
            ROLE.replace("Posting reviews.", ""),
            ROLE.rsplit("```", 1)[0],
            ROLE + "\n## Outputs\nDuplicate\n",
        ):
            with self.subTest(malformed=malformed[-50:]), self.assertRaises(ValueError):
                parse_role(malformed, "fixture.md")

    def test_both_ports_preserve_authority_sources_and_valid_description(self):
        with tempfile.TemporaryDirectory() as folder:
            root = Path(folder)
            (root / "roster").mkdir()
            (root / "ports/chatgpt").mkdir(parents=True)
            (root / "roster/reviewer.md").write_text(ROLE)
            (root / "ports/chatgpt/BASE.md").write_text("Never invent access.\n")
            outputs = expected_outputs(root)
            self.assertEqual(len(outputs), 2)
            for text in outputs.values():
                for required in ("Never invent access.", "Posting reviews.", "The exact revision.", "Never merge"):
                    self.assertIn(required, text)
            cursor = outputs[root / "ports/cursor/rules/reviewer.mdc"]
            encoded = cursor.splitlines()[1].removeprefix("description: ")
            self.assertEqual(json.loads(encoded), 'Reviewer: "strict". Checks facts: uses evidence.')
            self.assertEqual(outputs, expected_outputs(root))
            extra = root / "ports/chatgpt/roles/retired.md"
            extra.parent.mkdir()
            extra.write_text("do not silently delete me")
            self.assertEqual(stale_paths(root, outputs, "all"), [extra])

    def test_links_ignore_fenced_examples_but_reject_missing_or_escaping_paths(self):
        with tempfile.TemporaryDirectory() as folder:
            root = Path(folder)
            doc = root / "README.md"
            (root / "exists.md").touch()
            self.assertEqual(local_link_errors(doc, "[ok](exists.md)\n````text\n[x](missing.md)\n````\n", root), [])
            self.assertEqual(len(local_link_errors(doc, "[bad](missing.md)\n[x](../outside.md)", root)), 2)
            self.assertEqual(len(local_link_errors(doc, "```text\nnot closed", root)), 1)


if __name__ == "__main__":
    unittest.main()
