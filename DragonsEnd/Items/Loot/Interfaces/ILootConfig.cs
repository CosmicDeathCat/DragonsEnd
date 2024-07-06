using System.Collections.Generic;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot.Interfaces
{
    public interface ILootConfig
    {
        long MinItemAmountDrop { get; set; }
        long MaxItemAmountDrop { get; set; }
        long MinGold { get; set; }
        long MaxGold { get; set; }
        long MinCombatExperience { get; set; }
        long MaxCombatExperience { get; set; }
        List<SkillExperience>? SkillExperiences { get; set; }
        List<IItem>? LootableItems { get; set; }
    }
}