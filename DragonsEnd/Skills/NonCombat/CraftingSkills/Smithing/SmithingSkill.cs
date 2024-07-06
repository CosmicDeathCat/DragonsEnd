using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Smithing.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.CraftingSkills.Smithing
{
    public class SmithingSkill : BaseNonCombatSkill
    {
        public override SkillType SkillType { get => SkillType.Smithing; }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: BarNames.BronzeBar, description: "You can now smelt Bronze Bars!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeHelmet, description: "You can now smith Bronze Helmets!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeChestplate, description: "You can now smith Bronze Chestplates!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeGauntlets, description: "You can now smith Bronze Gauntlets!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeGreaves, description: "You can now smith Bronze Greaves!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeShield, description: "You can now smith Bronze Shields!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeDagger, description: "You can now smith Bronze Daggers!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeSword, description: "You can now smith Bronze Swords!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeClaws, description: "You can now smith Bronze Claws!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.BronzeGreatSword, description: "You can now smith Bronze GreatSwords!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.FeebleThrowingKnife, description: "You can now smith Feeble Throwing Knives!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.BeginnerSmith, description: "You've earned the title of Beginner Smith!"),
                new BasicSkillUnlock(name: BarNames.IronBar, description: "You can now smelt Iron Bars!"),
                new BasicSkillUnlock(name: ItemNames.IronHelmet, description: "You can now smith Iron Helmets!"),
                new BasicSkillUnlock(name: ItemNames.IronChestplate, description: "You can now smith Iron Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.IronGauntlets, description: "You can now smith Iron Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.IronGreaves, description: "You can now smith Iron Greaves!"),
                new BasicSkillUnlock(name: ItemNames.IronShield, description: "You can now smith Iron Shields!"),
                new BasicSkillUnlock(name: ItemNames.IronDagger, description: "You can now smith Iron Daggers!"),
                new BasicSkillUnlock(name: ItemNames.IronSword, description: "You can now smith Iron Swords!"),
                new BasicSkillUnlock(name: ItemNames.IronClaws, description: "You can now smith Iron Claws!"),
                new BasicSkillUnlock(name: ItemNames.IronGreatSword, description: "You can now smith Iron GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.WeakThrowingKnife, description: "You can now smith Weak Throwing Knives!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.NoviceSmith, description: "You've earned the title of Novice Smith!"),
                new BasicSkillUnlock(name: BarNames.SteelBar, description: "You can now smelt Steel Bars!"),
                new BasicSkillUnlock(name: ItemNames.SteelHelmet, description: "You can now smith Steel Helmets!"),
                new BasicSkillUnlock(name: ItemNames.SteelChestplate, description: "You can now smith Steel Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.SteelGauntlets, description: "You can now smith Steel Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.SteelGreaves, description: "You can now smith Steel Greaves!"),
                new BasicSkillUnlock(name: ItemNames.SteelShield, description: "You can now smith Steel Shields!"),
                new BasicSkillUnlock(name: ItemNames.SteelDagger, description: "You can now smith Steel Daggers!"),
                new BasicSkillUnlock(name: ItemNames.SteelSword, description: "You can now smith Steel Swords!"),
                new BasicSkillUnlock(name: ItemNames.SteelClaws, description: "You can now smith Steel Claws!"),
                new BasicSkillUnlock(name: ItemNames.SteelGreatSword, description: "You can now smith Steel GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.StrongThrowingKnife, description: "You can now smith Strong Throwing Knives!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.ApprenticeSmith, description: "You've earned the title of Apprentice Smith!"),
                new BasicSkillUnlock(name: BarNames.MithrilBar, description: "You can now smelt Mithril Bars!"),
                new BasicSkillUnlock(name: ItemNames.MithrilHelmet, description: "You can now smith Mithril Helmets!"),
                new BasicSkillUnlock(name: ItemNames.MithrilChestplate, description: "You can now smith Mithril Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.MithrilGauntlets, description: "You can now smith Mithril Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.MithrilGreaves, description: "You can now smith Mithril Greaves!"),
                new BasicSkillUnlock(name: ItemNames.MithrilShield, description: "You can now smith Mithril Shields!"),
                new BasicSkillUnlock(name: ItemNames.MithrilDagger, description: "You can now smith Mithril Daggers!"),
                new BasicSkillUnlock(name: ItemNames.MithrilSword, description: "You can now smith Mithril Swords!"),
                new BasicSkillUnlock(name: ItemNames.MithrilClaws, description: "You can now smith Mithril Claws!"),
                new BasicSkillUnlock(name: ItemNames.MithrilGreatSword, description: "You can now smith Mithril GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.ReinforcedThrowingKnife, description: "You can now smith Reinforced Throwing Knives!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.JourneymanSmith, description: "You've earned the title of Journeyman Smith!"),
                new BasicSkillUnlock(name: BarNames.AdamantiteBar, description: "You can now smelt Adamantite Bars!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumHelmet, description: "You can now smith Adamantium Helmets!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumChestplate, description: "You can now smith Adamantium Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumGauntlets, description: "You can now smith Adamantium Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumGreaves, description: "You can now smith Adamantium Greaves!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumShield, description: "You can now smith Adamantium Shields!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumDagger, description: "You can now smith Adamantium Daggers!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumSword, description: "You can now smith Adamantium Swords!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumClaws, description: "You can now smith Adamantium Claws!"),
                new BasicSkillUnlock(name: ItemNames.AdamantiumGreatSword, description: "You can now smith Adamantium GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.HardenedThrowingKnife, description: "You can now smith Hardened Throwing Knives!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.ExpertSmith, description: "You've earned the title of Expert Smith!"),
                new BasicSkillUnlock(name: BarNames.OrichalcumBar, description: "You can now smelt Orichalcum Bars!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumHelmet, description: "You can now smith Orichalcum Helmets!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumChestplate, description: "You can now smith Orichalcum Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumGauntlets, description: "You can now smith Orichalcum Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumGreaves, description: "You can now smith Orichalcum Greaves!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumShield, description: "You can now smith Orichalcum Shields!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumDagger, description: "You can now smith Orichalcum Daggers!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumSword, description: "You can now smith Orichalcum Swords!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumClaws, description: "You can now smith Orichalcum Claws!"),
                new BasicSkillUnlock(name: ItemNames.OrichalcumGreatSword, description: "You can now smith Orichalcum GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.SharpshooterThrowingKnife, description: "You can now smith Sharpshooter Throwing Knives!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.AdeptSmith, description: "You've earned the title of Adept Smith!"),
                new BasicSkillUnlock(name: BarNames.RuniteBar, description: "You can now smelt Runite Bars!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelHelmet, description: "You can now smith Dark Steel Helmets!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelChestplate, description: "You can now smith Dark Steel Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelGauntlets, description: "You can now smith Dark Steel Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelGreaves, description: "You can now smith Dark Steel Greaves!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelShield, description: "You can now smith Dark Steel Shields!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelDagger, description: "You can now smith Dark Steel Daggers!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelSword, description: "You can now smith Dark Steel Swords!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelClaws, description: "You can now smith Dark Steel Claws!"),
                new BasicSkillUnlock(name: ItemNames.DarkSteelGreatSword, description: "You can now smith Dark Steel GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.PrecisionThrowingKnife, description: "You can now smith Precision Throwing Knives!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.VeteranSmith, description: "You've earned the title of Veteran Smith!"),
                new BasicSkillUnlock(name: BarNames.LuminiteBar, description: "You can now smelt Luminite Bars!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelHelmet, description: "You can now smith Sun Steel Helmets!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelChestplate, description: "You can now smith Sun Steel Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelGauntlets, description: "You can now smith Sun Steel Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelGreaves, description: "You can now smith Sun Steel Greaves!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelShield, description: "You can now smith Sun Steel Shields!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelDagger, description: "You can now smith Sun Steel Daggers!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelSword, description: "You can now smith Sun Steel Swords!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelClaws, description: "You can now smith Sun Steel Claws!"),
                new BasicSkillUnlock(name: ItemNames.SunSteelGreatSword, description: "You can now smith Sun Steel GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.MarksmanThrowingKnife, description: "You can now smith Marksman Throwing Knives!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.MasterSmith, description: "You've earned the title of Master Smith!"),
                new BasicSkillUnlock(name: BarNames.StarmetalBar, description: "You can now smelt Starmetal Bars!"),
                new BasicSkillUnlock(name: ItemNames.CelestialHelmet, description: "You can now smith Celestial Helmets!"),
                new BasicSkillUnlock(name: ItemNames.CelestialChestplate, description: "You can now smith Celestial Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.CelestialGauntlets, description: "You can now smith Celestial Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.CelestialGreaves, description: "You can now smith Celestial Greaves!"),
                new BasicSkillUnlock(name: ItemNames.CelestialShield, description: "You can now smith Celestial Shields!"),
                new BasicSkillUnlock(name: ItemNames.CelestialDagger, description: "You can now smith Celestial Daggers!"),
                new BasicSkillUnlock(name: ItemNames.CelestialSword, description: "You can now smith Celestial Swords!"),
                new BasicSkillUnlock(name: ItemNames.CelestialClaws, description: "You can now smith Celestial Claws!"),
                new BasicSkillUnlock(name: ItemNames.CelestialGreatSword, description: "You can now smith Celestial GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.PhantomThrowingKnife, description: "You can now smith Phantom Throwing Knives!")
            },

            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.ElderSmith, description: "You've earned the title of Elder Smith!"),
                new BasicSkillUnlock(name: BarNames.AscendantBar, description: "You can now smelt Ascendant Bars!"),
                new BasicSkillUnlock(name: ItemNames.AscendantHelmet, description: "You can now smith Ascendant Helmets!"),
                new BasicSkillUnlock(name: ItemNames.AscendantChestplate, description: "You can now smith Ascendant Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.AscendantGauntlets, description: "You can now smith Ascendant Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.AscendantGreaves, description: "You can now smith Ascendant Greaves!"),
                new BasicSkillUnlock(name: ItemNames.AscendantShield, description: "You can now smith Ascendant Shields!"),
                new BasicSkillUnlock(name: ItemNames.AscendantDagger, description: "You can now smith Ascendant Daggers!"),
                new BasicSkillUnlock(name: ItemNames.AscendantSword, description: "You can now smith Ascendant Swords!"),
                new BasicSkillUnlock(name: ItemNames.AscendantClaws, description: "You can now smith Ascendant Claws!"),
                new BasicSkillUnlock(name: ItemNames.AscendantGreatSword, description: "You can now smith Ascendant GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.DragonfireThrowingKnife, description: "You can now smith Dragonfire Throwing Knives!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: SmithingTitles.IllustriousSmith, description: "You've earned the title of Illustrious Smith!"),
                new BasicSkillUnlock(name: BarNames.DragoniteBar, description: "You can now smelt Dragonite Bars!"),
                new BasicSkillUnlock(name: ItemNames.DragonHelmet, description: "You can now smith Dragon Helmets!"),
                new BasicSkillUnlock(name: ItemNames.DragonChestplate, description: "You can now smith Dragon Chestplates!"),
                new BasicSkillUnlock(name: ItemNames.DragonGauntlets, description: "You can now smith Dragon Gauntlets!"),
                new BasicSkillUnlock(name: ItemNames.DragonGreaves, description: "You can now smith Dragon Greaves!"),
                new BasicSkillUnlock(name: ItemNames.DragonShield, description: "You can now smith Dragon Shields!"),
                new BasicSkillUnlock(name: ItemNames.DragonDagger, description: "You can now smith Dragon Daggers!"),
                new BasicSkillUnlock(name: ItemNames.DragonSword, description: "You can now smith Dragon Swords!"),
                new BasicSkillUnlock(name: ItemNames.DragonClaws, description: "You can now smith Dragon Claws!"),
                new BasicSkillUnlock(name: ItemNames.DragonGreatSword, description: "You can now smith Dragon GreatSwords!"),
                new BasicSkillUnlock(name: ItemNames.CrystalThrowingKnife, description: "You can now smith Crystal Throwing Knives!")
            }
        };

        public SmithingSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}