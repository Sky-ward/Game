from game.items import Consumable
from game.player import Player


def test_pickup_and_use_healing_consumable():
    player = Player(x=0, y=0, hp=50)
    potion = Consumable(name="Potion", heal=30)

    player.pick_up(potion)
    assert potion in player.inventory

    player.use_item(potion)
    assert player.hp == 80
    assert potion not in player.inventory


def test_pickup_and_use_damage_consumable():
    player = Player(x=0, y=0, defense=0)
    poison = Consumable(name="Poison", damage=20)

    player.pick_up(poison)
    player.use_item(poison)

    assert player.hp == 80
    assert poison not in player.inventory

