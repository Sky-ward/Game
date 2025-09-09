from game.level import Level
from game.room import RoomTemplate


def test_room_generation_connectivity_and_bounds():
    templates = [
        RoomTemplate(1, 1, {"E", "W"}, weight=2),
        RoomTemplate(1, 1, {"N", "S"}, weight=2),
        RoomTemplate(1, 1, {"E", "S"}, weight=1),
        RoomTemplate(1, 1, {"N", "S", "E", "W"}, weight=1),
    ]
    boss = RoomTemplate(1, 1, {"N"})
    level = Level(width=4, height=4)
    level.generate(enemy_count=0, room_templates=templates, boss_room_template=boss)

    # All rooms lie within bounds and respect template sizes
    for (x, y), room in level.rooms.items():
        assert 0 <= x < level.width
        assert 0 <= y < level.height
        assert x + room.template.width <= level.width
        assert y + room.template.height <= level.height

    # Check connectivity from start to boss using exits
    deltas = {"N": (0, -1), "S": (0, 1), "E": (1, 0), "W": (-1, 0)}
    visited = {level.start_room}
    stack = [level.start_room]
    while stack:
        cx, cy = stack.pop()
        room = level.rooms[(cx, cy)]
        for d in room.exits:
            dx, dy = deltas[d]
            nx, ny = cx + dx, cy + dy
            if (nx, ny) in level.rooms and (nx, ny) not in visited:
                visited.add((nx, ny))
                stack.append((nx, ny))
    assert level.boss_room in visited
    assert len(level.rooms) == level.width + level.height - 1


def test_room_exit_symmetry_and_bounds():
    templates = [
        RoomTemplate(1, 1, {"E", "W"}, weight=2),
        RoomTemplate(1, 1, {"N", "S"}, weight=2),
        RoomTemplate(1, 1, {"E", "S"}, weight=1),
        RoomTemplate(1, 1, {"N", "S", "E", "W"}, weight=1),
    ]
    boss = RoomTemplate(1, 1, {"N"})
    level = Level(width=4, height=4)
    level.generate(enemy_count=0, room_templates=templates, boss_room_template=boss)

    deltas = {"N": (0, -1), "S": (0, 1), "E": (1, 0), "W": (-1, 0)}
    opposite = {"N": "S", "S": "N", "E": "W", "W": "E"}

    for (x, y), room in level.rooms.items():
        for d in room.exits:
            dx, dy = deltas[d]
            nx, ny = x + dx, y + dy
            assert (nx, ny) in level.rooms
            neighbour = level.rooms[(nx, ny)]
            assert opposite[d] in neighbour.exits
            assert 0 <= nx < level.width
            assert 0 <= ny < level.height
