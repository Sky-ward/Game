from dataclasses import dataclass, field
from typing import List, Optional

from .enemy import Enemy
from .weapon import Weapon


@dataclass
class Player:
    x: int
    y: int
    hp: int = 100
    attack: int = 10
    defense: int = 5
    weapon: Optional[Weapon] = None
    inventory: List[Weapon] = field(default_factory=list)

    def move(self, dx: int, dy: int) -> None:
        """Move player by delta."""
        self.x += dx
        self.y += dy

    def take_damage(self, dmg: int) -> int:
        """Apply damage after defense and return actual damage dealt."""
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual

    def pick_up(self, weapon: Weapon) -> None:
        """Add a weapon to inventory and equip it."""
        self.inventory.append(weapon)
        self.weapon = weapon

    def attack_enemy(self, enemy: Enemy) -> int:
        """Attack an enemy using base attack plus equipped weapon."""
        base = self.attack
        if self.weapon:
            base += self.weapon.damage
        return enemy.take_damage(base)

    def is_alive(self) -> bool:
        return self.hp > 0
