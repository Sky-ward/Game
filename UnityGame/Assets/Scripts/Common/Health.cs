using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int MaxHealth => maxHealth;
    public int Current { get; private set; }
    public bool IsDead => Current <= 0;

    private void Awake()
    {
        Current = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Current = Mathf.Max(0, Current - amount);
    }
}
