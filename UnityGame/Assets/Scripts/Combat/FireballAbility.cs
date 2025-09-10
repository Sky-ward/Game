using UnityEngine;

public class FireballAbility : Ability
{
    [SerializeField] private float damage = 10f;

    protected override bool PerformAbility()
    {
        Debug.Log($"Cast Fireball dealing {damage} damage");
        return true;
    }
}
