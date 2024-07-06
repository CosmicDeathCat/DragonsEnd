using System;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Messages;
using DragonsEnd.Items.Status.Interfaces;

namespace DragonsEnd.Items.Status
{
    [Serializable]
    public class LevelingItem : Item, ILevelingItem
    {
        public LevelingItem
        (
            string name,
            string description,
            long price,
            ItemType type,
            long meleeExperience = 0,
            long rangedExperience = 0,
            long magicExperience = 0,
            long alchemyExperience = 0,
            long cookingExperience = 0,
            long craftingExperience = 0,
            long enchantingExperience = 0,
            long fletchingExperience = 0,
            long smithingExperience = 0,
            long fishingExperience = 0,
            long foragingExperience = 0,
            long miningExperience = 0,
            long ranchingExperience = 0,
            long woodcuttingExperience = 0,
            int meleeLevel = 0,
            int rangedLevel = 0,
            int magicLevel = 0,
            int alchemyLevel = 0,
            int cookingLevel = 0,
            int craftingLevel = 0,
            int enchantingLevel = 0,
            int fletchingLevel = 0,
            int smithingLevel = 0,
            int fishingLevel = 0,
            int foragingLevel = 0,
            int miningLevel = 0,
            int ranchingLevel = 0,
            int woodcuttingLevel = 0,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        ) : base(name: name, description: description, price: price, type: type,
            stackable: stackable,
            quantity: quantity, dropRate: dropRate)
        {
            MeleeExperience = meleeExperience;
            RangedExperience = rangedExperience;
            MagicExperience = magicExperience;
            AlchemyExperience = alchemyExperience;
            CookingExperience = cookingExperience;
            CraftingExperience = craftingExperience;
            EnchantingExperience = enchantingExperience;
            FletchingExperience = fletchingExperience;
            SmithingExperience = smithingExperience;
            FishingExperience = fishingExperience;
            ForagingExperience = foragingExperience;
            MiningExperience = miningExperience;
            RanchingExperience = ranchingExperience;
            WoodcuttingExperience = woodcuttingExperience;
            MeleeLevel = meleeLevel;
            RangedLevel = rangedLevel;
            MagicLevel = magicLevel;
            AlchemyLevel = alchemyLevel;
            CookingLevel = cookingLevel;
            CraftingLevel = craftingLevel;
            EnchantingLevel = enchantingLevel;
            FletchingLevel = fletchingLevel;
            SmithingLevel = smithingLevel;
            FishingLevel = fishingLevel;
            ForagingLevel = foragingLevel;
            MiningLevel = miningLevel;
            RanchingLevel = ranchingLevel;
            WoodcuttingLevel = woodcuttingLevel;
        }

        public virtual long MeleeExperience { get; set; }
        public virtual long RangedExperience { get; set; }
        public virtual long MagicExperience { get; set; }
        public virtual long AlchemyExperience { get; set; }
        public virtual long CookingExperience { get; set; }
        public virtual long CraftingExperience { get; set; }
        public virtual long EnchantingExperience { get; set; }
        public virtual long FletchingExperience { get; set; }
        public virtual long SmithingExperience { get; set; }
        public virtual long FishingExperience { get; set; }
        public virtual long ForagingExperience { get; set; }
        public virtual long MiningExperience { get; set; }
        public virtual long RanchingExperience { get; set; }
        public virtual long WoodcuttingExperience { get; set; }
        public virtual int MeleeLevel { get; set; }
        public virtual int RangedLevel { get; set; }
        public virtual int MagicLevel { get; set; }
        public virtual int AlchemyLevel { get; set; }
        public virtual int CookingLevel { get; set; }
        public virtual int CraftingLevel { get; set; }
        public virtual int EnchantingLevel { get; set; }
        public virtual int FletchingLevel { get; set; }
        public virtual int SmithingLevel { get; set; }
        public virtual int FishingLevel { get; set; }
        public virtual int ForagingLevel { get; set; }
        public virtual int MiningLevel { get; set; }
        public virtual int RanchingLevel { get; set; }
        public virtual int WoodcuttingLevel { get; set; }

