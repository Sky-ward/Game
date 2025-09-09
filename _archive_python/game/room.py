
"""Room templates defining size, exits and optional names for level generation."""


from dataclasses import dataclass, field
from typing import List, Set


@dataclass(frozen=True)
class RoomTemplate:

    """Reusable room layout with size, exit, and name metadata."""


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

    RoomTemplate(1, 1, {"E", "W"}, name="corridor_ew", weight=3),
    RoomTemplate(1, 1, {"N", "S"}, name="corridor_ns", weight=3),
    RoomTemplate(1, 1, {"E", "S"}, name="corner_es", weight=2),
    RoomTemplate(1, 1, {"N", "S", "E", "W"}, name="cross", weight=1),

]

# Default boss room expects entrance from the north.
BOSS_ROOM_TEMPLATE = RoomTemplate(1, 1, {"N"}, name="boss")
