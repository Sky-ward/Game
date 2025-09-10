using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerStats stats = new PlayerStats();
    public List<string> inventory = new List<string>();
    public PlayerProgress progress = new PlayerProgress();
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
    public string checkpoint;
    public int stage;
}
