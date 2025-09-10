using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public const int CurrentVersion = 1;

    // metadata
    public int slot;
    public int version = CurrentVersion;
    public long timestamp;

    public PlayerStats stats = new PlayerStats();
    public List<string> inventory = new List<string>();
    public PlayerProgress progress = new PlayerProgress();

    public void Migrate()
    {
        if (version == CurrentVersion) return;

        // Future migration logic can be added here
        version = CurrentVersion;
    }
}

[Serializable]
public class PlayerStats
{
    public int level;
    public int health;
    public int experience;
}

[Serializable]
public class PlayerProgress
{
    public string checkpoint = "";
    public int stage;
}
