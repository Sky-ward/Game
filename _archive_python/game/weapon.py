from dataclasses import dataclass


@dataclass
class Weapon:
    """Simple weapon representation."""
    name: str
    damage: int
    x: int = 0
    y: int = 0
