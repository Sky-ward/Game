
"""Enemy entity with minimal AI for moving and attacking."""
from __future__ import annotations

from dataclasses import dataclass
from typing import Optional, TYPE_CHECKING

if TYPE_CHECKING:  # pragma: no cover
    from .level import Level
    from .player import Player


from dataclasses import dataclass


@dataclass
class Enemy:
    x: int
    y: int
    hp: int = 20
    attack: int = 5
    defense: int = 2

    def take_damage(self, dmg: int) -> int:
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual


    def attack_player(self, player: Player) -> int:
        """Inflict damage on the player."""
        return player.take_damage(self.attack)

    def move_towards(self, player: Player, level: Optional[Level] = None) -> None:
        """Move one step towards the player and optionally clamp to bounds."""
        dx = 1 if player.x > self.x else -1 if player.x < self.x else 0
        dy = 1 if player.y > self.y else -1 if player.y < self.y else 0
        nx, ny = self.x + dx, self.y + dy
        if level:
            nx, ny = level.clamp_position(nx, ny)
        self.x, self.y = nx, ny


    def is_alive(self) -> bool:
        return self.hp > 0
