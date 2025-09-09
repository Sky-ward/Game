from game.level import Level
from level.room import RoomTemplate


def test_room_generation_connectivity_and_bounds():
    templates = [
        RoomTemplate("corridor_ew", {"E", "W"}, weight=2),
        RoomTemplate("corridor_ns", {"N", "S"}, weight=2),
        RoomTemplate("corner_es", {"E", "S"}, weight=1),
        RoomTemplate("cross", {"N", "S", "E", "W"}, weight=1),
    ]
    boss = RoomTemplate("boss", {"N"})
    level = Level(width=4, height=4)
    level.generate(enemy_count=0, room_templates=templates, boss_room_template=boss)

    # All rooms lie within bounds
    for (x, y) in level.rooms.keys():
        assert 0 <= x < level.width
        assert 0 <= y < level.height

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
