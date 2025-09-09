from game.player import Player
from game.weapon import Weapon
from game.ui import (
    bind_item_keys,
    draw_health_bar,
    draw_weapon_name,
    render_inventory_slots,
)


def test_draw_hud_elements():
    player = Player(x=0, y=0, hp=50)
    player.max_hp = 100
    sword = Weapon(name="Sword", damage=10)
    player.weapon = sword

    bar = draw_health_bar(player)
    weapon_str = draw_weapon_name(player)

    assert bar == "HP: [#####-----] 50/100"
    assert weapon_str == "Weapon: Sword"


def test_inventory_render_and_bind():
    sword = Weapon(name="Sword", damage=10)
    axe = Weapon(name="Axe", damage=12)
    inventory = [sword, axe]

    labels = render_inventory_slots(inventory, slots=5)
    mapping = bind_item_keys(inventory)

    assert labels == [
        "1: Sword",
        "2: Axe",
        "3: Empty",
        "4: Empty",
        "5: Empty",
    ]
    assert mapping == {"1": sword, "2": axe}
