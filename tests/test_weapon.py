from game.enemy import Enemy
from game.player import Player
from game.weapon import Weapon


def test_player_weapon_attack():
    player = Player(x=0, y=0, attack=5)
    sword = Weapon(name="Sword", damage=10)
    player.pick_up(sword)
    player.equip(sword)
    enemy = Enemy(x=0, y=0, hp=30, defense=0)
    dmg = player.attack_enemy(enemy)
    assert dmg == 15
    assert enemy.hp == 15
