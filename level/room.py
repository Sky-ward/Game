from dataclasses import dataclass, field
from typing import Set, List

@dataclass(frozen=True)
class RoomTemplate:
    """Reusable room definition with exit metadata."""
    name: str
    exits: Set[str]
    weight: int = 1

@dataclass
class Room:
    """A placed room within a level."""
    template: RoomTemplate
    x: int
    y: int
    exits: Set[str] = field(default_factory=set)

# Basic collection of reusable rooms. Exits use compass directions.
BASIC_TEMPLATES: List[RoomTemplate] = [
    RoomTemplate("corridor_ew", {"E", "W"}, weight=3),
    RoomTemplate("corridor_ns", {"N", "S"}, weight=3),
    RoomTemplate("corner_es", {"E", "S"}, weight=2),
    RoomTemplate("cross", {"N", "S", "E", "W"}, weight=1),
]

# Default boss room template, expecting an entrance from the north.
BOSS_ROOM_TEMPLATE = RoomTemplate("boss", {"N"})
