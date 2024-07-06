using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills;
using DragonsEnd.Utility.Extensions.Random;

namespace DragonsEnd.Items.Loot
{
    public class LootSystem
    {
        public static (long gold, long combatExperience, List<SkillExperience> skillExperiences, List<IItem> items) GenerateLoot(ILootable lootedObject, ILootConfig? lootConfig)
        {
            if (lootConfig == null)
            {
                return GenerateLoot(lootedObject: lootedObject);
            }
            
            return GenerateLoot(
                lootedObject: lootedObject,
                minItemAmountDrop: lootConfig.MinItemAmountDrop,
                maxItemAmountDrop: lootConfig.MaxItemAmountDrop,
                minGold: lootConfig.MinGold,
                maxGold: lootConfig.MaxGold,
                minCombatExp: lootConfig.MinCombatExperience,
                maxCombatExp: lootConfig.MaxCombatExperience,
                skillExperiences: lootConfig.SkillExperiences,
                lootableItems: lootConfig.LootableItems);
        }
        
        public static (long gold, long combatExperience, List<SkillExperience> skillExperiences, List<IItem> items) GenerateLoot
        (
            long minItemAmountDrop,
            long maxItemAmountDrop,
            long minGold,
            long maxGold,
            long minCombatExp,
            long maxCombatExp,
            List<SkillExperience>? skillExperiences,
            List<IItem>? lootableItems
        )
        {
            var rnd = new Random();
            var gold = rnd.NextInt64(minValue: minGold, maxValue: maxGold + 1);
            var combatExperience = rnd.NextInt64(minValue: minCombatExp, maxValue: maxCombatExp + 1);;
            var skillExps = skillExperiences;

            var itemAmount = rnd.NextInt64(minValue: minItemAmountDrop, maxValue: maxItemAmountDrop + 1);
            var items = new List<IItem>();
            
            if(lootableItems == null)
            {
                return (gold, combatExperience, skillExps, items);
            }
            
            for (var i = 0; i < itemAmount; i++)
            {
                var cumulativeProbability = 0.0;
                var diceRoll = rnd.NextDouble();

                foreach (var dropItem in lootableItems.OrderBy(keySelector: x => x.DropRate))
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
            long minCombatExp = -1L,
            long maxCombatExp = -1L,
            List<SkillExperience>? skillExperiences = null,
            List<IItem>? lootableItems = null
        )
        {
            lootedObject.LootConfig ??= new LootConfig();

            lootableItems ??= new List<IItem>();

            if (lootedObject.LootContainer != null)
            {
                lootableItems.AddRange(collection: lootedObject.LootContainer.Items);
            }

            if (lootedObject is IActor actor)
            {
                if (lootableItems?.Count == 0)
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
                    minGold = lootedObject.LootContainer != null ? lootedObject.LootContainer.Gold.CurrentValue : actor.Inventory.Gold.CurrentValue;
                }

                if (maxGold == -1)
                {
                    maxGold = lootedObject.LootContainer != null ? lootedObject.LootContainer.Gold.CurrentValue : actor.Inventory.Gold.CurrentValue;
                }

                if (minCombatExp == -1)
                {
                    if (lootedObject.LootContainer != null)
                    {
                        minCombatExp = lootedObject.LootContainer.CombatExperience;
                    }
                    else
                    {
                        minCombatExp = (actor.ActorSkills.MeleeSkill.Leveling.Experience + actor.ActorSkills.RangedSkill.Leveling.Experience + actor.ActorSkills.MagicSkill.Leveling.Experience) / 3;
                    }
                }

                if (maxCombatExp == -1)
                {
                    if (lootedObject.LootContainer != null)
                    {
                        maxCombatExp = lootedObject.LootContainer.CombatExperience;
                    }
                    else
                    {
                        maxCombatExp = (actor.ActorSkills.MeleeSkill.Leveling.Experience + actor.ActorSkills.RangedSkill.Leveling.Experience + actor.ActorSkills.MagicSkill.Leveling.Experience) / 3;
                    }

                }

                if (skillExperiences == null)
                {
                    if (lootedObject.LootContainer != null)
                    {
                        skillExperiences = lootedObject.LootContainer.SkillExperiences;
                    }
                    else
                    {
                        skillExperiences = new List<SkillExperience>();
                    
                    }
                }
            }

            return GenerateLoot(
                minItemAmountDrop: minItemAmountDrop,
                maxItemAmountDrop: maxItemAmountDrop,
                minGold: minGold,
                maxGold: maxGold,
                minCombatExp: minCombatExp,
                maxCombatExp: maxCombatExp,
                skillExperiences: skillExperiences,
                lootableItems: lootableItems);
        }
        
        
    }
}