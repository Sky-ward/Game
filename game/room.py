"""Room templates defining size, exits and identifiers for level generation."""

from dataclasses import dataclass, field
from typing import List, Set


@dataclass(frozen=True)
class RoomTemplate:
    """Reusable room layout with size, exit and name metadata."""

    width: int
    height: int
    exits: Set[str]
    weight: int = 1
    name: str = ""


@dataclass
class Room:
    """A concrete room placed within a level grid."""

    template: RoomTemplate
    x: int
    y: int
    exits: Set[str] = field(default_factory=set)


# Basic room templates. All are single-tile rooms with different exits.
BASIC_TEMPLATES: List[RoomTemplate] = [
    RoomTemplate(1, 1, {"E", "W"}, weight=3, name="corridor_ew"),
    RoomTemplate(1, 1, {"N", "S"}, weight=3, name="corridor_ns"),
    RoomTemplate(1, 1, {"E", "S"}, weight=2, name="corner_es"),
    RoomTemplate(1, 1, {"N", "S", "E", "W"}, weight=1, name="cross"),
]

# Default boss room expects entrance from the north.
BOSS_ROOM_TEMPLATE = RoomTemplate(1, 1, {"N"}, name="boss")
