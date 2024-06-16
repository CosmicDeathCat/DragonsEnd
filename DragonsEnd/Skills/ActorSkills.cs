using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Skills.Combat;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Skills.NonCombat;

namespace DragonsEnd.Skills
{
    public class ActorSkills : IActorSkills
    {
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

        public ActorSkills(IActor actor)
        {
            Actor = actor;
            MeleeSkill = new MeleeSkill("Melee", actor, 20);
            RangedSkill = new RangedSkill("Ranged", actor, 20);
            MagicSkill = new MagicSkill("Magic", actor, 20);
            AlchemySkill = new AlchemySkill("Alchemy", actor, 20);
            CookingSkill = new CookingSkill("Cooking", actor, 20);
            CraftingSkill = new CraftingSkill("Crafting", actor, 20);
            EnchantingSkill = new EnchantingSkill("Enchanting", actor, 20);
            FishingSkill = new FishingSkill("Fishing", actor, 20);
            FletchingSkill = new FletchingSkill("Fletching", actor, 20);
            ForagingSkill = new ForagingSkill("Foraging", actor, 20);
            MiningSkill = new MiningSkill("Mining", actor, 20);
            SmithingSkill = new SmithingSkill("Smithing", actor, 20);
            WoodcuttingSkill = new WoodcuttingSkill("Woodcutting", actor, 20);
        }
        
        public ActorSkills(IActor actor, MeleeSkill meleeSkill, RangedSkill rangedSkill, MagicSkill magicSkill, AlchemySkill alchemySkill, CookingSkill cookingSkill, CraftingSkill craftingSkill, EnchantingSkill enchantingSkill, FishingSkill fishingSkill, FletchingSkill fletchingSkill, ForagingSkill foragingSkill, MiningSkill miningSkill, SmithingSkill smithingSkill, WoodcuttingSkill woodcuttingSkill)
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
    }
}