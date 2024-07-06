using System.Collections.Generic;
using DragonsEnd.Identity.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot.Interfaces
{
    public interface ILootable : IIdentity
    {
        ILootContainer? LootContainer { get; set; }
        bool HasAlreadyBeenLooted { get; set; }

        ILootContainer? Loot
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