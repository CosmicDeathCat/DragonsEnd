using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills;
using DragonsEnd.Utility.Extensions.Random;

namespace DragonsEnd.Items.Loot
{
    public class LootSystem
    {
        public static (long gold, long combatExperience, List<SkillExperience> skillExperiences, List<IItem> items) GenerateLoot
        (
            long minItemAmountDrop,
            long maxItemAmountDrop,
            long minGold,
            long maxGold,
            long combatExp,
            List<SkillExperience>? skillExperiences,
            params IItem[] specificLootableItems
        )
        {
            var rnd = new Random();
            var gold = rnd.NextInt64(minValue: minGold, maxValue: maxGold + 1);
            var combatExperience = combatExp;
            var skillExps = skillExperiences;

            var itemAmount = rnd.NextInt64(minValue: minItemAmountDrop, maxValue: maxItemAmountDrop + 1);
            var items = new List<IItem>();

            for (var i = 0; i < itemAmount; i++)
            {
                var cumulativeProbability = 0.0;
                var diceRoll = rnd.NextDouble();

                foreach (var dropItem in specificLootableItems.OrderBy(keySelector: x => x.DropRate))
                {
                    cumulativeProbability += dropItem.DropRate;
                    if (items.Exists(match: x => x.Name.Equals(value: dropItem.Name, comparisonType: StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    if (diceRoll <= cumulativeProbability)
                    {
                        var item = dropItem.Copy();
                        items.Add(item: item);
                        break;
                    }
                }
            }

            return (gold, combatExperience, skillExps, items);
        }

        public static (long gold, long combatExperience, List<SkillExperience> skillExperiences, List<IItem> items) GenerateLoot
        (
            ILootable lootedObject,
            long minItemAmountDrop = -1L,
            long maxItemAmountDrop = -1L,
            long minGold = -1L,
            long maxGold = -1L,
            long combatExp = -1L,
            List<SkillExperience>? skillExperiences = null,
            params IItem[] specificLootableItems
        )
        {
            var lootableItems = specificLootableItems.ToList();
            lootedObject.LootContainer ??= new LootContainer();
            lootableItems.AddRange(collection: lootedObject.LootContainer.Items);

            if (lootableItems.Count == 0)
            {
                if (lootedObject is IActor actor)
                {
                    // Default to using the actor's inventory and equipment as drop items with default drop rates
                    foreach (var item in actor.Inventory.Items)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        lootableItems.Add(item: new Item(item: item));
                    }

                    foreach (var item in actor.Equipment)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        lootableItems.Add(item: new Item(item: item));
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
                minGold = lootedObject.LootContainer.Gold.CurrentValue;
            }

            if (maxGold == -1)
            {
                maxGold = lootedObject.LootContainer.Gold.CurrentValue;
            }

            if (combatExp == -1)
            {
                combatExp = lootedObject.LootContainer.CombatExperience;
            }

            if (skillExperiences == null)
            {
                skillExperiences = lootedObject.LootContainer.SkillExperiences;
            }

            return GenerateLoot(
                minItemAmountDrop: minItemAmountDrop,
                maxItemAmountDrop: maxItemAmountDrop,
                minGold: minGold,
                maxGold: maxGold,
                combatExp: combatExp,
                skillExperiences: skillExperiences,
                specificLootableItems: lootableItems.ToArray());
        }
    }
}