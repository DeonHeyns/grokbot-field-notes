#!/usr/bin/env python3
"""Compatibility entry point; run from any working directory."""
from pathlib import Path
import sys

sys.path.insert(0, str(Path(__file__).resolve().parents[2] / "scripts"))
from build_ports import main

if __name__ == "__main__":
    raise SystemExit(main(["--target", "cursor", *sys.argv[1:]]))
