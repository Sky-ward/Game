from game.enemy import Enemy
from game.level import Level
from game.player import Player


def test_enemy_move_and_attack():
    level = Level(width=5, height=5)
    enemy = Enemy(x=0, y=0, attack=5)
    player = Player(x=4, y=4, hp=20, defense=0)
    enemy.move_towards(player, level)
    assert (enemy.x, enemy.y) == (1, 1)
    dmg = enemy.attack_player(player)
    assert dmg == 5
    assert player.hp == 15
