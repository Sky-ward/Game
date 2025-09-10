#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;

public class UpgradeManagerTests
{
    [Test]
    public void PurchaseUpgradeDeductsCurrencyAndIncreasesLevel()
    {
        var stats = ScriptableObject.CreateInstance<PlayerStats>();
        stats.currency = 15;
        stats.upgradeLevel = 0;

        var go = new GameObject();
        var manager = go.AddComponent<UpgradeManager>();
        manager.Stats = stats;
        manager.BaseUpgradeCost = 10;

        Assert.IsTrue(manager.PurchaseUpgrade());
        Assert.AreEqual(1, stats.upgradeLevel);
        Assert.AreEqual(5, stats.currency);
    }

    [Test]
    public void PurchaseFailsWithInsufficientCurrency()
    {
        var stats = ScriptableObject.CreateInstance<PlayerStats>();
        stats.currency = 5;
        stats.upgradeLevel = 0;

        var go = new GameObject();
        var manager = go.AddComponent<UpgradeManager>();
        manager.Stats = stats;
        manager.BaseUpgradeCost = 10;

        Assert.IsFalse(manager.PurchaseUpgrade());
        Assert.AreEqual(0, stats.upgradeLevel);
        Assert.AreEqual(5, stats.currency);
    }
}
#endif
