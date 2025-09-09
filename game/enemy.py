from dataclasses import dataclass

@dataclass
class Enemy:
    x: int
    y: int
    hp: int = 20
    attack: int = 5
    defense: int = 2

    def take_damage(self, dmg: int) -> int:
        actual = max(1, dmg - self.defense)
        self.hp -= actual
        return actual

    def is_alive(self) -> bool:
        return self.hp > 0
