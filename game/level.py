import random
from dataclasses import dataclass, field
from typing import List, Optional, Tuple

from .enemy import Enemy
from .weapon import Weapon


@dataclass
class Level:
    width: int
    height: int
    enemies: List[Enemy] = field(default_factory=list)
    weapons: List[Weapon] = field(default_factory=list)

    def generate(
        self,
        enemy_count: int,
        weapon_count: int = 0,
        weapon_pool: Optional[List[Weapon]] = None,
    ) -> Tuple[List[Enemy], List[Weapon]]:
        """Generate enemies and weapons at random positions within the level."""
        self.enemies = []
        self.weapons = []
        for _ in range(enemy_count):
            x = random.randint(0, self.width - 1)
            y = random.randint(0, self.height - 1)
            self.enemies.append(Enemy(x=x, y=y))
        if weapon_pool:
            for _ in range(weapon_count):
                base_weapon = random.choice(weapon_pool)
                x = random.randint(0, self.width - 1)
                y = random.randint(0, self.height - 1)
                self.weapons.append(
                    Weapon(name=base_weapon.name, damage=base_weapon.damage, x=x, y=y)
                )
        return self.enemies, self.weapons
