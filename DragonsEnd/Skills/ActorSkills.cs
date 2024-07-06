using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Skills.Combat.Magic;
using DragonsEnd.Skills.Combat.Melee;
using DragonsEnd.Skills.Combat.Ranged;
using DragonsEnd.Skills.Constants;
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
using DragonsEnd.Skills.NonCombat.GatheringSkills.Ranching;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Woodcutting;

namespace DragonsEnd.Skills
{
    public class ActorSkills : IActorSkills
    {
        public virtual IActor? Actor { get; set; }
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
        public virtual RanchingSkill RanchingSkill { get; set; }
        public virtual WoodcuttingSkill WoodcuttingSkill { get; set; }

        public ActorSkills()
        {
            Actor = null;
            MeleeSkill = new MeleeSkill(name: SkillNames.MeleeSkill, actor: Actor);
            RangedSkill = new RangedSkill(name: SkillNames.RangedSkill, actor: Actor);
            MagicSkill = new MagicSkill(name: SkillNames.MagicSkill, actor: Actor);
            AlchemySkill = new AlchemySkill(name: SkillNames.AlchemySkill, actor: Actor);
            CookingSkill = new CookingSkill(name: SkillNames.CookingSkill, actor: Actor);
            CraftingSkill = new CraftingSkill(name: SkillNames.CraftingSkill, actor: Actor);
            EnchantingSkill = new EnchantingSkill(name: SkillNames.EnchantingSkill, actor: Actor);
            FishingSkill = new FishingSkill(name: SkillNames.FishingSkill, actor: Actor);
            FletchingSkill = new FletchingSkill(name: SkillNames.FletchingSkill, actor: Actor);
            ForagingSkill = new ForagingSkill(name: SkillNames.ForagingSkill, actor: Actor);
            MiningSkill = new MiningSkill(name: SkillNames.MiningSkill, actor: Actor);
            SmithingSkill = new SmithingSkill(name: SkillNames.SmithingSkill, actor: Actor);
            RanchingSkill = new RanchingSkill(name: SkillNames.RanchingSkill, actor: Actor);
            WoodcuttingSkill = new WoodcuttingSkill(name: SkillNames.WoodcuttingSkill, actor: Actor);
        }

        public ActorSkills(IActor? actor)
        {
            Actor = actor;
            MeleeSkill = new MeleeSkill(name: SkillNames.MeleeSkill, actor: actor);
            RangedSkill = new RangedSkill(name: SkillNames.RangedSkill, actor: actor);
            MagicSkill = new MagicSkill(name: SkillNames.MagicSkill, actor: actor);
            AlchemySkill = new AlchemySkill(name: SkillNames.AlchemySkill, actor: actor);
            CookingSkill = new CookingSkill(name: SkillNames.CookingSkill, actor: actor);
            CraftingSkill = new CraftingSkill(name: SkillNames.CraftingSkill, actor: actor);
            EnchantingSkill = new EnchantingSkill(name: SkillNames.EnchantingSkill, actor: actor);
            FishingSkill = new FishingSkill(name: SkillNames.FishingSkill, actor: actor);
            FletchingSkill = new FletchingSkill(name: SkillNames.FletchingSkill, actor: actor);
            ForagingSkill = new ForagingSkill(name: SkillNames.ForagingSkill, actor: actor);
            MiningSkill = new MiningSkill(name: SkillNames.MiningSkill, actor: actor);
            SmithingSkill = new SmithingSkill(name: SkillNames.SmithingSkill, actor: actor);
            RanchingSkill = new RanchingSkill(name: SkillNames.RanchingSkill, actor: actor);
            WoodcuttingSkill = new WoodcuttingSkill(name: SkillNames.WoodcuttingSkill, actor: actor);
        }

        public ActorSkills
        (
            IActor? actor,
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
            RanchingSkill ranchingSkill,
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
            RanchingSkill = ranchingSkill;
            WoodcuttingSkill = woodcuttingSkill;
        }

        public virtual void UpdateActor(IActor actor)
        {
            Actor = actor;
            MeleeSkill.UpdateActor(actor: actor);
            RangedSkill.UpdateActor(actor: actor);
            MagicSkill.UpdateActor(actor: actor);
            AlchemySkill.UpdateActor(actor: actor);
            CookingSkill.UpdateActor(actor: actor);
            CraftingSkill.UpdateActor(actor: actor);
            EnchantingSkill.UpdateActor(actor: actor);
            FishingSkill.UpdateActor(actor: actor);
            FletchingSkill.UpdateActor(actor: actor);
            ForagingSkill.UpdateActor(actor: actor);
            MiningSkill.UpdateActor(actor: actor);
            SmithingSkill.UpdateActor(actor: actor);
            RanchingSkill.UpdateActor(actor: actor);
            WoodcuttingSkill.UpdateActor(actor: actor);
        }
    }
}