"""Heads-up display rendering utilities."""
from __future__ import annotations

from typing import Optional

from ..player import Player


def draw_health_bar(player: Player, width: int = 10) -> str:
    """Return a simple text health bar for a player.

    The bar uses ``#`` to represent current health and ``-`` for missing health
    based on ``width`` segments.
    """
    ratio = player.hp / player.max_hp if player.max_hp else 0
    filled = int(width * ratio)
    empty = width - filled
    bar = "#" * filled + "-" * empty
    return f"HP: [{bar}] {player.hp}/{player.max_hp}"


def draw_weapon_name(player: Player) -> str:
    """Return a string showing the currently equipped weapon name."""
    name: Optional[str] = None
    if player.weapon:
        name = player.weapon.name
    return f"Weapon: {name or 'None'}"
