using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Status.Interfaces
{
    public interface ILevelingItem :IItem
    {
        long MeleeExperience { get; set; }
        long RangedExperience { get; set; }
        long MagicExperience { get; set; }
        long AlchemyExperience { get; set; }
        long CookingExperience { get; set; }
        long CraftingExperience { get; set; }
        long EnchantingExperience { get; set; }
        long FletchingExperience { get; set; }
        long SmithingExperience { get; set; }
        long FishingExperience { get; set; }
        long ForagingExperience { get; set; }
        long MiningExperience { get; set; }
        long RanchingExperience { get; set; }
        long WoodcuttingExperience { get; set; }
        int MeleeLevel { get; set; }
        int RangedLevel { get; set; }
        int MagicLevel { get; set; }
        int AlchemyLevel { get; set; }
        int CookingLevel { get; set; }
        int CraftingLevel { get; set; }
        int EnchantingLevel { get; set; }
        int FletchingLevel { get; set; }
        int SmithingLevel { get; set; }
        int FishingLevel { get; set; }
        int ForagingLevel { get; set; }
        int MiningLevel { get; set; }
        int RanchingLevel { get; set; }
        int WoodcuttingLevel { get; set; }
    }
}