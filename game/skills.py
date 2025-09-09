from __future__ import annotations

"""Skill system with temporary effects applied to the player."""

from dataclasses import dataclass
from typing import Tuple


@dataclass
class Skill:
    """Base skill effect with a limited duration."""
    duration: int

    def on_combat(self, player, damage: int) -> int:
        """Modify outgoing combat damage."""
        return damage

    def on_move(self, player, dx: int, dy: int) -> Tuple[int, int]:
        """Modify movement delta."""
        return dx, dy

    def tick(self) -> bool:
        """Decrease duration and return True if expired."""
        self.duration -= 1
        return self.duration <= 0


@dataclass
class FireDamage(Skill):
    """Adds bonus fire damage on attack."""
    bonus: int = 0

    def on_combat(self, player, damage: int) -> int:
        return damage + self.bonus


@dataclass
class SpeedBoost(Skill):
    """Multiplies movement speed."""
    multiplier: int = 1

    def on_move(self, player, dx: int, dy: int) -> Tuple[int, int]:
        return dx * self.multiplier, dy * self.multiplier
