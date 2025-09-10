#if UNITY_EDITOR
using NUnit.Framework;
using System.IO;
using UnityEngine;
using Game.Save;

public class SaveSystemTests
{
    [Test]
    public void SaveAndLoadData()
    {
        var tempPath = Path.Combine(Application.dataPath, "test_save.json");
        var prevPath = SaveSystem.SavePath;

        var data = new SaveData
        {
            stats = new SavePlayerStats { level = 5, health = 80, experience = 1200 },
            inventory = { "sword", "potion" },
            progress = { checkpoint = "Hub", stage = 3 }
        };

        SaveSystem.SavePath = tempPath;
        SaveSystem.CurrentData = data;
        SaveSystem.Save();

        SaveSystem.CurrentData = new SaveData();
        SaveSystem.Load();
        var loaded = SaveSystem.CurrentData;

        Assert.AreEqual(data.stats.level, loaded.stats.level);
        Assert.AreEqual(data.stats.health, loaded.stats.health);
        Assert.AreEqual(data.stats.experience, loaded.stats.experience);
        CollectionAssert.AreEqual(data.inventory, loaded.inventory);
        Assert.AreEqual(data.progress.checkpoint, loaded.progress.checkpoint);
        Assert.AreEqual(data.progress.stage, loaded.progress.stage);

        File.Delete(tempPath);
        SaveSystem.SavePath = prevPath;
    }
}
#endif
