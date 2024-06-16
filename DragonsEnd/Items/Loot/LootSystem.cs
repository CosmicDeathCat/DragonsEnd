using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Drops;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Utility.Extensions.Random;

namespace DragonsEnd.Items.Loot
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
            var gold = rnd.NextInt64(minGold, maxGold + 1);
            var experience = rnd.NextInt64(minExperience, maxExperience + 1);
            var itemAmount = rnd.NextInt64(minItemAmountDrop, maxItemAmountDrop + 1);
            var items = new List<IItem>();

            for (var i = 0; i < itemAmount; i++)
            {
                var cumulativeProbability = 0.0;
                var diceRoll = rnd.NextDouble();

                foreach (var dropItem in lootableItems.OrderBy(x => x.DropRate))
                {
                    cumulativeProbability += dropItem.DropRate;
                    if (items.Exists(x => x.Name.Equals(dropItem.Item.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

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
            ILootable lootedObject,
            long minItemAmountDrop = -1L,
            long maxItemAmountDrop = -1L,
            long minGold = -1L,
            long maxGold = -1L,
            long minExperience = -1L,
            long maxExperience = -1L,
            params IDropItem[] specificLootableItems)
        {
            var lootableItems = specificLootableItems.ToList();
            lootableItems.AddRange(lootedObject.DropItems);

            if (lootableItems.Count == 0)
            {
                if (lootedObject is IActor actor)
                {
                    // Default to using the actor's inventory and equipment as drop items with default drop rates
                    foreach (var item in actor.Inventory)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        lootableItems.Add(new DropItem(item, item.DropRate));
                    }

                    foreach (var item in actor.Equipment)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        lootableItems.Add(new DropItem(item, item.DropRate));
                    }
                }
            }

            // Set default values if -1 is provided
            if (minItemAmountDrop == -1)
            {
                minItemAmountDrop = 0;
            }

            if (maxItemAmountDrop == -1)
            {
                maxItemAmountDrop = lootableItems.Count;
            }

            if (minGold == -1)
            {
                minGold = lootedObject.Gold.CurrentValue;
            }

            if (maxGold == -1)
            {
                maxGold = lootedObject.Gold.CurrentValue;
            }

            if (minExperience == -1)
            {
                minExperience = lootedObject.Leveling.Experience;
            }

            if (maxExperience == -1)
            {
                maxExperience = lootedObject.Leveling.Experience;
            }

            return GenerateLoot(
                minItemAmountDrop,
                maxItemAmountDrop,
                minGold,
                maxGold,
                minExperience,
                maxExperience,
                lootableItems.ToArray());
        }
    }
}