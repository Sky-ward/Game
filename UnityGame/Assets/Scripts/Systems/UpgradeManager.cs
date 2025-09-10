using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private int baseUpgradeCost = 10;

    public PlayerStats Stats
    {
        get => stats;
        set => stats = value;
    }

    public int BaseUpgradeCost
    {
        get => baseUpgradeCost;
        set => baseUpgradeCost = value;
    }

    public bool PurchaseUpgrade()
    {
        int cost = GetUpgradeCost();
        if (!stats || !stats.CanAfford(cost))
            return false;

        stats.ApplyUpgrade(cost);
        return true;
    }

    public int GetUpgradeCost()
    {
        return baseUpgradeCost * (stats != null ? stats.upgradeLevel + 1 : 1);
    }
}
