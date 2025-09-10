using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnHealthChanged;

    [SerializeField] private float maxHealth = 100f;
    public float MaxHealth => maxHealth;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
}
