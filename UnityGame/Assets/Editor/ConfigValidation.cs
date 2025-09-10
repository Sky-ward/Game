#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

public static class ConfigValidation
{
    [MenuItem("Game/Validate Configs")]
    public static void ValidateMenu()
    {
        var errors = ValidateInternal();
        if (errors.Count == 0)
        {
            Debug.Log("Config validation passed");
            EditorUtility.DisplayDialog("Validate Configs", "All configs valid", "OK");
        }
        else
        {
            foreach (var e in errors)
                Debug.LogError(e);
            EditorUtility.DisplayDialog("Validate Configs", "Configs invalid", "OK");
        }
    }

    [MenuItem("Game/Reload Configs")]
    public static void ReloadMenu()
    {
        if (ConfigService.Instance != null)
            ConfigService.Instance.ReloadAll();
    }

    [MenuItem("Game/Log Level/Info")]
    public static void LogInfo()
    {
        Log.SetLevel(LogLevel.Info);
    }

    [MenuItem("Game/Log Level/Warn")]
    public static void LogWarn()
    {
        Log.SetLevel(LogLevel.Warn);
    }

    public static int Run()
    {
        var errors = ValidateInternal();
        if (errors.Count == 0)
        {
            Debug.Log("Config validation passed");
            return 0;
        }
        foreach (var e in errors)
            Debug.LogError(e);
        return 1;
    }

    private static List<string> ValidateInternal()
    {
        var basePath = Path.Combine(Application.dataPath, "Configs");
        var enemies = JsonSerializer.Deserialize<EnemiesConfig>(File.ReadAllText(Path.Combine(basePath, "Enemies/enemies.json")));

        var items = JsonSerializer.Deserialize<ItemsConfig>(File.ReadAllText(Path.Combine(basePath, "Items/items.json")));

        var rooms = JsonSerializer.Deserialize<RoomArchetypesConfig>(File.ReadAllText(Path.Combine(basePath, "Levels/room_archetypes.json")));
        var waves = JsonSerializer.Deserialize<WavesConfig>(File.ReadAllText(Path.Combine(basePath, "Levels/wave_configs.json")));
        var progression = JsonSerializer.Deserialize<ProgressionConfig>(File.ReadAllText(Path.Combine(basePath, "Progression/progression.json")));
        var enemyDict = enemies.enemies.ToDictionary(e => e.id);

        var itemDict = items.items.ToDictionary(i => i.id);
        return ConfigValidator.Validate(enemyDict, itemDict, rooms, waves.biomes, progression.levels);

    }
}
#endif
