#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using System.Reflection;

public class EnemyTests
{
    [Test]
    public void PursuesTargetWithinRange()
    {
        var player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = Vector3.right * 2f;

        var enemy = new GameObject("Enemy");
        var dummy = enemy.AddComponent<DummyEnemy>();
        dummy.moveSpeed = 1f;
        dummy.chaseRange = 5f;

        var update = typeof(DummyEnemy).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
        update.Invoke(dummy, null);

        Assert.Greater(enemy.transform.position.x, 0f);

        Object.DestroyImmediate(player);
        Object.DestroyImmediate(enemy);
    }

    [Test]
    public void DamageExchangedOnAttack()
    {
        var player = new GameObject("Player");
        player.tag = "Player";
        var playerHealth = player.AddComponent<Health>();
        var playerDamage = player.AddComponent<Damage>();
        playerDamage.Amount = 7;
        var playerCombat = player.AddComponent<PlayerCombat>();

        var enemy = new GameObject("Enemy");
        var enemyHealth = enemy.AddComponent<Health>();
        var enemyDamage = enemy.AddComponent<Damage>();
        enemyDamage.Amount = 5;

        playerCombat.Attack(enemy);
        Assert.AreEqual(enemyHealth.MaxHealth - 7, enemyHealth.Current);

        enemyDamage.DealDamage(player);
        Assert.AreEqual(playerHealth.MaxHealth - 5, playerHealth.Current);

        Object.DestroyImmediate(player);
        Object.DestroyImmediate(enemy);
    }
}
#endif
