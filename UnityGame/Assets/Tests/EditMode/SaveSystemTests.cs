using NUnit.Framework;
using System.IO;
using System.Linq;

public class SaveSystemTests
{
    [Test]
    public void SaveAndLoadMultipleSlots()
    {
        var tempDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "test_saves");
        var prevDir = SaveSystem.SaveDirectory;
        SaveSystem.SaveDirectory = tempDir;

        if (Directory.Exists(tempDir))
            Directory.Delete(tempDir, true);
        Directory.CreateDirectory(tempDir);

        var data0 = new SaveData
        {
            stats = { level = 5, health = 80, experience = 1200 },
            inventory = { "sword", "potion" },
            progress = { checkpoint = "Hub", stage = 3 }
        };

        SaveSystem.CurrentData = data0;
        SaveSystem.Save(0);

        var data1 = new SaveData
        {
            stats = { level = 2, health = 50, experience = 400 },
            inventory = { "shield" },
            progress = { checkpoint = "Town", stage = 1 }
        };

        SaveSystem.CurrentData = data1;
        SaveSystem.Save(1);

        var infos = SaveSystem.GetAllSlots();
        Assert.AreEqual(2, infos.Length);
        Assert.IsTrue(infos.Any(i => i.slot == 0));
        Assert.IsTrue(infos.Any(i => i.slot == 1));

        SaveSystem.CurrentData = new SaveData();
        SaveSystem.Load(0);
        Assert.AreEqual(5, SaveSystem.CurrentData.stats.level);
        Assert.AreEqual("Hub", SaveSystem.CurrentData.progress.checkpoint);

        SaveSystem.CurrentData = new SaveData();
        SaveSystem.Load(1);
        Assert.AreEqual(2, SaveSystem.CurrentData.stats.level);
        Assert.AreEqual("Town", SaveSystem.CurrentData.progress.checkpoint);

        Directory.Delete(tempDir, true);
        SaveSystem.SaveDirectory = prevDir;
    }
}

