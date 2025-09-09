from game.level import Level
from game.weapon import Weapon


def test_level_generation_bounds():
    level = Level(width=10, height=5)
    enemy_pool = []
    weapon_pool = [Weapon(name="Sword", damage=5), Weapon(name="Bow", damage=3)]
    enemies, weapons = level.generate(
        enemy_count=5, weapon_count=2, weapon_pool=weapon_pool
    )
    assert len(enemies) == 5
    assert len(weapons) == 2
    for e in enemies:
        assert 0 <= e.x < level.width
        assert 0 <= e.y < level.height
    for w in weapons:
        assert 0 <= w.x < level.width
        assert 0 <= w.y < level.height
