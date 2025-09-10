using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private float cooldown = 1f;
    private float lastUseTime = -Mathf.Infinity;

    public float Cooldown => cooldown;
    public bool IsOnCooldown => Time.time < lastUseTime + cooldown;
    public float CooldownRemaining => Mathf.Max(0f, lastUseTime + cooldown - Time.time);

    public bool Activate()
    {
        if (IsOnCooldown)
            return false;
        if (PerformAbility())
        {
            lastUseTime = Time.time;
            return true;
        }
        return false;
    }

    protected abstract bool PerformAbility();
}
