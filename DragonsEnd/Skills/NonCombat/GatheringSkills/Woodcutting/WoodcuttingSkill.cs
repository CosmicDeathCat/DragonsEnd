using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Woodcutting.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.GatheringSkills.Woodcutting
{
    public class WoodcuttingSkill : BaseNonCombatSkill
    {
        public override SkillType SkillType { get => SkillType.Woodcutting; }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: TreeNames.NormalTree, description: "You can now cut down Normal Trees!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.BeginnerWoodcutter, description: "You've earned the title of Beginner Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.OakTree, description: "You can now cut down Oak Trees!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.NoviceWoodcutter, description: "You've earned the title of Novice Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.WillowTree, description: "You can now cut down Willow Trees!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.ApprenticeWoodcutter, description: "You've earned the title of Apprentice Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.MapleTree, description: "You can now cut down Maple Trees!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.JourneymanWoodcutter, description: "You've earned the title of Journeyman Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.YewTree, description: "You can now cut down Yew Trees!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.ExpertWoodcutter, description: "You've earned the title of Expert Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.MagicTree, description: "You can now cut down Magic Trees!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.AdeptWoodcutter, description: "You've earned the title of Adept Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.MahoganyTree, description: "You can now cut down Mahogany Trees!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.VeteranWoodcutter, description: "You've earned the title of Veteran Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.TeakTree, description: "You can now cut down Teak Trees!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.MasterWoodcutter, description: "You've earned the title of Master Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.ElderTree, description: "You can now cut down Elder Trees!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.IllustriousWoodcutter,
                    description: "You've earned the title of Illustrious Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.CrystalTree, description: "You can now cut down Crystal Trees!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: WoodcuttingTitles.ElderWoodcutter, description: "You've earned the title of Elder Woodcutter!"),
                new BasicSkillUnlock(name: TreeNames.DragonwoodTree, description: "You can now cut down Dragonwood Trees!")
            }
        };

        public WoodcuttingSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}