using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount);
}

public class PlayerCombat : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public void Attack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            var target = enemy.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void PerformAttack(IDamageable target)
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
