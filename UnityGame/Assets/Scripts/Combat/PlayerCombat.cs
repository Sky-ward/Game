using UnityEngine;

[RequireComponent(typeof(Damage))]
public class PlayerCombat : MonoBehaviour
{
    private Damage damage;

    private void Awake()
    {
        damage = GetComponent<Damage>();
    }

    public void Attack(GameObject target)
    {
        if (damage != null)
        {
            damage.DealDamage(target);
        }
    }
}
