using System.Collections.Generic;
using System.Linq;

public static class ConfigValidator
{
    private static readonly HashSet<string> AllowedRoomTypes = new HashSet<string>
    {
        "Combat","Elite","Reward","Shop","Rest","Challenge"
    };

    public static List<string> Validate(
        Dictionary<string, EnemyConfig> enemies,
        Dictionary<string, ItemConfig> items,
        RoomArchetypesConfig rooms,
        Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>> waves,
        List<ProgressionLevel> progression)
    {
        var errors = new List<string>();
        var enemyIds = enemies.Keys.ToHashSet();

        if (items != null)
        {
            foreach (var item in items.Values)
            {
                if (item.drop_rate < 0 || item.drop_rate > 1)
                    errors.Add($"Item {item.id} has drop_rate outside [0,1]");
            }
        }

        if (waves != null)
        {
            foreach (var biome in waves.Values)
            foreach (var wave in biome.Values)
            foreach (var spawn in wave)
            {
                if (!enemyIds.Contains(spawn.enemy))
                    errors.Add($"Wave references missing enemy id: {spawn.enemy}");
                if (spawn.count <= 0)
                    errors.Add($"Wave spawn count must be positive for enemy {spawn.enemy}");
            }
        }

        if (rooms?.biomes != null)
        {
            foreach (var biome in rooms.biomes)
            {
                if (biome.hidden_room_chance < 0 || biome.hidden_room_chance > 1)
                    errors.Add($"hidden_room_chance out of range in biome {biome.id}");
                foreach (var entry in biome.rooms)
                {
                    if (!AllowedRoomTypes.Contains(entry.type))
                        errors.Add($"Room type {entry.type} in biome {biome.id} is invalid");
                    if (entry.weight <= 0)
                        errors.Add($"Room type {entry.type} has non-positive weight in biome {biome.id}");
                    if (entry.waves_min < 0 || entry.waves_max < entry.waves_min)
                        errors.Add($"Room waves range invalid for {entry.type} in biome {biome.id}");
                }
            }
        }

        if (progression != null && progression.Count > 0)
        {
            int expectedLevel = progression[0].level;
            int prevXp = -1;
            foreach (var lvl in progression)
            {
                if (lvl.level != expectedLevel)
                    errors.Add("Progression levels must be consecutive starting from " + progression[0].level);
                if (lvl.xp < 0)
                    errors.Add($"Progression xp negative at level {lvl.level}");
                if (prevXp > lvl.xp)
                    errors.Add($"Progression xp not non-decreasing at level {lvl.level}");
                expectedLevel++;
                prevXp = lvl.xp;
            }
        }

        return errors;
    }
}
