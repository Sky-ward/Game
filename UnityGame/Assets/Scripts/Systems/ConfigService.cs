using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;

[CreateAssetMenu(menuName = "Configs/ConfigService")]
public class ConfigService : ScriptableObject
{
    private Dictionary<string, EnemyConfig> _enemies;
    private Dictionary<string, ItemConfig> _items;
    private RoomArchetypesConfig _roomArchetypes;
    private Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>> _waves;
    private List<ProgressionLevel> _progression;

    public async Task InitializeAsync()
    {
        _enemies = (await LoadConfigAsync<List<EnemyConfig>>("Configs/Enemies/enemies.json"))
            ?.ToDictionary(e => e.id) ?? new Dictionary<string, EnemyConfig>();

        _items = (await LoadConfigAsync<List<ItemConfig>>("Configs/Items/items.json"))
            ?.ToDictionary(i => i.id) ?? new Dictionary<string, ItemConfig>();

        _roomArchetypes = await LoadConfigAsync<RoomArchetypesConfig>("Configs/Levels/room_archetypes.json")
            ?? new RoomArchetypesConfig();

        _waves = await LoadConfigAsync<Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>>>("Configs/Levels/wave_configs.json")
            ?? new Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>>();

        _progression = await LoadConfigAsync<List<ProgressionLevel>>("Configs/Progression/progression.json")
            ?? new List<ProgressionLevel>();
    }

    public EnemyConfig GetEnemy(string id)
    {
        if (_enemies != null && _enemies.TryGetValue(id, out var config))
            return config;
        Debug.LogWarning($"Enemy config for '{id}' not found.");
        return null;
    }

    public ItemConfig GetItem(string id)
    {
        if (_items != null && _items.TryGetValue(id, out var config))
            return config;
        Debug.LogWarning($"Item config for '{id}' not found.");
        return null;
    }

    public IEnumerable<WaveSpawnConfig> GetWave(string biome, string waveId)
    {
        if (_waves != null && _waves.TryGetValue(biome, out var biomeWaves) && biomeWaves.TryGetValue(waveId, out var wave))
            return wave;
        Debug.LogWarning($"Wave config '{biome}/{waveId}' not found.");
        return null;
    }

    public RoomArchetypesConfig RoomArchetypes => _roomArchetypes;

    public IEnumerable<ProgressionLevel> GetProgression() => _progression;

    public async Task<T> LoadConfigAsync<T>(string path)
    {
        string text = null;
        try
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(path);
            var asset = await handle.Task;
            if (asset != null)
            {
                text = asset.text;
            }
        }
        catch { }

        if (text == null)
        {
            var textAsset = Resources.Load<TextAsset>(path);
            if (textAsset != null)
            {
                text = textAsset.text;
            }
        }

        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning($"Config {path} not found, using defaults.");
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(text);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to parse config {path}: {e.Message}");
            return default;
        }
    }
}

[Serializable]
public class EnemyConfig
{
    public string id;
    public int hp;
    public float speed;
    public int attack;
    public string ai;
    public float projectile_speed;
}

[Serializable]
public class ItemConfig
{
    public string id;
    public string type;
    public int value;
    public int heal;
}

[Serializable]
public class RoomArchetypesConfig
{
    public List<BiomeRooms> biomes;
}

[Serializable]
public class BiomeRooms
{
    public string id;
    public List<RoomTypeEntry> rooms;
    public float hidden_room_chance;
}

[Serializable]
public class RoomTypeEntry
{
    public string type;
    public int weight;
    public int waves_min;
    public int waves_max;
}

[Serializable]
public class WaveSpawnConfig
{
    public string enemy;
    public int count;
    public float spawn_interval;
}

[Serializable]
public class ProgressionLevel
{
    public int level;
    public int xp;
}
