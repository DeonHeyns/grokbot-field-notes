"""Build the portable CTO workflow pack from explicit source files."""
import argparse
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
TEAM = ROOT / "ports/chatgpt/cto-team"
SOURCES = ["00-chief-of-staff.md", "CONFIGURATION.md", "01-email.md",
           "02-meetings.md", "03-github.md", "04-industry-news.md",
           "05-technology-news.md", "06-ai-opportunities.md", "07-strategy.md",
           "08-project-management.md"]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--check", action="store_true")
    args = parser.parse_args()
    text = "# CTO team: complete portable instruction pack\n\n"
    text += "Generated from the individual files in this folder. Do not edit directly.\n"
    text += "Reference bundle: create each specialist separately, then the Chief of Staff Workspace Agent. Dots is not required. Loading instructions does not install agents or create schedules.\n\n"
    text += "\n\n---\n\n".join((TEAM / name).read_text() for name in SOURCES)
    target = TEAM / "TEAM-PACK.md"
    if args.check:
        if not target.exists() or target.read_text() != text:
            raise SystemExit("CTO team pack is missing or stale; run scripts/build_cto_pack.py")
        print("Verified CTO team pack: manager, configuration, eight specialists")
    else:
        target.write_text(text)
        print("Built CTO team pack")


if __name__ == "__main__":
    main()
