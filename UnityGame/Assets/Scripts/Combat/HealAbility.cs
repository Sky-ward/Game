using UnityEngine;

public class HealAbility : Ability
{
    [SerializeField] private float healAmount = 10f;

    protected override bool PerformAbility()
    {
        Debug.Log($"Heal for {healAmount}");
        return true;
    }
}
