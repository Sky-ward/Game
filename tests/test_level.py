
from game.enemy import Enemy

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



def test_remove_dead_enemies():
    level = Level(width=5, height=5)
    level.enemies = [Enemy(x=0, y=0, hp=0), Enemy(x=1, y=1, hp=5)]
    level.remove_dead_enemies()
    assert len(level.enemies) == 1
    assert level.enemies[0].hp == 5

