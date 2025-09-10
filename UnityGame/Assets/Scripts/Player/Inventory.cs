using System.Collections.Generic;

public class Inventory
{
    private readonly List<Item> _items = new();
    private readonly Dictionary<EquipmentSlot, Item> _equipped = new();
    private readonly PlayerStats _stats = new();

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public bool Equip(Item item)
    {
        if (!_items.Contains(item))
            return false;
        _equipped[item.slot] = item;
        _stats.attack += item.attack;
        _stats.defense += item.defense;
        return true;
    }

    public bool Contains(Item item) => _items.Contains(item);
    public PlayerStats Stats => _stats;
}

public class Item
{
    public string id;
    public EquipmentSlot slot;
    public int attack;
    public int defense;
}

public class PlayerStats
{
    public int attack;
    public int defense;
}
