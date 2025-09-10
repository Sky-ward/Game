using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Game.Save
{

    public static string SaveDirectory = Path.Combine(Application.persistentDataPath, "saves");
    public static SaveData CurrentData = new SaveData();
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { IncludeFields = true };

    private static string GetSlotPath(int slot) => Path.Combine(SaveDirectory, $"slot_{slot}.json");

    public static void Save(int slot)
    {
        try
        {
            Directory.CreateDirectory(SaveDirectory);
            CurrentData.slot = slot;
            CurrentData.version = SaveData.CurrentVersion;
            CurrentData.timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var json = JsonSerializer.Serialize(CurrentData, options);
            File.WriteAllText(GetSlotPath(slot), json);
        }
        catch (Exception e)

        {
            try
            {
                var json = JsonSerializer.Serialize(CurrentData);
                File.WriteAllText(SavePath, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Save failed: {e.Message}");
            }
        }


    public static void Save() => Save(CurrentData.slot);

    public static void Load(int slot)
    {
        var path = GetSlotPath(slot);
        try
        {
            if (!File.Exists(path))

            {
                if (!File.Exists(SavePath))
                {
                    Debug.LogWarning("Save file not found");
                    CurrentData = new SaveData();
                    return;
                }
                var json = File.ReadAllText(SavePath);
                CurrentData = JsonSerializer.Deserialize<SaveData>(json) ?? new SaveData();
            }
            catch (Exception e)
            {
                Debug.LogError($"Load failed: {e.Message}");
                CurrentData = new SaveData();
            }

            var json = File.ReadAllText(path);
            CurrentData = JsonSerializer.Deserialize<SaveData>(json, options) ?? new SaveData();
            CurrentData.Migrate();
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}");
            CurrentData = new SaveData();

        }
    }

    public static void Load() => Load(CurrentData.slot);

    public static SlotInfo[] GetAllSlots()
    {
        if (!Directory.Exists(SaveDirectory))
            return Array.Empty<SlotInfo>();

        var files = Directory.GetFiles(SaveDirectory, "slot_*.json");
        var infos = new SlotInfo[files.Length];
        int idx = 0;
        foreach (var file in files)
        {
            try
            {
                var json = File.ReadAllText(file);
                var data = JsonSerializer.Deserialize<SaveData>(json, options);
                if (data != null)
                {
                    infos[idx++] = new SlotInfo
                    {
                        slot = data.slot,
                        version = data.version,
                        timestamp = data.timestamp
                    };
                }
            }
            catch { }
        }
        Array.Resize(ref infos, idx);
        return infos;
    }

    public struct SlotInfo
    {
        public int slot;
        public int version;
        public long timestamp;
    }
}

