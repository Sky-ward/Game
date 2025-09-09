from __future__ import annotations

from dataclasses import dataclass, field
from typing import List, Optional, Union, TYPE_CHECKING

from .enemy import Enemy
from .weapon import Weapon
from .items import Consumable
from .skills import Skill

if TYPE_CHECKING:  # pragma: no cover - used only for typing
    from .level import Level

Item = Union[Weapon, Consumable]


@dataclass
class Player:
    """Player controlled character."""

    x: int
    y: int

    max_hp: int = 100
    hp: int = 100
    attack: int = 10
    defense: int = 5
    weapon: Optional[Weapon] = None

    inventory: List[Item] = field(default_factory=list)
    skills: List[Skill] = field(default_factory=list)

    def move(self, dx: int, dy: int, level: Optional["Level"] = None) -> None:
        """Move the player applying movement skills and clamping to the level."""
        for skill in list(self.skills):
            dx, dy = skill.on_move(self, dx, dy)
            if skill.tick():
                self.skills.remove(skill)
        nx, ny = self.x + dx, self.y + dy
        if level:
            nx, ny = level.clamp_position(nx, ny)
        self.x, self.y = nx, ny

    def attack_enemy(self, enemy: Enemy) -> int:
        """Attack ``enemy`` applying weapon and combat skill bonuses."""
        dmg = self.attack
        if self.weapon:
            dmg += self.weapon.damage
        for skill in list(self.skills):
            dmg = skill.on_combat(self, dmg)
            if skill.tick():
                self.skills.remove(skill)
        return enemy.take_damage(dmg)

    def take_damage(self, dmg: int) -> int:
        """Apply incoming ``dmg`` after defense and return actual damage dealt."""
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual

    def heal(self, amount: int) -> None:
        """Restore hit points up to ``max_hp``."""
        self.hp = min(self.max_hp, self.hp + amount)

    def pick_up(self, item: Item) -> None:
        """Add an item to the inventory if it's not already present."""
        if item not in self.inventory:
            self.inventory.append(item)

    def equip(self, weapon: Weapon) -> None:
        """Equip a ``weapon`` that is already in the inventory."""
        if weapon in self.inventory:
            self.weapon = weapon

    def use_item(self, item: Item) -> None:
        """Use a consumable or equip a weapon from the inventory."""
        if item not in self.inventory:
            return
        if isinstance(item, Consumable):
            item.use(self)
            self.inventory.remove(item)
        elif isinstance(item, Weapon):
            self.equip(item)

    def is_alive(self) -> bool:
        """Return ``True`` if the player still has hit points."""
        return self.hp > 0
