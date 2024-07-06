using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Enchanting.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.CraftingSkills.Enchanting
{
    public class EnchantingSkill : BaseNonCombatSkill
    {
        public EnchantingSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }

        public override SkillType SkillType => SkillType.Enchanting;

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: "Enchant: Old", description: "You can now enchant Old Mage gear!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.BeginnerEnchanter, description: "You've earned the title of Beginner Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Worn", description: "You can now enchant Worn Mage gear!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.NoviceEnchanter, description: "You've earned the title of Novice Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Enchanted", description: "You can now enchant Enchanted Mage gear!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.ApprenticeEnchanter, description: "You've earned the title of Apprentice Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Arcane", description: "You can now enchant Arcane Mage gear!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.JourneymanEnchanter, description: "You've earned the title of Journeyman Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Ancient", description: "You can now enchant Ancient Mage gear!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.ExpertEnchanter, description: "You've earned the title of Expert Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Eldritch", description: "You can now enchant Eldritch Mage gear!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.AdeptEnchanter, description: "You've earned the title of Adept Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Divine", description: "You can now enchant Divine Mage gear!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.VeteranEnchanter, description: "You've earned the title of Veteran Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Runic", description: "You can now enchant Runic Mage gear!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.MasterEnchanter, description: "You've earned the title of Master Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Ethereal", description: "You can now enchant Ethereal Mage gear!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.IllustriousEnchanter, description: "You've earned the title of Illustrious Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Mystic", description: "You can now enchant Mystic Mage gear!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: EnchantingTitles.ElderEnchanter, description: "You've earned the title of Elder Enchanter!"),
                new BasicSkillUnlock(name: "Enchant: Draconic", description: "You can now enchant Draconic Mage gear!")
            }
        };
    }
}