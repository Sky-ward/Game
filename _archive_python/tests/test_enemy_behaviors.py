import pytest

from game.enemy import MeleeEnemy, RangedEnemy, HeavyEnemy
from game.level import Level
from game.player import Player


def test_melee_enemy_moves_and_attacks():
    level = Level(width=5, height=5)
    player = Player(x=4, y=4, hp=20, defense=0)
    enemy = MeleeEnemy(x=0, y=0, attack=5)

    dmg = enemy.take_turn(player, level)
    assert dmg == 0
    assert (enemy.x, enemy.y) == (1, 1)
    assert enemy.state == "pursuing"

    enemy.x, enemy.y = 3, 4
    dmg = enemy.take_turn(player, level)
    assert dmg == 5
    assert player.hp == 15
    assert enemy.state == "attacking"


def test_ranged_enemy_projectile_attack_and_movement():
    level = Level(width=5, height=5)
    player = Player(x=4, y=4, hp=20, defense=0)
    enemy = RangedEnemy(x=2, y=3, attack=3)

    dmg = enemy.take_turn(player, level)
    assert dmg == 3
    assert (enemy.x, enemy.y) == (2, 3)
    assert player.hp == 17
    assert enemy.state == "shooting"

    enemy.x, enemy.y = 0, 0
    dmg = enemy.take_turn(player, level)
    assert dmg == 0
    assert (enemy.x, enemy.y) == (1, 1)
    assert enemy.state == "positioning"


def test_heavy_enemy_slow_move_and_strike():
    level = Level(width=5, height=5)
    player = Player(x=4, y=4, hp=30, defense=0)
    enemy = HeavyEnemy(x=0, y=0, attack=4)

    dmg = enemy.take_turn(player, level)
    assert dmg == 0
    assert (enemy.x, enemy.y) == (1, 0)
    assert enemy.state == "pursuing"

    enemy.x, enemy.y = 3, 4
    dmg = enemy.take_turn(player, level)
    assert dmg == 8
    assert player.hp == 22
    assert enemy.state == "striking"

    dmg = enemy.take_turn(player, level)
    assert dmg == 0
    assert enemy.state == "recovering"
