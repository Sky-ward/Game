#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarTests
{
    [Test]
    public void HealthBarUpdatesAfterDamage()
    {
        var player = new GameObject("Player");
        var health = player.AddComponent<Health>();

        var hudObj = new GameObject("HUD");
        var slider = hudObj.AddComponent<Slider>();
        var hud = hudObj.AddComponent<HUDController>();
        hud.playerHealthBar = slider;
        hud.Initialize(health);

        Assert.AreEqual(1f, slider.value, 0.001f);

        health.TakeDamage(50f);

        Assert.AreEqual(0.5f, slider.value, 0.001f);
    }
}
#endif
