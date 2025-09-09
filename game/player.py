
from __future__ import annotations

from dataclasses import dataclass, field
from typing import List, Optional, TYPE_CHECKING, Union

from .weapon import Weapon
from .items import Consumable
from .skills import Skill

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

    # Inventory can store both weapons and consumables.
    inventory: List[Item] = field(default_factory=list)

        for skill in list(self.skills):
            dx, dy = skill.on_move(self, dx, dy)
            if skill.tick():
                self.skills.remove(skill)

        nx, ny = self.x + dx, self.y + dy
        if level:
            nx, ny = level.clamp_position(nx, ny)
        self.x, self.y = nx, ny


    def take_damage(self, dmg: int) -> int:
        """Apply incoming ``dmg`` after defense and return actual damage dealt."""
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual

    def heal(self, amount: int) -> None:
        """Restore hit points up to ``max_hp``."""
        self.hp = min(self.max_hp, self.hp + amount)

    def attack_enemy(self, enemy: Enemy) -> int:
        """Attack an enemy using base attack, weapon damage and skills."""
        dmg = self.attack
        if self.weapon:
            dmg += self.weapon.damage
        for skill in list(self.skills):
            dmg = skill.on_combat(self, dmg)
            if skill.tick():
                self.skills.remove(skill)
        return enemy.take_damage(dmg)

    def is_alive(self) -> bool:
        """Return ``True`` if the player still has hit points."""
        return self.hp > 0

    # -- inventory ---------------------------------------------------------------
    def pick_up(self, item: Item) -> None:

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


