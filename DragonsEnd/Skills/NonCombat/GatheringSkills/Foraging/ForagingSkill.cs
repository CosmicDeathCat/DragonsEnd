using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Foraging.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.GatheringSkills.Foraging
{
    public class ForagingSkill : BaseNonCombatSkill
    {
        public ForagingSkill(string name, IActor actor, int maxLevel = 100) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
        public override SkillType SkillType => SkillType.Foraging;

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForageNames.WildBerries, description: "You can now forage Wild Berries!", isLocked: false),
                new BasicSkillUnlock(name: ForageNames.Mushrooms, description: "You can now forage Mushrooms!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.BeginnerForager, description: "You've earned the title of Beginner Forager!"),
                new BasicSkillUnlock(name: ForageNames.WildHerbs, description: "You can now forage Wild Herbs!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.NoviceForager, description: "You've earned the title of Novice Forager!"),
                new BasicSkillUnlock(name: ForageNames.WildFlowers, description: "You can now forage Wild Flowers!"),
                new BasicSkillUnlock(name: ForageNames.EdibleRoots, description: "You can now forage Edible Roots!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.ApprenticeForager, description: "You've earned the title of Apprentice Forager!"),
                new BasicSkillUnlock(name: ForageNames.WildGarlic, description: "You can now forage Wild Garlic!"),
                new BasicSkillUnlock(name: ForageNames.WildOnions, description: "You can now forage Wild Onions!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.JourneymanForager, description: "You've earned the title of Journeyman Forager!"),
                new BasicSkillUnlock(name: ForageNames.RareHerbs, description: "You can now forage Rare Herbs!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.ExpertForager, description: "You've earned the title of Expert Forager!"),
                new BasicSkillUnlock(name: ForageNames.WildSpices, description: "You can now forage Wild Spices!"),
                new BasicSkillUnlock(name: ForageNames.ExoticMushrooms, description: "You can now forage Exotic Mushrooms!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.AdeptForager, description: "You've earned the title of Adept Forager!"),
                new BasicSkillUnlock(name: ForageNames.GlowingBerries, description: "You can now forage Glowing Berries!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.VeteranForager, description: "You've earned the title of Veteran Forager!"),
                new BasicSkillUnlock(name: ForageNames.Sunpetal, description: "You can now forage Sunpetals!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.MasterForager, description: "You've earned the title of Master Forager!"),
                new BasicSkillUnlock(name: ForageNames.Moonpetal, description: "You can now forage Moonpetals!"),
                new BasicSkillUnlock(name: ForageNames.DragonLily, description: "You can now forage Dragon Lilies!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.IllustriousForager, description: "You've earned the title of Illustrious Forager!"),
                new BasicSkillUnlock(name: ForageNames.PhoenixFruit, description: "You can now forage Phoenix Fruit!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ForagingTitles.ElderForager, description: "You've earned the title of Elder Forager!"),
                new BasicSkillUnlock(name: ForageNames.LifeBloom, description: "You can now forage Life Blooms!")
            }
        };
    }
}