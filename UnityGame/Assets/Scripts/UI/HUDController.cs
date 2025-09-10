using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] public Slider playerHealthBar;
    [SerializeField] public Slider enemyHealthBar;
    [SerializeField] public Slider staminaBar;
    [SerializeField] public Text promptText;

    private Health playerHealth;
    private Health enemyHealth;

    public void Initialize(Health player, Health enemy = null)
    {
        playerHealth = player;
        enemyHealth = enemy;
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdatePlayerHealth;
            UpdatePlayerHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged += UpdateEnemyHealth;
            UpdateEnemyHealth(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdatePlayerHealth;
        }
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged -= UpdateEnemyHealth;
        }
    }

    private void UpdatePlayerHealth(float current, float max)
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.value = max > 0f ? current / max : 0f;
        }
    }

    private void UpdateEnemyHealth(float current, float max)
    {
        if (enemyHealthBar != null)
        {
            enemyHealthBar.value = max > 0f ? current / max : 0f;
        }
    }

    public void SetStamina(float normalizedValue)
    {
        if (staminaBar != null)
        {
            staminaBar.value = Mathf.Clamp01(normalizedValue);
        }
    }

    public void SetPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.text = message;
        }
    }
}
