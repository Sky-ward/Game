from game.enemy import Enemy
from game.player import Player
from game.skills import FireDamage, SpeedBoost


def test_fire_damage_stacking_and_expiration():
    player = Player(x=0, y=0, attack=10)
    enemy = Enemy(x=0, y=0, hp=100, defense=0)
    player.skills.append(FireDamage(duration=2, bonus=3))
    player.skills.append(FireDamage(duration=2, bonus=2))

    dmg1 = player.attack_enemy(enemy)
    dmg2 = player.attack_enemy(enemy)
    dmg3 = player.attack_enemy(enemy)

    assert dmg1 == 15 and dmg2 == 15 and dmg3 == 10
    assert enemy.hp == 60  # 100 - 15 - 15 - 10


def test_speed_boost_stacking_and_expiration():
    player = Player(x=0, y=0)
    player.skills.append(SpeedBoost(duration=2, multiplier=2))
    player.skills.append(SpeedBoost(duration=1, multiplier=2))

    player.move(1, 0)
    assert (player.x, player.y) == (4, 0)

    player.move(1, 0)
    assert (player.x, player.y) == (6, 0)

    player.move(1, 0)
    assert (player.x, player.y) == (7, 0)
