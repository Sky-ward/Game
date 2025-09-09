"""Simple internationalization helper."""

from __future__ import annotations

import json
from functools import lru_cache
from pathlib import Path

# Path to the language files directory
LANG_DIR = Path(__file__).resolve().parent.parent / "assets" / "lang"


@lru_cache(maxsize=None)
def _load_lang(lang: str) -> dict:
    """Load a language JSON file and cache the result."""
    with (LANG_DIR / f"{lang}.json").open(encoding="utf-8") as f:
        return json.load(f)


def t(key: str, lang: str = "en") -> str:
    """Translate a key using the given language.

    If the key is missing, the key itself is returned.
    """
    data = _load_lang(lang)
    return data.get(key, key)

