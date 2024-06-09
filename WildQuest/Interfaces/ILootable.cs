using System.Collections.Generic;
using WildQuest.Items.Currency;
using WildQuest.Items.Loot;

namespace WildQuest.Interfaces;

public interface ILootable
{
    List<IDropItem> DropItems { get; set; }
    GoldCurrency Gold { get; set; }
    ILeveling Leveling {get;set;}

    LootContainer Loot(
        long minItemAmountDrop = -1L,
        long maxItemAmountDrop = -1L,
        long minGold = -1L,
        long maxGold = -1L,
        long minExperience = -1L,
        long maxExperience = -1L,
        params IDropItem[] specificLootableItems);
}