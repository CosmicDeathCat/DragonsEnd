using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Skills.Combat;
using DragonsEnd.Skills.NonCombat;

namespace DragonsEnd.Skills.Interfaces
{
    public interface IActorSkills
    {
        IActor Actor { get; set; }
        MeleeSkill MeleeSkill { get; set; }
        RangedSkill RangedSkill { get; set; }
        MagicSkill MagicSkill { get; set; }
        AlchemySkill AlchemySkill { get; set; }
        CookingSkill CookingSkill { get; set; }
        CraftingSkill CraftingSkill { get; set; }
        EnchantingSkill EnchantingSkill { get; set; }
        FishingSkill FishingSkill { get; set; }
        FletchingSkill FletchingSkill { get; set; }
        ForagingSkill ForagingSkill { get; set; }
        MiningSkill MiningSkill { get; set; }
        SmithingSkill SmithingSkill { get; set; }
        WoodcuttingSkill WoodcuttingSkill { get; set; }
    }
}