using System;
using System.Collections.Generic;
using System.Linq;
using WildQuest.Interfaces;

namespace WildQuest.Items.Loot
{
    public class LootSystem
    {
        public static (long gold, long experience, List<IItem> items) GenerateLoot(
            long minItemAmountDrop, 
            long maxItemAmountDrop, 
            long minGold, 
            long maxGold, 
            long minExperience, 
            long maxExperience, 
            params IDropItem[] lootableItems)
        {
            var rnd = new Random();
            long gold = rnd.NextInt64(minGold, maxGold + 1);
            long experience = rnd.NextInt64(minExperience, maxExperience + 1);
            long itemAmount = rnd.NextInt64(minItemAmountDrop, maxItemAmountDrop + 1);
            var items = new List<IItem>();

            for (int i = 0; i < itemAmount; i++)
            {
                var cumulativeProbability = 0.0;
                var diceRoll = rnd.NextDouble();

                foreach (var dropItem in lootableItems.OrderBy(x=> x.DropRate))
                {
                    cumulativeProbability += dropItem.DropRate;
                    if (items.Exists(x => x.Name.Equals(dropItem.Item.Name, StringComparison.OrdinalIgnoreCase))) continue;
                    if (diceRoll <= cumulativeProbability)
                    {
                        var item = dropItem.Item.Copy();
                        items.Add(item);
                        break;
                    }
                }
            }

            return (gold, experience, items);
        }

        public static (long gold, long experience, List<IItem> items) GenerateLoot(
            IActor actor, 
            long minItemAmountDrop = -1L, 
            long maxItemAmountDrop = -1L, 
            long minGold = -1L, 
            long maxGold = -1L, 
            long minExperience = -1L, 
            long maxExperience = -1L, 
            params IDropItem[] specificLootableItems)
        {
            var lootableItems = specificLootableItems.ToList();

            if (actor is ILootable lootable)
            {
                lootableItems.AddRange(lootable.DropItems);
            }
            
            if (lootableItems.Count == 0)
            {
                // Default to using the actor's inventory and equipment as drop items with default drop rates
                foreach (var item in actor.Inventory)
                {
                    if (item == null) continue;
                    lootableItems.Add(new DropItem(item, item.DropRate));
                }

                foreach (var item in actor.Equipment)
                {
                    if (item == null) continue;
                    lootableItems.Add(new DropItem(item, item.DropRate));
                }
            }

            // Set default values if -1 is provided
            if (minItemAmountDrop == -1) minItemAmountDrop = 0;
            if (maxItemAmountDrop == -1) maxItemAmountDrop = lootableItems.Count;
            if (minGold == -1) minGold = actor.Gold.CurrentValue;
            if (maxGold == -1) maxGold = actor.Gold.CurrentValue;
            if (minExperience == -1) minExperience = actor.Leveling.Experience;
            if (maxExperience == -1) maxExperience = actor.Leveling.Experience;

            return GenerateLoot(
                minItemAmountDrop: minItemAmountDrop,
                maxItemAmountDrop: maxItemAmountDrop,
                minGold: minGold,
                maxGold: maxGold,
                minExperience: minExperience,
                maxExperience: maxExperience,
                lootableItems: lootableItems.ToArray());
        }
    }
}
