"""Level representation handling entity placement and bounds."""

import random
from dataclasses import dataclass, field
from typing import Dict, List, Optional, Tuple, Set

from .room import (
    BASIC_TEMPLATES,
    BOSS_ROOM_TEMPLATE,
    Room,
    RoomTemplate,
)
from .enemy import Enemy
from .weapon import Weapon

DIRECTION_DELTAS = {"N": (0, -1), "S": (0, 1), "E": (1, 0), "W": (-1, 0)}


def _direction(from_pos: Tuple[int, int], to_pos: Tuple[int, int]) -> str:
    """Return cardinal direction from one coordinate to an adjacent one."""
    dx = to_pos[0] - from_pos[0]
    dy = to_pos[1] - from_pos[1]
    if dx == 1 and dy == 0:
        return "E"
    if dx == -1 and dy == 0:
        return "W"
    if dy == 1 and dx == 0:
        return "S"
    if dy == -1 and dx == 0:
        return "N"
    raise ValueError("Positions are not adjacent")


def _weighted_choice(templates: List[RoomTemplate]) -> RoomTemplate:
    weights = [t.weight for t in templates]
    return random.choices(templates, weights=weights, k=1)[0]


@dataclass
class Level:
    width: int
    height: int
    enemies: List[Enemy] = field(default_factory=list)
    weapons: List[Weapon] = field(default_factory=list)
    rooms: Dict[Tuple[int, int], Room] = field(default_factory=dict)
    start_room: Tuple[int, int] = (0, 0)
    boss_room: Tuple[int, int] = (0, 0)

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
        room_templates: Optional[List[RoomTemplate]] = None,
        boss_room_template: RoomTemplate = BOSS_ROOM_TEMPLATE,
    ) -> Tuple[List[Enemy], List[Weapon]]:
        """Generate enemies, weapons, and a connected path of rooms."""
        if room_templates is None:
            room_templates = BASIC_TEMPLATES

        # Assemble rooms with at least one path to a boss room.
        self.rooms = {}
        path = [(x, 0) for x in range(self.width)]
        if self.height > 1:
            path.extend([(self.width - 1, y) for y in range(1, self.height)])
        for i, (x, y) in enumerate(path):
            required: Set[str] = set()
            if i > 0:
                required.add(_direction((x, y), path[i - 1]))
            if i < len(path) - 1:
                required.add(_direction((x, y), path[i + 1]))
            if i == len(path) - 1:
                template = boss_room_template
            else:
                candidates = [t for t in room_templates if required <= t.exits]
                if not candidates:
                    candidates = room_templates
                template = _weighted_choice(candidates)
            if x + template.width > self.width or y + template.height > self.height:
                raise ValueError("Room template exceeds level bounds")
            self.rooms[(x, y)] = Room(template=template, x=x, y=y, exits=required.copy())

        if path:
            self.start_room = path[0]
            self.boss_room = path[-1]

        # Enemy and weapon placement remains unchanged.
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
