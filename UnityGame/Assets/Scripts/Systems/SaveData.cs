using System;
using System.Collections.Generic;

namespace Game.Save
{
    [Serializable]
    public class SaveData
    {
        public SavePlayerStats stats = new SavePlayerStats();
        public List<string> inventory = new List<string>();
        public PlayerProgress progress = new PlayerProgress();
    }

    [Serializable]
    public class SavePlayerStats
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
}
