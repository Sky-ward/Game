
"""Player entity with movement, inventory management and combat helpers."""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import List, Optional, Union, TYPE_CHECKING

from .enemy import Enemy
from .weapon import Weapon
from .items import Consumable

"""Player entity with basic movement, inventory and combat helpers."""
from __future__ import annotations

from dataclasses import dataclass, field
from typing import List, Optional, TYPE_CHECKING




from .enemy import Enemy
from .weapon import Weapon


if TYPE_CHECKING:  # pragma: no cover - for type checking only
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

    inventory: List[Weapon] = field(default_factory=list)


    def move(self, dx: int, dy: int, level: Optional[Level] = None) -> None:
        """Move the player by ``dx``/``dy`` and optionally clamp to level bounds."""
        nx, ny = self.x + dx, self.y + dy
        if level:
            nx, ny = level.clamp_position(nx, ny)
        self.x, self.y = nx, ny





    def take_damage(self, dmg: int) -> int:
        """Apply damage after defense and return the actual damage dealt."""
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual


    def heal(self, amount: int) -> None:
        """Restore hit points up to the maximum."""
        self.hp = min(self.max_hp, self.hp + amount)

    def pick_up(self, item: Item) -> None:
        """Add an item to inventory without equipping or using it."""
        if item not in self.inventory:
            self.inventory.append(item)

    def pick_up(self, weapon: Weapon) -> None:
        """Add a weapon to inventory without equipping."""
        if weapon not in self.inventory:
            self.inventory.append(weapon)


    def equip(self, weapon: Weapon) -> None:
        """Equip a weapon that is already in the inventory."""
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

    def heal(self, amount: int) -> None:
        """Restore hit points up to maximum."""
        self.hp = min(self.max_hp, self.hp + amount)





    def attack_enemy(self, enemy: Enemy) -> int:
        """Attack an enemy using base attack plus equipped weapon damage."""
        dmg = self.attack
        if self.weapon:
            dmg += self.weapon.damage
        return enemy.take_damage(dmg)

    def is_alive(self) -> bool:
        """Return ``True`` if the player still has hit points."""
        return self.hp > 0

