"""Track unlockable upgrades and apply them to players."""
from __future__ import annotations

import json
from dataclasses import dataclass, field
from typing import Dict, TYPE_CHECKING

if TYPE_CHECKING:  # pragma: no cover - for type checking only
    from .player import Player


@dataclass
class Progression:
    """Store unlocked upgrades and handle persistence."""

    upgrades: Dict[str, int] = field(default_factory=dict)

    def unlock(self, name: str, amount: int = 1) -> None:
        """Unlock or increment an upgrade by a given amount."""
        self.upgrades[name] = self.upgrades.get(name, 0) + amount

    def apply(self, player: Player) -> None:
        """Apply upgrades to the provided player instance."""
        if "max_hp" in self.upgrades:
            inc = self.upgrades["max_hp"]
            player.max_hp += inc
            player.hp += inc

    # -- serialization helpers -------------------------------------------------
    def to_dict(self) -> Dict[str, int]:
        return dict(self.upgrades)

    @classmethod
    def from_dict(cls, data: Dict[str, int]) -> "Progression":
        return cls(upgrades=dict(data))

    def to_json(self) -> str:
        return json.dumps(self.upgrades)

    @classmethod
    def from_json(cls, data: str) -> "Progression":
        if not data:
            return cls()
        return cls(upgrades=json.loads(data))
