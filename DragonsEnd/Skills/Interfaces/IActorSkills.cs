using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Skills.Combat.Magic;
using DragonsEnd.Skills.Combat.Melee;
using DragonsEnd.Skills.Combat.Ranged;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Alchemy;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Cooking;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Crafting;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Enchanting;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Fletching;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Smithing;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Fishing;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Foraging;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Mining;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Ranching;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Woodcutting;

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
        RanchingSkill RanchingSkill { get; set; }
        WoodcuttingSkill WoodcuttingSkill { get; set; }
    }
}