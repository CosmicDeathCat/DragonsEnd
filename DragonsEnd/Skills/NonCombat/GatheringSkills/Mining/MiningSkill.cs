using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.GatheringSkills.Mining.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.GatheringSkills.Mining
{
    public class MiningSkill : BaseNonCombatSkill
    {
        public MiningSkill(string name, IActor actor, int maxLevel = 20) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: OreNames.CopperOre, description: "You can now mine Copper Ore!", isLocked: false),
                new BasicSkillUnlock(name: OreNames.TinOre, description: "You can now mine Tin Ore!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.BeginnerMiner, description: "You've earned the title of Beginner Miner!"),
                new BasicSkillUnlock(name: OreNames.IronOre, description: "You can now mine Iron Ore!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.NoviceMiner, description: "You've earned the title of Novice Miner!"),
                new BasicSkillUnlock(name: OreNames.SteelOre, description: "You can now mine Steel Ore!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.ApprenticeMiner, description: "You've earned the title of Apprentice Miner!"),
                new BasicSkillUnlock(name: OreNames.MithrilOre, description: "You can now mine Mithril Ore!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.JourneymanMiner, description: "You've earned the title of Journeyman Miner!"),
                new BasicSkillUnlock(name: OreNames.AdamantiteOre, description: "You can now mine Adamantite Ore!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.ExpertMiner, description: "You've earned the title of Expert Miner!"),
                new BasicSkillUnlock(name: OreNames.OrichalcumOre, description: "You can now mine Orichalcum Ore!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.AdeptMiner, description: "You've earned the title of Adept Miner!"),
                new BasicSkillUnlock(name: OreNames.RuniteOre, description: "You can now mine Runite Ore!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.VeteranMiner, description: "You've earned the title of Veteran Miner!"),
                new BasicSkillUnlock(name: OreNames.LuminiteOre, description: "You can now mine Luminite Ore!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.MasterMiner, description: "You've earned the title of Master Miner!"),
                new BasicSkillUnlock(name: OreNames.StarmetalOre, description: "You can now mine Starmetal Ore!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.IllustriousMiner, description: "You've earned the title of Illustrious Miner!"),
                new BasicSkillUnlock(name: OreNames.AscendantOre, description: "You can now mine Ascendant Ore!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: MiningTitles.ElderMiner, description: "You've earned the title of Elder Miner!"),
                new BasicSkillUnlock(name: OreNames.DragoniteOre, description: "You can now mine Dragonite Ore!")

            }
        };
    }
}