"""Inventory UI helpers."""
from __future__ import annotations

from typing import Dict, Iterable, List

from ..weapon import Weapon


def render_inventory_slots(items: Iterable[Weapon], slots: int = 5) -> List[str]:
    """Render inventory slots as simple text labels.

    Parameters
    ----------
    items:
        Weapons to place into slots ordered by preference.
    slots:
        Total number of available slots.
    """
    labels: List[str] = []
    items_list = list(items)
    for idx in range(slots):
        if idx < len(items_list):
            labels.append(f"{idx + 1}: {items_list[idx].name}")
        else:
            labels.append(f"{idx + 1}: Empty")
    return labels


def bind_item_keys(items: Iterable[Weapon]) -> Dict[str, Weapon]:
    """Bind numerical keys (1..n) to weapons for quick use."""
    mapping: Dict[str, Weapon] = {}
    for idx, item in enumerate(items, start=1):
        mapping[str(idx)] = item
    return mapping
