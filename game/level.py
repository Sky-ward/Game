"""Level representation handling entity placement and bounds."""
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

    def clamp_position(self, x: int, y: int) -> Tuple[int, int]:
        """Clamp coordinates to level bounds."""
        return max(0, min(self.width - 1, x)), max(0, min(self.height - 1, y))

    def within_bounds(self, x: int, y: int) -> bool:
        """Check if coordinates fall within the level."""
        return 0 <= x < self.width and 0 <= y < self.height

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

    def remove_dead_enemies(self) -> None:
        """Prune enemies that are no longer alive."""
        self.enemies = [e for e in self.enemies if e.is_alive()]
