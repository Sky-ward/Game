using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public int currency;
    public int upgradeLevel;

    public bool CanAfford(int cost) => currency >= cost;

    public void ApplyUpgrade(int cost)
    {
        if (CanAfford(cost))
        {
            currency -= cost;
            upgradeLevel++;
        }
    }
}
