#if UNITY_EDITOR
using NUnit.Framework;

public class InventoryTests
{
    [Test]
    public void ItemPickupAddsToInventory()
    {
        var inv = new Inventory();
        var item = new Item { id = "sword", slot = EquipmentSlot.Weapon, attack = 5 };
        inv.AddItem(item);
        Assert.IsTrue(inv.Contains(item));
    }

    [Test]
    public void EquipItemAppliesStats()
    {
        var inv = new Inventory();
        var item = new Item { id = "sword", slot = EquipmentSlot.Weapon, attack = 5 };
        inv.AddItem(item);
        inv.Equip(item);
        Assert.AreEqual(5, inv.Stats.attack);
    }

    [Test]
    public void EquipItemIncreasesDefense()
    {
        var inv = new Inventory();
        var item = new Item { id = "shield", slot = EquipmentSlot.Offhand, defense = 3 };
        inv.AddItem(item);
        inv.Equip(item);
        Assert.AreEqual(3, inv.Stats.defense);
    }
}
#endif
