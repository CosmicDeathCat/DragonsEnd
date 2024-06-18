using System.Collections.Generic;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Leveling.Interfaces;

namespace DragonsEnd.Items.Loot.Interfaces
{
    public interface ILootable
    {
        List<IDropItem> DropItems { get; set; }
        GoldCurrency Gold { get; set; }
        ILeveling Leveling { get; set; }

        LootContainer Loot(
            long minItemAmountDrop = -1L,
            long maxItemAmountDrop = -1L,
            long minGold = -1L,
            long maxGold = -1L,
            long minExperience = -1L,
            long maxExperience = -1L,
            params IDropItem[] specificLootableItems);
    }
}