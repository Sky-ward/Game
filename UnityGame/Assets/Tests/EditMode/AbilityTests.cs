#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class AbilityTests
{
    private class TestAbility : Ability
    {
        protected override bool PerformAbility() => true;
    }

    [UnityTest]
    public IEnumerator CooldownPreventsImmediateReuse()
    {
        var go = new GameObject("Ability");
        var ability = go.AddComponent<TestAbility>();
        ability.Activate();
        Assert.IsTrue(ability.IsOnCooldown);
        yield return new WaitForSeconds(ability.Cooldown);
        Assert.IsFalse(ability.IsOnCooldown);
        Object.DestroyImmediate(go);
    }

    [Test]
    public void SkillUnlockRequiresPrerequisites()
    {
        var json = "{\"version\":1,\"skills\":[{\"id\":\"a\",\"name\":\"A\",\"prerequisites\":[],\"ability\":\"FireballAbility\"},{\"id\":\"b\",\"name\":\"B\",\"prerequisites\":[\"a\"],\"ability\":\"HealAbility\"}]}";
        var go = new GameObject("SkillTree");
        var tree = go.AddComponent<SkillTree>();
        tree.Load(json);
        Assert.IsFalse(tree.Unlock("b"));
        Assert.IsTrue(tree.Unlock("a"));
        Assert.IsTrue(tree.Unlock("b"));
        Object.DestroyImmediate(go);
    }
}
#endif