        public override void Use(IActor? source, IActor? target)
        {
            if (target != null)
            {
                target.ActorSkills.MeleeSkill.Leveling.GainExperience(amount: MeleeExperience);
                target.ActorSkills.RangedSkill.Leveling.GainExperience(amount: RangedExperience);
                target.ActorSkills.MagicSkill.Leveling.GainExperience(amount: MagicExperience);
                target.ActorSkills.AlchemySkill.Leveling.GainExperience(amount: AlchemyExperience);
                target.ActorSkills.CookingSkill.Leveling.GainExperience(amount: CookingExperience);
                target.ActorSkills.CraftingSkill.Leveling.GainExperience(amount: CraftingExperience);
                target.ActorSkills.EnchantingSkill.Leveling.GainExperience(amount: EnchantingExperience);
                target.ActorSkills.FletchingSkill.Leveling.GainExperience(amount: FletchingExperience);
                target.ActorSkills.SmithingSkill.Leveling.GainExperience(amount: SmithingExperience);
                target.ActorSkills.FishingSkill.Leveling.GainExperience(amount: FishingExperience);
                target.ActorSkills.ForagingSkill.Leveling.GainExperience(amount: ForagingExperience);
                target.ActorSkills.MiningSkill.Leveling.GainExperience(amount: MiningExperience);
                target.ActorSkills.RanchingSkill.Leveling.GainExperience(amount: RanchingExperience);
                target.ActorSkills.WoodcuttingSkill.Leveling.GainExperience(amount: WoodcuttingExperience);
                target.ActorSkills.MeleeSkill.Leveling.CurrentLevel += MeleeLevel;
                target.ActorSkills.RangedSkill.Leveling.CurrentLevel += RangedLevel;
                target.ActorSkills.MagicSkill.Leveling.CurrentLevel += MagicLevel;
                target.ActorSkills.AlchemySkill.Leveling.CurrentLevel += AlchemyLevel;
                target.ActorSkills.CookingSkill.Leveling.CurrentLevel += CookingLevel;
                target.ActorSkills.CraftingSkill.Leveling.CurrentLevel += CraftingLevel;
                target.ActorSkills.EnchantingSkill.Leveling.CurrentLevel += EnchantingLevel;
                target.ActorSkills.FletchingSkill.Leveling.CurrentLevel += FletchingLevel;
                target.ActorSkills.SmithingSkill.Leveling.CurrentLevel += SmithingLevel;
                target.ActorSkills.FishingSkill.Leveling.CurrentLevel += FishingLevel;
                target.ActorSkills.ForagingSkill.Leveling.CurrentLevel += ForagingLevel;
                target.ActorSkills.MiningSkill.Leveling.CurrentLevel += MiningLevel;
                target.ActorSkills.RanchingSkill.Leveling.CurrentLevel += RanchingLevel;
                target.ActorSkills.WoodcuttingSkill.Leveling.CurrentLevel += WoodcuttingLevel;
            }

            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Items,
                message: new ItemMessage(item: this, source: source, target: target));
        }

        public override IItem Copy()
        {
            return new LevelingItem(
                name: Name,
                description: Description,
                price: Price,
                type: Type,
                meleeExperience: MeleeExperience,
                rangedExperience: RangedExperience,
                magicExperience: MagicExperience,
                alchemyExperience: AlchemyExperience,
                cookingExperience: CookingExperience,
                craftingExperience: CraftingExperience,
                enchantingExperience: EnchantingExperience,
                fletchingExperience: FletchingExperience,
                smithingExperience: SmithingExperience,
                fishingExperience: FishingExperience,
                foragingExperience: ForagingExperience,
                miningExperience: MiningExperience,
                ranchingExperience: RanchingExperience,
                woodcuttingExperience: WoodcuttingExperience,
                meleeLevel: MeleeLevel,
                rangedLevel: RangedLevel,
                magicLevel: MagicLevel,
                alchemyLevel: AlchemyLevel,
                cookingLevel: CookingLevel,
                craftingLevel: CraftingLevel,
                enchantingLevel: EnchantingLevel,
                fletchingLevel: FletchingLevel,
                smithingLevel: SmithingLevel,
                fishingLevel: FishingLevel,
                foragingLevel: ForagingLevel,
                miningLevel: MiningLevel,
                ranchingLevel: RanchingLevel,
                woodcuttingLevel: WoodcuttingLevel,
                stackable: Stackable,
                quantity: Quantity
            );
        }
    }
}