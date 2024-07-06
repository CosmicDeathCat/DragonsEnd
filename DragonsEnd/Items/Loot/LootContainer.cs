using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Lists;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot
{
    [Serializable]
    public class LootContainer : ILootContainer
    {
        public GoldCurrency Gold { get; set; } = new(quantity: 0);
        public long CombatExperience { get; set; }
        public List<SkillExperience> SkillExperiences { get; set; } = new();
        public ItemList<IItem> Items { get; set; } = new();

        public LootContainer()
        {
            Items = new ItemList<IItem>();
            Gold = new GoldCurrency(quantity: 0);
            SkillExperiences = new List<SkillExperience>();
        }

        public LootContainer(GoldCurrency? gold, long combatExperience, List<SkillExperience> experiences, params IItem[] items)
        {
            gold ??= new GoldCurrency(quantity: 0);
            AddLoot(gold: gold, combatExperience: combatExperience, experiences: experiences, items: items);
        }

        public LootContainer(long gold, long combatExperience, List<SkillExperience> experiences, params IItem[] items)
        {
            AddLoot(gold: gold, combatExperience: combatExperience, experiences: experiences, items: items);
        }

        public LootContainer(LootContainer lootContainer)
        {
            AddLoot(lootContainer: lootContainer);
        }

        public void AddLoot(GoldCurrency? gold, long combatExperience, List<SkillExperience>? experiences, params IItem[] items)
        {
            gold ??= new GoldCurrency(quantity: 0);
            Gold.Add(otherCurrency: gold);
            CombatExperience += combatExperience;
            experiences ??= new List<SkillExperience>();
            foreach (var skillExperience in experiences)
            {
                skillExperience.GenerateRandomExperience();
                var skillExp = SkillExperiences.FirstOrDefault(predicate: x => x.SkillType == skillExperience.SkillType);
                if (skillExp.SkillType == skillExperience.SkillType)
                {
                    skillExp.Experience += skillExperience.Experience;
                }
                else
                {
                    SkillExperiences.Add(item: skillExperience);
                }
            }

            Items.AddRange(collection: items);
        }

        public void AddLoot(long gold, long combatExperience, List<SkillExperience>? experiences, params IItem[] items)
        {
            Gold.Add(otherCurrency: new GoldCurrency(quantity: gold));
            CombatExperience += combatExperience;
            experiences ??= new List<SkillExperience>();
            foreach (var skillExperience in experiences)
            {
                skillExperience.GenerateRandomExperience();
                var skillExp = SkillExperiences.FirstOrDefault(predicate: x => x.SkillType == skillExperience.SkillType);
                if (skillExp.SkillType == skillExperience.SkillType)
                {
                    skillExp.Experience += skillExperience.Experience;
                }
                else
                {
                    SkillExperiences.Add(item: skillExperience);
                }
            }

            Items.AddRange(collection: items);
        }

        public void AddLoot(ILootContainer? lootContainer)
        {
            if (lootContainer is null)
            {
                return;
            }

            Gold.Add(otherCurrency: lootContainer.Gold);
            CombatExperience += lootContainer.CombatExperience;
            foreach (var skillExperience in lootContainer.SkillExperiences)
            {
                skillExperience.GenerateRandomExperience();
                var skillExp = SkillExperiences.FirstOrDefault(predicate: x => x.SkillType == skillExperience.SkillType);
                if (skillExp.SkillType == skillExperience.SkillType)
                {
                    skillExp.Experience += skillExperience.Experience;
                }
                else
                {
                    SkillExperiences.Add(item: skillExperience);
                }
            }

            Items.AddRange(collection: lootContainer.Items);
        }

        public static LootContainer MergeLootContainers(List<ILootContainer?> lootContainers)
        {
            var result = new LootContainer();

            foreach (var lootContainer in lootContainers)
            {
                if (lootContainer == null)
                {
                    continue;
                }

                result.AddLoot(lootContainer: lootContainer);
            }

            return result;
        }

        public static LootContainer operator +(LootContainer lootContainer1, LootContainer lootContainer2)
        {
            lootContainer1.AddLoot(lootContainer: lootContainer2);
            return lootContainer1;
        }
    }
}