using System.Collections.Generic;
using WildQuest.Interfaces;
using WildQuest.Items.Currency;
using WildQuest.Items.Currency.Extensions;

namespace WildQuest.Items.Loot;

[System.Serializable]
public class LootContainer
{
    public GoldCurrency Gold { get; set; } = new(0);
    public long Experience { get; set; }
    public List<IItem> Items { get; set; } = new();

    public LootContainer()
    {
        Items = new List<IItem>();
        Gold = new GoldCurrency(0);
        Items = new List<IItem>();
    }
    
    public LootContainer(GoldCurrency? gold, long experience, params IItem[] items)
    {
        gold ??= new GoldCurrency(0);
        AddLoot(gold, experience, items);
    }
    
    public LootContainer(long gold, long experience, params IItem[] items)
    {
        AddLoot(gold, experience, items);
    }
    
    public LootContainer(LootContainer lootContainer)
    {
        AddLoot(lootContainer);
    }

    public void AddLoot(GoldCurrency? gold, long experience, params IItem[] items)
    {
        gold ??= new GoldCurrency(0);
        Gold.Add(gold);
        Experience += experience;
        Items.AddRange(items);
    }
    
    public void AddLoot(long gold, long experience, params IItem[] items)
    {
        Gold.Add(new GoldCurrency(gold));
        Experience += experience;
        Items.AddRange(items);
    }
    
    public void AddLoot(LootContainer lootContainer)
    {
        Gold.Add(lootContainer.Gold);
        Experience += lootContainer.Experience;
        Items.AddRange(lootContainer.Items);
    }
}