using System;
using System.Collections.Generic;
using DragonsEnd.Enums;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot
{
    [Serializable]
    public class LootContainer
    {
        public LootContainer()
        {
            Items = new List<IItem>();
            Gold = new GoldCurrency(quantity: 0);
        }
        
        public LootContainer(GoldCurrency? gold, long combatExperience,List<SkillExperience> experiences, params IItem[] items)
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

        public GoldCurrency Gold { get; set; } = new(quantity: 0);
        public long CombatExperience { get; set; }
        public List<SkillExperience> SkillExperiences { get; set; }
        public List<IItem> Items { get; set; } = new();

        public void AddLoot(GoldCurrency? gold, long combatExperience, List<SkillExperience> experiences, params IItem[] items)
        {
            gold ??= new GoldCurrency(quantity: 0);
            Gold.Add(otherCurrency: gold);
            CombatExperience = combatExperience;
            SkillExperiences = experiences;
            Items.AddRange(collection: items);
        }
        
        public void AddLoot(long gold, long combatExperience, List<SkillExperience> experiences, params IItem[] items)
        {
            Gold.Add(otherCurrency: new GoldCurrency(quantity: gold));
            CombatExperience = combatExperience;
            SkillExperiences = experiences;
            Items.AddRange(collection: items);
        }
        
        public void AddLoot(LootContainer lootContainer)
        {
            Gold.Add(otherCurrency: lootContainer.Gold);
            CombatExperience = lootContainer.CombatExperience;
            SkillExperiences = lootContainer.SkillExperiences;
            Items.AddRange(collection: lootContainer.Items);
        }
        
    }
}