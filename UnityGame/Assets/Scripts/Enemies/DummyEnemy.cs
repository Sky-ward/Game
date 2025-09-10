using UnityEngine;

[RequireComponent(typeof(Health))]
public class DummyEnemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float chaseRange = 5f;
    [SerializeField] public float attackRange = 1f;
    [SerializeField] public int contactDamage = 10;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private float attackTimer;

    private void Update()
    {
        if (player == null)
        {
            var playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                return;
            }
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                var health = player.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(contactDamage);
                }
                attackTimer = attackCooldown;
            }
        }
        else if (distance <= chaseRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
