using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Constants;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Ranching.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.GatheringSkills.Ranching
{
    public class RanchingSkill : BaseNonCombatSkill
    {
        public RanchingSkill(string name, IActor actor, int maxLevel = 20) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CropNames.Wheat, description: "You can now grow Wheat!", isLocked: false),
                new BasicSkillUnlock(name: AnimalNames.Chicken, description: "You can now raise Chickens!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BasicFeed, description: "You can now make Basic Feed!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.BeginnerRancher, description: "You've earned the title of Beginner Rancher!"),
                new BasicSkillUnlock(name: CropNames.Potato, description: "You can now grow Potatoes!"),
                new BasicSkillUnlock(name: AnimalNames.Cow, description: "You can now raise Cows!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.NoviceRancher, description: "You've earned the title of Novice Rancher!"),
                new BasicSkillUnlock(name: CropNames.Carrot, description: "You can now grow Carrots!"),
                new BasicSkillUnlock(name: AnimalNames.Sheep, description: "You can now raise Sheep!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.ApprenticeRancher, description: "You've earned the title of Apprentice Rancher!"),
                new BasicSkillUnlock(name: CropNames.Corn, description: "You can now grow Corn!"),
                new BasicSkillUnlock(name: AnimalNames.Pig, description: "You can now raise Pigs!"),
                new BasicSkillUnlock(name: ItemNames.ImprovedFeed, description: "You can now make Improved Feed!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.JourneymanRancher, description: "You've earned the title of Journeyman Rancher!"),
                new BasicSkillUnlock(name: CropNames.Tomatoes, description: "You can now grow Tomatoes!"),
                new BasicSkillUnlock(name: AnimalNames.Goat, description: "You can now raise Goats!"),
                new BasicSkillUnlock(name: ItemNames.AdvancedFeed, description: "You can now make Advanced Feed!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.ExpertRancher, description: "You've earned the title of Expert Rancher!"),
                new BasicSkillUnlock(name: CropNames.Sugarcane, description: "You can now grow Sugarcane!"),
                new BasicSkillUnlock(name: AnimalNames.Horse, description: "You can now raise Horses!"),
                new BasicSkillUnlock(name: ItemNames.HorseSaddle, description: "You can now craft Horse Saddles!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.AdeptRancher, description: "You've earned the title of Adept Rancher!"),
                new BasicSkillUnlock(name: CropNames.Cotton, description: "You can now grow Cotton!"),
                new BasicSkillUnlock(name: AnimalNames.Beehive, description: "You can now manage Beehives!"),
                new BasicSkillUnlock(name: ItemNames.Honey, description: "You can now harvest Honey!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.VeteranRancher, description: "You've earned the title of Veteran Rancher!"),
                new BasicSkillUnlock(name: CropNames.Grapes, description: "You can now grow Grapes!"),
                new BasicSkillUnlock(name: AnimalNames.Alpaca, description: "You can now raise Alpacas!"),
                new BasicSkillUnlock(name: ItemNames.AlpacaWool, description: "You can now shear Alpaca Wool!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.MasterRancher, description: "You've earned the title of Master Rancher!"),
                new BasicSkillUnlock(name: CropNames.Mushrooms, description: "You can now cultivate special Mushrooms!"),
                new BasicSkillUnlock(name: AnimalNames.Yak, description: "You can now raise Yaks!"),
                new BasicSkillUnlock(name: ItemNames.YakMilk, description: "You can now harvest Yak Milk!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.IllustriousRancher, description: "You've earned the title of Illustrious Rancher!"),
                new BasicSkillUnlock(name: CropNames.CoffeeBeans, description: "You can now grow Coffee Beans!"),
                new BasicSkillUnlock(name: AnimalNames.Silkworm, description: "You can now raise Silkworms!"),
                new BasicSkillUnlock(name: ItemNames.Silk, description: "You can now harvest Silk!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: RanchingTitles.ElderRancher, description: "You've earned the title of Elder Rancher!"),
                new BasicSkillUnlock(name: CropNames.Dragonfruit, description: "You can now grow Dragonfruit!"),
                new BasicSkillUnlock(name: AnimalNames.Dragon, description: "You can now raise Dragons!"),
                new BasicSkillUnlock(name: ItemNames.DragonScale, description: "You can now collect Dragon Scales!")
            }
        };
    }
}