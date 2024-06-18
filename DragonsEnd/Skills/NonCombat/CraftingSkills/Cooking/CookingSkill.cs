using System.Collections.Concurrent;
using System.Collections.Generic;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Leveling.Messages;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Cooking.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.CraftingSkills.Cooking
{
    public class CookingSkill : BaseNonCombatSkill
    {
        public CookingSkill(string name, IActor actor, int maxLevel = 20) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: DishNames.BoiledEgg, description: "You can now cook Boiled Eggs (requires Eggs)!", isLocked: false),
                new BasicSkillUnlock(name: DishNames.ScrambledEggs, description: "You can now cook Scrambled Eggs (requires Eggs)!", isLocked: false),
                new BasicSkillUnlock(name: DishNames.ButteredToast, description: "You can now make Buttered Toast (requires Bread, Butter)!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.BeginnerCook, description: "You've earned the title of Beginner Cook!"),
                new BasicSkillUnlock(name: DishNames.RoastedChicken, description: "You can now cook Roasted Chicken (requires Chicken Meat)!"),
                new BasicSkillUnlock(name: DishNames.MashedPotatoes, description: "You can now cook Mashed Potatoes (requires Potatoes, Milk)!"),
                new BasicSkillUnlock(name: DishNames.SunfishSoup, description: "You can now cook Sunfish Soup (requires Sunfish, Herbs)!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.NoviceCook, description: "You've earned the title of Novice Cook!"),
                new BasicSkillUnlock(name: DishNames.CatfishStew, description: "You can now cook Catfish Stew (requires Catfish, Tomatoes, Onions)!"),
                new BasicSkillUnlock(name: DishNames.LambChop, description: "You can now cook Lamb Chops (requires Lamb Meat)!"),
                new BasicSkillUnlock(name: DishNames.CarrotCake, description: "You can now bake Carrot Cake (requires Carrots, Eggs, Milk, Wheat)!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.ApprenticeCook, description: "You've earned the title of Apprentice Cook!"),
                new BasicSkillUnlock(name: DishNames.Cornbread, description: "You can now bake Cornbread (requires Corn, Eggs, Milk)!"),
                new BasicSkillUnlock(name: DishNames.BaconAndEggs, description: "You can now cook Bacon and Eggs (requires Pork Belly, Eggs)!"),
                new BasicSkillUnlock(name: DishNames.BassMeuniere,
                    description: "You can now cook Bass Meunière (requires Largemouth Bass, Butter, Lemon, Herbs)!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.JourneymanCook, description: "You've earned the title of Journeyman Cook!"),
                new BasicSkillUnlock(name: DishNames.TomatoSoup, description: "You can now cook Tomato Soup (requires Tomatoes, Herbs)!"),
                new BasicSkillUnlock(name: DishNames.RoastGoat, description: "You can now cook Roast Goat (requires Goat Meat, Garlic, Spices)!"),
                new BasicSkillUnlock(name: DishNames.SalmonDinner,
                    description: "You can now cook a Salmon Dinner (requires Salmon, Potatoes, Lemon)!"),
                new BasicSkillUnlock(name: DishNames.ApplePie,
                    description: "You can now bake Apple Pie (requires Apples, Wheat Flour, Butter, Sugar)!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.ExpertCook, description: "You've earned the title of Expert Cook!"),
                new BasicSkillUnlock(name: DishNames.SwordfishSteak,
                    description: "You can now cook Swordfish Steak (requires Swordfish, Garlic, Butter)!"),
                new BasicSkillUnlock(name: DishNames.CandiedYams, description: "You can now cook Candied Yams (requires Yams, Butter, Brown Sugar)!"),
                new BasicSkillUnlock(name: DishNames.HorseSteak,
                    description: "You can now cook Horse Steak (requires Horse Meat, Salt, Pepper)!") // Consider this controversial
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.AdeptCook, description: "You've earned the title of Adept Cook!"),
                new BasicSkillUnlock(name: DishNames.SharkFinSoup,
                    description: "You can now cook Shark Fin Soup (requires Shark Fin)!"), // Consider this controversial
                new BasicSkillUnlock(name: DishNames.HoneyGlazedHam,
                    description: "You can now cook Honey Glazed Ham (requires Pork Leg, Honey, Spices)!"),
                new BasicSkillUnlock(name: DishNames.GrapeJelly, description: "You can now make Grape Jelly (requires Grapes, Sugar)!"),
                new BasicSkillUnlock(name: DishNames.FriedRice, description: "You can now cook Fried Rice (requires Rice, Eggs, Vegetables)!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.VeteranCook, description: "You've earned the title of Veteran Cook!"),
                new BasicSkillUnlock(name: DishNames.SquidInkPasta,
                    description: "You can now cook Squid Ink Pasta (requires Squid Ink, Pasta, Garlic)!"),
                new BasicSkillUnlock(name: DishNames.AlpacaJerky, description: "You can now make Alpaca Jerky (requires Alpaca Meat, Salt, Spices)!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.MasterCook, description: "You've earned the title of Master Cook!"),
                new BasicSkillUnlock(name: DishNames.CoelacanthCaviar,
                    description: "You can now prepare Coelacanth Caviar (requires Coelacanth Roe, Salt)!"),
                new BasicSkillUnlock(name: DishNames.YakButterTea, description: "You can now brew Yak Butter Tea (requires Yak Butter, Tea Leaves)!"),
                new BasicSkillUnlock(name: DishNames.MushroomMedley,
                    description: "You can now cook a Mushroom Medley (requires Assorted Mushrooms, Cream, Herbs)!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.IllustriousCook, description: "You've earned the title of Illustrious Cook!"),
                new BasicSkillUnlock(name: DishNames.GoblinSharkSushi,
                    description: "You can now prepare Goblin Shark Sushi (requires Goblin Shark Meat, Rice, Seaweed)!"),
                new BasicSkillUnlock(name: DishNames.SilkwormPupaeStirFry,
                    description: "You can now cook Silkworm Pupae Stir-Fry (requires Silkworm Pupae, Vegetables, Spices)!"),
                new BasicSkillUnlock(name: DishNames.PhoenixFruitSalad,
                    description: "You can now prepare Phoenix Fruit Salad (requires Phoenix Fruit, Honey, Yogurt)!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: CookingTitles.ElderCook, description: "You've earned the title of Elder Cook!"),
                new BasicSkillUnlock(name: DishNames.KrakenCalamari,
                    description: "You can now cook Kraken Calamari (requires Kraken Tentacles, Flour, Spices)!"),
                new BasicSkillUnlock(name: DishNames.DragonfruitTart,
                    description: "You can now bake Dragonfruit Tart (requires Dragonfruit, Flour, Sugar, Eggs)!"),
                new BasicSkillUnlock(name: DishNames.DragonRoast,
                    description: "You can now cook Dragon Roast (requires Dragon Meat, Rare Herbs, Exotic Mushrooms)!")
            }
        };

        ~CookingSkill()
        {
            MessageSystem.MessageManager.UnregisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }
    }
}