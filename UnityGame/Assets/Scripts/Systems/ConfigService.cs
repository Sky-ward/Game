using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using System.IO;

[CreateAssetMenu(menuName = "Configs/ConfigService")]
public class ConfigService : ScriptableObject
{
    public static ConfigService Instance { get; private set; }

    public static event Action OnConfigsReloaded;

    private Dictionary<string, EnemyConfig> _enemies;
    private Dictionary<string, ItemConfig> _items;
    private RoomArchetypesConfig _roomArchetypes;
    private Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>> _waves;
    private List<ProgressionLevel> _progression;

    private void Awake()
    {
        Instance = this;
    }

    public async Task InitializeAsync()
    {
        var enemiesCfg = await LoadConfigAsync<EnemiesConfig>("Configs/Enemies/enemies.json", c => c.enemies?.Count ?? 0, "Enemies");
        _enemies = enemiesCfg?.enemies.ToDictionary(e => e.id) ?? new Dictionary<string, EnemyConfig>();

        var itemsCfg = await LoadConfigAsync<ItemsConfig>("Configs/Items/items.json", c => c.items?.Count ?? 0, "Items");
        _items = itemsCfg?.items.ToDictionary(i => i.id) ?? new Dictionary<string, ItemConfig>();

        var progressionCfg = await LoadConfigAsync<ProgressionConfig>("Configs/Progression/progression.json", c => c.levels?.Count ?? 0, "Progression");
        _progression = progressionCfg?.levels ?? new List<ProgressionLevel>();

        var wavesCfg = await LoadConfigAsync<WavesConfig>("Configs/Levels/wave_configs.json", c => c.biomes?.Sum(b => b.Value.Sum(w => w.Value.Count)) ?? 0, "Waves");
        _waves = wavesCfg?.biomes ?? new Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>>();

        var roomsCfg = await LoadConfigAsync<RoomArchetypesConfig>("Configs/Levels/room_archetypes.json", c => c.biomes?.Sum(b => b.rooms.Count) ?? 0, "Rooms");
        _roomArchetypes = roomsCfg ?? new RoomArchetypesConfig();

        var errors = ConfigValidator.Validate(_enemies, _items, _roomArchetypes, _waves, _progression);
        if (errors.Count > 0)
        {
            foreach (var e in errors)
                Debug.LogError(e);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public async Task ReloadAll()
    {
        await InitializeAsync();
        OnConfigsReloaded?.Invoke();
    }

    public EnemyConfig GetEnemy(string id)
    {
        if (_enemies != null && _enemies.TryGetValue(id, out var config))
            return config;
        Log.Warn($"Enemy config for '{id}' not found.");
        return null;
    }

    public ItemConfig GetItem(string id)
    {
        if (_items != null && _items.TryGetValue(id, out var config))
            return config;
        Log.Warn($"Item config for '{id}' not found.");
        return null;
    }

    public IEnumerable<WaveSpawnConfig> GetWave(string biome, string waveId)
    {
        if (_waves != null && _waves.TryGetValue(biome, out var biomeWaves) && biomeWaves.TryGetValue(waveId, out var wave))
            return wave;
        Log.Warn($"Wave config '{biome}/{waveId}' not found.");
        return null;
    }

    public RoomArchetypesConfig RoomArchetypes => _roomArchetypes;

    public IEnumerable<ProgressionLevel> GetProgression() => _progression;

    public async Task<T> LoadConfigAsync<T>(string path, Func<T, int> countFunc, string name) where T : IVersioned
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
            Log.Warn($"Config {path} not found, using defaults.");
            return default;
        }

        var hash = ComputeHash(text);

        try
        {
            var data = JsonSerializer.Deserialize<T>(text);
            int count = countFunc != null ? countFunc(data) : 0;
            Log.Info($"[Config] {name} v{data.version} #{hash} loaded ({count} entries)");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse config {path}: {e.Message}");
            throw;
        }
    }

    private string ComputeHash(string text)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
        var sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
            sb.Append(bytes[i].ToString("x2"));
        return sb.ToString();
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
    public float drop_rate;
}

[Serializable]
public class RoomArchetypesConfig : IVersioned
{ 
    public int version;
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

public interface IVersioned
{
    int version { get; }
}

[Serializable]
public class EnemiesConfig : IVersioned
{
    public int version;
    public List<EnemyConfig> enemies;
}

[Serializable]
public class ItemsConfig : IVersioned
{
    public int version;
    public List<ItemConfig> items;
}

[Serializable]
public class WavesConfig : IVersioned
{
    public int version;
    public Dictionary<string, Dictionary<string, List<WaveSpawnConfig>>> biomes;
}

[Serializable]
public class ProgressionConfig : IVersioned
{
    public int version;
    public List<ProgressionLevel> levels;
}

