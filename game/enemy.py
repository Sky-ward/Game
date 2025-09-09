
"""Enemy entity with basic AI and specialized subclasses."""
from __future__ import annotations

from dataclasses import dataclass
from typing import Optional, TYPE_CHECKING

if TYPE_CHECKING:  # pragma: no cover
    from .level import Level
    from .player import Player


@dataclass
class Enemy:
    x: int
    y: int
    hp: int = 20
    attack: int = 5
    defense: int = 2
    state: str = "idle"

    def take_damage(self, dmg: int) -> int:
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual

    def distance_to(self, player: Player) -> int:
        """Manhattan distance to the player."""
        return abs(player.x - self.x) + abs(player.y - self.y)

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


class MeleeEnemy(Enemy):
    """Enemy that rushes the player and attacks in melee."""

    def take_turn(self, player: Player, level: Optional[Level] = None) -> int:
        if self.distance_to(player) <= 1:
            self.state = "attacking"
            return self.attack_player(player)
        self.state = "pursuing"
        self.move_towards(player, level)
        return 0


class RangedEnemy(Enemy):
    """Enemy that shoots the player from a distance."""

    range: int = 3

    def projectile_attack(self, player: Player) -> int:
        return player.take_damage(self.attack)

    def take_turn(self, player: Player, level: Optional[Level] = None) -> int:
        if self.distance_to(player) <= self.range:
            self.state = "shooting"
            return self.projectile_attack(player)
        self.state = "positioning"
        self.move_towards(player, level)
        return 0


class HeavyEnemy(Enemy):
    """Slow moving enemy with a powerful strike."""

    cooldown: int = 0

    def move_towards(self, player: Player, level: Optional[Level] = None) -> None:
        """Move only along one axis per turn to represent slowness."""
        dx = 1 if player.x > self.x else -1 if player.x < self.x else 0
        dy = 0
        if dx == 0:
            dy = 1 if player.y > self.y else -1 if player.y < self.y else 0
        nx, ny = self.x + dx, self.y + dy
        if level:
            nx, ny = level.clamp_position(nx, ny)
        self.x, self.y = nx, ny

    def heavy_strike(self, player: Player) -> int:
        return player.take_damage(self.attack * 2)

    def take_turn(self, player: Player, level: Optional[Level] = None) -> int:
        if self.cooldown > 0:
            self.cooldown -= 1
            self.state = "recovering"
            return 0
        if self.distance_to(player) <= 1:
            self.cooldown = 1
            self.state = "striking"
            return self.heavy_strike(player)
        self.state = "pursuing"
        self.move_towards(player, level)
        return 0
