using System.Collections.Generic;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Lists;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot.Interfaces
{
    public interface ILootContainer
    {
        GoldCurrency Gold { get; set; }
        long CombatExperience { get; set; }
        List<SkillExperience> SkillExperiences { get; set; }
        ItemList<IItem> Items { get; set; }

        void AddLoot(GoldCurrency? gold, long combatExperience, List<SkillExperience> experiences, params IItem[] items);

        void AddLoot(long gold, long combatExperience, List<SkillExperience> experiences, params IItem[] items);

        void AddLoot(ILootContainer? lootContainer);
    }
}