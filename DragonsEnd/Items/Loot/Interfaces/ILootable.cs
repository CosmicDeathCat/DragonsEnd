using System.Collections.Generic;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot.Interfaces
{
    public interface ILootable
    {
        LootContainer? LootContainer { get; set; }

        LootContainer Loot
        (
            long minItemAmountDrop = -1L,
            long maxItemAmountDrop = -1L,
            long minGold = -1L,
            long maxGold = -1L,
            long combatExp = -1L,
            List<SkillExperience>? skillExperiences = null,
            params IItem[] specificLootableItems
        );
    }
}