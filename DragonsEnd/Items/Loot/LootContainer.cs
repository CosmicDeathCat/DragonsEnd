using System;
using System.Collections.Generic;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Loot
{
    [Serializable]
    public class LootContainer
    {
        public LootContainer()
        {
            Items = new List<IItem>();
            Gold = new GoldCurrency(quantity: 0);
            Items = new List<IItem>();
        }

        public LootContainer(GoldCurrency? gold, long experience, params IItem[] items)
        {
            gold ??= new GoldCurrency(quantity: 0);
            AddLoot(gold: gold, experience: experience, items: items);
        }

        public LootContainer(long gold, long experience, params IItem[] items)
        {
            AddLoot(gold: gold, experience: experience, items: items);
        }

        public LootContainer(LootContainer lootContainer)
        {
            AddLoot(lootContainer: lootContainer);
        }

        public GoldCurrency Gold { get; set; } = new(quantity: 0);
        public long Experience { get; set; }
        public List<IItem> Items { get; set; } = new();

        public void AddLoot(GoldCurrency? gold, long experience, params IItem[] items)
        {
            gold ??= new GoldCurrency(quantity: 0);
            Gold.Add(otherCurrency: gold);
            Experience += experience;
            Items.AddRange(collection: items);
        }

        public void AddLoot(long gold, long experience, params IItem[] items)
        {
            Gold.Add(otherCurrency: new GoldCurrency(quantity: gold));
            Experience += experience;
            Items.AddRange(collection: items);
        }

        public void AddLoot(LootContainer lootContainer)
        {
            Gold.Add(otherCurrency: lootContainer.Gold);
            Experience += lootContainer.Experience;
            Items.AddRange(collection: lootContainer.Items);
        }
    }
}