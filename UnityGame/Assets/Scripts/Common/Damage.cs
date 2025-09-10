using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damage : MonoBehaviour
{
    [SerializeField] private float amount = 1f;
    public float Amount
    {
        get => amount;
        set => amount = value;
    }

    public void DealDamage(GameObject target)
    {
        var health = target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(amount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(collision.gameObject);
    }
}
