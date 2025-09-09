from game.player import Player
from game.progression import Progression


def test_unlock_and_serialize():
    prog = Progression()
    prog.unlock("max_hp", 20)
    data = prog.to_json()
    loaded = Progression.from_json(data)
    assert loaded.upgrades == {"max_hp": 20}


def test_apply_max_hp_upgrade():
    player = Player(x=0, y=0)
    prog = Progression()
    prog.unlock("max_hp", 20)
    prog.apply(player)
    assert player.max_hp == 120
    assert player.hp == 120
