using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Crafting.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.CraftingSkills.Crafting
{
    public class CraftingSkill : BaseNonCombatSkill
    {
        public override SkillType SkillType { get => SkillType.Crafting; }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ItemNames.FeebleFeatherCap, description: "You can now craft Feeble Feather Caps!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleLeatherVest, description: "You can now craft Feeble Leather Vests!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleLeatherGloves, description: "You can now craft Feeble Leather Gloves!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleLeatherBoots, description: "You can now craft Feeble Leather Boots!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleBuckler, description: "You can now craft Feeble Bucklers!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.BeginnerCrafter, description: "You've earned the title of Beginner Crafter!"),
                new BasicSkillUnlock(name: ItemNames.WeakFeatherCap, description: "You can now craft Weak Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.WeakLeatherVest, description: "You can now craft Weak Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.WeakLeatherGloves, description: "You can now craft Weak Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.WeakLeatherBoots, description: "You can now craft Weak Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.WeakBuckler, description: "You can now craft Weak Bucklers!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.NoviceCrafter, description: "You've earned the title of Novice Crafter!"),
                new BasicSkillUnlock(name: ItemNames.StrongFeatherCap, description: "You can now craft Strong Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.StrongLeatherVest, description: "You can now craft Strong Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.StrongLeatherGloves, description: "You can now craft Strong Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.StrongLeatherBoots, description: "You can now craft Strong Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.StrongBuckler, description: "You can now craft Strong Bucklers!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.ApprenticeCrafter, description: "You've earned the title of Apprentice Crafter!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedFeatherCap, description: "You can now craft Reinforced Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedLeatherVest, description: "You can now craft Reinforced Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedLeatherGloves, description: "You can now craft Reinforced Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedLeatherBoots, description: "You can now craft Reinforced Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedBuckler, description: "You can now craft Reinforced Bucklers!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.JourneymanCrafter, description: "You've earned the title of Journeyman Crafter!"),
                new BasicSkillUnlock(name: ItemNames.HardenedFeatherCap, description: "You can now craft Hardened Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.HardenedLeatherVest, description: "You can now craft Hardened Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.HardenedLeatherGloves, description: "You can now craft Hardened Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.HardenedLeatherBoots, description: "You can now craft Hardened Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.HardenedBuckler, description: "You can now craft Hardened Bucklers!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.ExpertCrafter, description: "You've earned the title of Expert Crafter!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterFeatherCap, description: "You can now craft Sharpshooter Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterLeatherVest, description: "You can now craft Sharpshooter Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterLeatherGloves, description: "You can now craft Sharpshooter Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterLeatherBoots, description: "You can now craft Sharpshooter Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterBuckler, description: "You can now craft Sharpshooter Bucklers!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.AdeptCrafter, description: "You've earned the title of Adept Crafter!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionFeatherCap, description: "You can now craft Precision Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionLeatherVest, description: "You can now craft Precision Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionLeatherGloves, description: "You can now craft Precision Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionLeatherBoots, description: "You can now craft Precision Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionBuckler, description: "You can now craft Precision Bucklers!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.VeteranCrafter, description: "You've earned the title of Veteran Crafter!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanFeatherCap, description: "You can now craft Marksman Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanLeatherVest, description: "You can now craft Marksman Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanLeatherGloves, description: "You can now craft Marksman Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanLeatherBoots, description: "You can now craft Marksman Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanBuckler, description: "You can now craft Marksman Bucklers!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.MasterCrafter, description: "You've earned the title of Master Crafter!"),
                new BasicSkillUnlock(name: ItemNames.PhantomFeatherCap, description: "You can now craft Phantom Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.PhantomLeatherVest, description: "You can now craft Phantom Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.PhantomLeatherGloves, description: "You can now craft Phantom Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.PhantomLeatherBoots, description: "You can now craft Phantom Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.PhantomBuckler, description: "You can now craft Phantom Bucklers!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.IllustriousCrafter, description: "You've earned the title of Illustrious Crafter!"),
                new BasicSkillUnlock(name: ItemNames.CrystalFeatherCap, description: "You can now craft Crystal Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.CrystalLeatherVest, description: "You can now craft Crystal Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.CrystalLeatherGloves, description: "You can now craft Crystal Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.CrystalLeatherBoots, description: "You can now craft Crystal Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.CrystalBuckler, description: "You can now craft Crystal Bucklers!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CraftingTitles.ElderCrafter, description: "You've earned the title of Elder Crafter!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireFeatherCap, description: "You can now craft Dragonfire Feather Caps!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireLeatherVest, description: "You can now craft Dragonfire Leather Vests!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireLeatherGloves, description: "You can now craft Dragonfire Leather Gloves!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireLeatherBoots, description: "You can now craft Dragonfire Leather Boots!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireBuckler, description: "You can now craft Dragonfire Bucklers!")
            }
        };

        public CraftingSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}