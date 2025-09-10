#if UNITY_EDITOR
using NUnit.Framework;
using System.IO;
using System.Text.Json;
using System.Linq;
using UnityEngine;

public class ConfigTests
{
    private string BasePath => Path.Combine(Application.dataPath, "Configs");

    [Test]
    public void EnemyIdsUnique()
    {
        var enemies = JsonSerializer.Deserialize<EnemiesConfig>(File.ReadAllText(Path.Combine(BasePath, "Enemies/enemies.json")));
        var ids = enemies.enemies.Select(e => e.id).ToList();
        Assert.AreEqual(ids.Count, ids.Distinct().Count());
    }

    [Test]
    public void WavesReferenceValidEnemies()
    {
        var enemies = JsonSerializer.Deserialize<EnemiesConfig>(File.ReadAllText(Path.Combine(BasePath, "Enemies/enemies.json")));
        var waves = JsonSerializer.Deserialize<WavesConfig>(File.ReadAllText(Path.Combine(BasePath, "Levels/wave_configs.json")));
        var enemyIds = enemies.enemies.Select(e => e.id).ToHashSet();
        foreach (var biome in waves.biomes.Values)
        foreach (var wave in biome.Values)
        foreach (var spawn in wave)
            Assert.IsTrue(enemyIds.Contains(spawn.enemy));
    }

    [Test]
    public void ProgressionMonotonic()
    {
        var progression = JsonSerializer.Deserialize<ProgressionConfig>(File.ReadAllText(Path.Combine(BasePath, "Progression/progression.json")));
        int prevLevel = 0; int prevXp = -1;
        foreach (var lvl in progression.levels)
        {
            Assert.Greater(lvl.level, prevLevel);
            Assert.GreaterOrEqual(lvl.xp, prevXp);
            prevLevel = lvl.level;
            prevXp = lvl.xp;
        }
    }

    [Test]
    public void RoomWeightsPositive()
    {
        var rooms = JsonSerializer.Deserialize<RoomArchetypesConfig>(File.ReadAllText(Path.Combine(BasePath, "Levels/room_archetypes.json")));
        foreach (var biome in rooms.biomes)
        foreach (var entry in biome.rooms)
            Assert.Greater(entry.weight, 0);
    }

    [Test]
    public void JsonDeserializes()
    {
        JsonSerializer.Deserialize<ItemsConfig>(File.ReadAllText(Path.Combine(BasePath, "Items/items.json")));
        Assert.Pass();
    }
}
#endif
