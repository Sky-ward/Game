
"""Player entity with basic movement, inventory and combat helpers."""
from __future__ import annotations

from dataclasses import dataclass, field
from typing import List, Optional, TYPE_CHECKING

from .enemy import Enemy
from .weapon import Weapon

if TYPE_CHECKING:  # pragma: no cover - for type checking only
    from .level import Level


@dataclass
class Player:
    x: int
    y: int

    max_hp: int = 100
    hp: int = 100
    attack: int = 10
    defense: int = 5
    weapon: Optional[Weapon] = None
    inventory: List[Weapon] = field(default_factory=list)

    def move(self, dx: int, dy: int, level: Optional["Level"] = None) -> None:
        """Move player by delta and optionally clamp to level bounds."""
        nx, ny = self.x + dx, self.y + dy
        if level:
            nx, ny = level.clamp_position(nx, ny)
        self.x, self.y = nx, ny

    def take_damage(self, dmg: int) -> int:
        """Apply damage after defense and return actual damage dealt."""
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual

    def pick_up(self, weapon: Weapon) -> None:
        """Add a weapon to inventory without equipping."""
        if weapon not in self.inventory:
            self.inventory.append(weapon)

    def equip(self, weapon: Weapon) -> None:
        """Equip a weapon from inventory."""
        if weapon in self.inventory:
            self.weapon = weapon

    def heal(self, amount: int) -> None:
        """Restore hit points up to maximum."""
        self.hp = min(self.max_hp, self.hp + amount)

    def attack_enemy(self, enemy: Enemy) -> int:
        """Attack an enemy using base attack plus equipped weapon."""
        base = self.attack
        if self.weapon:
            base += self.weapon.damage
        return enemy.take_damage(base)

    def is_alive(self) -> bool:
        return self.hp > 0
