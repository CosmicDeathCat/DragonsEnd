using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Fletching.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.CraftingSkills.Fletching
{
    public class FletchingSkill : BaseNonCombatSkill
    {
        public override SkillType SkillType { get => SkillType.Fletching; }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ItemNames.FeebleWoodShortbow, description: "You can now fletch Feeble Wood Shortbows!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleCrossbow, description: "You can now fletch Feeble Crossbows!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleFirearm, description: "You can now craft Feeble Firearms!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.BeginnerFletcher, description: "You've earned the title of Beginner Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.WeakWoodShortbow, description: "You can now fletch Weak Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.WeakCrossbow, description: "You can now fletch Weak Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.WeakFirearm, description: "You can now craft Weak Firearms!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.NoviceFletcher, description: "You've earned the title of Novice Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.StrongWoodShortbow, description: "You can now fletch Strong Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.StrongCrossbow, description: "You can now fletch Strong Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.StrongFirearm, description: "You can now craft Strong Firearms!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.ApprenticeFletcher, description: "You've earned the title of Apprentice Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedWoodShortbow, description: "You can now fletch Reinforced Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedCrossbow, description: "You can now fletch Reinforced Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedFirearm, description: "You can now craft Reinforced Firearms!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.JourneymanFletcher, description: "You've earned the title of Journeyman Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.HardenedWoodShortbow, description: "You can now fletch Hardened Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.HardenedCrossbow, description: "You can now fletch Hardened Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.HardenedFirearm, description: "You can now craft Hardened Firearms!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.ExpertFletcher, description: "You've earned the title of Expert Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterWoodShortbow, description: "You can now fletch Sharpshooter Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterCrossbow, description: "You can now fletch Sharpshooter Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterFirearm, description: "You can now craft Sharpshooter Firearms!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.AdeptFletcher, description: "You've earned the title of Adept Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionWoodShortbow, description: "You can now fletch Precision Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionCrossbow, description: "You can now fletch Precision Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionFirearm, description: "You can now craft Precision Firearms!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.VeteranFletcher, description: "You've earned the title of Veteran Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanWoodShortbow, description: "You can now fletch Marksman Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanCrossbow, description: "You can now fletch Marksman Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanFirearm, description: "You can now craft Marksman Firearms!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.MasterFletcher, description: "You've earned the title of Master Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.PhantomWoodShortbow, description: "You can now fletch Phantom Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.PhantomCrossbow, description: "You can now fletch Phantom Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.PhantomFirearm, description: "You can now craft Phantom Firearms!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.IllustriousFletcher, description: "You've earned the title of Illustrious Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.CrystalWoodShortbow, description: "You can now fletch Crystal Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.CrystalCrossbow, description: "You can now fletch Crystal Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.CrystalFirearm, description: "You can now craft Crystal Firearms!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FletchingTitles.ElderFletcher, description: "You've earned the title of Elder Fletcher!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireWoodShortbow, description: "You can now fletch Dragonfire Wood Shortbows!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireCrossbow, description: "You can now fletch Dragonfire Crossbows!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireFirearm, description: "You can now craft Dragonfire Firearms!")
            }
        };

        public FletchingSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}