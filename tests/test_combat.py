from game.player import Player
from game.enemy import Enemy


def test_combat_damage_application():
    player = Player(x=0, y=0, attack=10)
    enemy = Enemy(x=0, y=0, hp=15, defense=3)
    dmg = player.attack_enemy(enemy)
    assert dmg == 7  # 10 - 3 defense
    assert enemy.hp == 8
    assert enemy.is_alive()

    dmg2 = player.attack_enemy(enemy)
    assert dmg2 == 7
    assert enemy.hp == 1
    assert enemy.is_alive()

    dmg3 = player.attack_enemy(enemy)
    assert dmg3 == 7
    assert enemy.hp == -6
    assert not enemy.is_alive()
