using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Damage : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    public int Amount
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

    private void OnTriggerEnter(Collider other)
    {
        DealDamage(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision.gameObject);
    }
}
