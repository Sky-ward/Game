using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Game.Save
{
    public static class SaveSystem
    {
        public static string SavePath = Path.Combine(Application.persistentDataPath, "save.json");
        public static SaveData CurrentData = new SaveData();

        public static void Save()
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

        public static void Load()
        {
            try
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
        }
    }
}
