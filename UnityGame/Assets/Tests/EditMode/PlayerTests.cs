#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;

public class PlayerTests
{
    [Test]
    public void MovementSpeedMatchesInput()
    {
        var go = new GameObject();
        var rb = go.AddComponent<Rigidbody2D>();
        var controller = go.AddComponent<PlayerController>();
        controller.moveSpeed = 5f;
        var velocity = controller.GetVelocityForInput(new Vector2(1,0), false);
        Assert.AreEqual(5f, velocity.magnitude, 0.01f);
    }

    private class DummyDamageable : IDamageable
    {
        public int received;
        public void TakeDamage(int amount)
        {
            received = amount;
        }
    }

    [Test]
    public void AttackDealsDamage()
    {
        var go = new GameObject();
        var combat = go.AddComponent<PlayerCombat>();
        combat.damage = 3;
        var dummy = new DummyDamageable();
        combat.PerformAttack(dummy);
        Assert.AreEqual(3, dummy.received);
    }
}
#endif
