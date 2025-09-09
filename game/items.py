"""Item definitions used by the game."""

from __future__ import annotations

from dataclasses import dataclass
from typing import TYPE_CHECKING

if TYPE_CHECKING:  # pragma: no cover - used only for typing
    from .player import Player


@dataclass
class Consumable:
    """An item that can be consumed to produce an effect on the player."""

    name: str
    heal: int = 0
    damage: int = 0

    def use(self, player: Player) -> None:
        """Apply the consumable's effect to ``player``."""
        if self.heal:
            player.heal(self.heal)
        if self.damage:
            player.take_damage(self.damage)

