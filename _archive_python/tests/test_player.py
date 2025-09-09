from game.level import Level
from game.player import Player


def test_player_movement_clamped():
    level = Level(width=5, height=5)
    player = Player(x=0, y=0)
    player.move(-1, -1, level)
    assert (player.x, player.y) == (0, 0)
    player.move(10, 10, level)
    assert (player.x, player.y) == (4, 4)


def test_player_heal_capped():
    player = Player(x=0, y=0, hp=50)
    player.heal(30)
    assert player.hp == 80
    player.heal(50)
    assert player.hp == 100
