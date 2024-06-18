using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Skills.Combat.Magic;
using DragonsEnd.Skills.Combat.Melee;
using DragonsEnd.Skills.Combat.Ranged;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Alchemy;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Cooking;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Crafting;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Enchanting;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Fletching;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Smithing;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Fishing;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Foraging;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Mining;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Woodcutting;

namespace DragonsEnd.Skills
{
    public class ActorSkills : IActorSkills
    {
        public ActorSkills(IActor actor)
        {
            Actor = actor;
            MeleeSkill = new MeleeSkill(name: "Melee", actor: actor);
            RangedSkill = new RangedSkill(name: "Ranged", actor: actor);
            MagicSkill = new MagicSkill(name: "Magic", actor: actor);
            AlchemySkill = new AlchemySkill(name: "Alchemy", actor: actor);
            CookingSkill = new CookingSkill(name: "Cooking", actor: actor);
            CraftingSkill = new CraftingSkill(name: "Crafting", actor: actor);
            EnchantingSkill = new EnchantingSkill(name: "Enchanting", actor: actor);
            FishingSkill = new FishingSkill(name: "Fishing", actor: actor);
            FletchingSkill = new FletchingSkill(name: "Fletching", actor: actor);
            ForagingSkill = new ForagingSkill(name: "Foraging", actor: actor);
            MiningSkill = new MiningSkill(name: "Mining", actor: actor);
            SmithingSkill = new SmithingSkill(name: "Smithing", actor: actor);
            WoodcuttingSkill = new WoodcuttingSkill(name: "Woodcutting", actor: actor);
        }

        public ActorSkills
        (
            IActor actor,
            MeleeSkill meleeSkill,
            RangedSkill rangedSkill,
            MagicSkill magicSkill,
            AlchemySkill alchemySkill,
            CookingSkill cookingSkill,
            CraftingSkill craftingSkill,
            EnchantingSkill enchantingSkill,
            FishingSkill fishingSkill,
            FletchingSkill fletchingSkill,
            ForagingSkill foragingSkill,
            MiningSkill miningSkill,
            SmithingSkill smithingSkill,
            WoodcuttingSkill woodcuttingSkill
        )
        {
            Actor = actor;
            MeleeSkill = meleeSkill;
            RangedSkill = rangedSkill;
            MagicSkill = magicSkill;
            AlchemySkill = alchemySkill;
            CookingSkill = cookingSkill;
            CraftingSkill = craftingSkill;
            EnchantingSkill = enchantingSkill;
            FishingSkill = fishingSkill;
            FletchingSkill = fletchingSkill;
            ForagingSkill = foragingSkill;
            MiningSkill = miningSkill;
            SmithingSkill = smithingSkill;
            WoodcuttingSkill = woodcuttingSkill;
        }

        public virtual IActor Actor { get; set; }
        public virtual MeleeSkill MeleeSkill { get; set; }
        public virtual RangedSkill RangedSkill { get; set; }
        public virtual MagicSkill MagicSkill { get; set; }
        public virtual AlchemySkill AlchemySkill { get; set; }
        public virtual CookingSkill CookingSkill { get; set; }
        public virtual CraftingSkill CraftingSkill { get; set; }
        public virtual EnchantingSkill EnchantingSkill { get; set; }
        public virtual FishingSkill FishingSkill { get; set; }
        public virtual FletchingSkill FletchingSkill { get; set; }
        public virtual ForagingSkill ForagingSkill { get; set; }
        public virtual MiningSkill MiningSkill { get; set; }
        public virtual SmithingSkill SmithingSkill { get; set; }
        public virtual WoodcuttingSkill WoodcuttingSkill { get; set; }
    }
}