using UnityEngine;

public class DashAbility : Ability
{
    [SerializeField] private float dashDistance = 5f;

    protected override bool PerformAbility()
    {
        transform.position += transform.forward * dashDistance;
        Debug.Log($"Dashed {dashDistance} units");
        return true;
    }
}
