"""Minimal prototype for a side-scrolling roguelike."""
from game.player import Player
from game.level import Level
from game.weapon import Weapon


def main() -> None:
    level = Level(width=10, height=5)
    weapon_pool = [Weapon(name="Sword", damage=15), Weapon(name="Bow", damage=7)]
    enemies, weapons = level.generate(enemy_count=3, weapon_count=1, weapon_pool=weapon_pool)
    player = Player(x=0, y=0)

    print("Level generated with enemies and weapons at:")
    for enemy in enemies:
        print(f"  Enemy ({enemy.x},{enemy.y}) HP:{enemy.hp}")
    for weapon in weapons:
        print(f"  Weapon {weapon.name} at ({weapon.x},{weapon.y})")

    # Player picks up first weapon if available
    if weapons:
        player.pick_up(weapons[0])

        player.equip(weapons[0])

        print(f"Player equipped {player.weapon.name}")

    # Simple combat demo
    if enemies:
        target = enemies[0]
        dmg = player.attack_enemy(target)
        print(f"Player hits enemy for {dmg} damage.")
        if not target.is_alive():
            print("Enemy defeated!")
    else:
        print("No enemies to fight.")


if __name__ == "__main__":
    main()
