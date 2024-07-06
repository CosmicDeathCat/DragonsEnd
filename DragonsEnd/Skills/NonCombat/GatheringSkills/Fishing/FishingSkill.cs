using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Fishing.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.GatheringSkills.Fishing
{
    public class FishingSkill : BaseNonCombatSkill
    {
        public FishingSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }

        public override SkillType SkillType => SkillType.Fishing;

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishNames.Minnow, description: "You can now catch Minnows!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.BeginnerAngler, description: "You've earned the title of Beginner Angler!"),
                new BasicSkillUnlock(name: FishNames.Sunfish, description: "You can now catch Sunfish!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.NoviceAngler, description: "You've earned the title of Novice Angler!"),
                new BasicSkillUnlock(name: FishNames.Catfish, description: "You can now catch Catfish!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.ApprenticeAngler, description: "You've earned the title of Apprentice Angler!"),
                new BasicSkillUnlock(name: FishNames.LargemouthBass, description: "You can now catch Largemouth Bass!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.JourneymanAngler, description: "You've earned the title of Journeyman Angler!"),
                new BasicSkillUnlock(name: FishNames.Salmon, description: "You can now catch Salmon!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.ExpertAngler, description: "You've earned the title of Expert Angler!"),
                new BasicSkillUnlock(name: FishNames.Swordfish, description: "You can now catch Swordfish!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.AdeptAngler, description: "You've earned the title of Adept Angler!"),
                new BasicSkillUnlock(name: FishNames.Shark, description: "You can now catch Sharks!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.VeteranAngler, description: "You've earned the title of Veteran Angler!"),
                new BasicSkillUnlock(name: FishNames.GiantSquid, description: "You can now catch Giant Squid!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.MasterAngler, description: "You've earned the title of Master Angler!"),
                new BasicSkillUnlock(name: FishNames.Coelacanth, description: "You can now catch Coelacanth!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.IllustriousAngler, description: "You've earned the title of Illustrious Angler!"),
                new BasicSkillUnlock(name: FishNames.GoblinShark, description: "You can now catch Goblin Sharks!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: FishingTitles.ElderAngler, description: "You've earned the title of Elder Angler!"),
                new BasicSkillUnlock(name: FishNames.Kraken, description: "You can now catch Kraken!")
            }
        };
    }
}