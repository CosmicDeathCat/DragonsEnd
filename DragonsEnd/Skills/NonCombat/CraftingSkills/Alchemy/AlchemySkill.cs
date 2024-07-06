using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.NonCombat.CraftingSkills.Alchemy.Constants;
using DragonsEnd.Skills.Unlocks;

namespace DragonsEnd.Skills.NonCombat.CraftingSkills.Alchemy
{
    public class AlchemySkill : BaseNonCombatSkill
    {
        public AlchemySkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }

        public override SkillType SkillType => SkillType.Alchemy;

        public override ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new()
        {
            [key: 1] = new List<ILockable>
            {
                new BasicSkillUnlock(name: ItemNames.WeakHealthPotion, description: "You can now brew Weak Health Potions!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.WeakManaPotion, description: "You can now brew Weak Mana Potions!", isLocked: false),
                new BasicSkillUnlock(name: ItemNames.WeakStaminaPotion, description: "You can now brew Weak Action Potions!", isLocked: false)
            },
            [key: 10] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.BeginnerAlchemist, description: "You've earned the title of Beginner Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.WeakMeleeAttackPotion, description: "You can now brew Weak Melee Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakMeleeDefensePotion, description: "You can now brew Weak Melee Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakRangedAttackPotion, description: "You can now brew Weak Ranged Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakRangedDefensePotion, description: "You can now brew Weak Ranged Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakMagicAttackPotion, description: "You can now brew Weak Magic Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakMagicDefensePotion, description: "You can now brew Weak Magic Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalHealthPotion, description: "You can now brew Normal Health Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalManaPotion, description: "You can now brew Normal Mana Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalStaminaPotion, description: "You can now brew Normal Action Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakElixir, description: "You can now brew Weak Elixirs!")
            },
            [key: 20] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.NoviceAlchemist, description: "You've earned the title of Novice Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.WeakGatheringPotion, description: "You can now brew Weak Gathering Potions!"),
                new BasicSkillUnlock(name: ItemNames.WeakCraftingPotion, description: "You can now brew Weak Crafting Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalMeleeAttackPotion, description: "You can now brew Normal Melee Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalMeleeDefensePotion, description: "You can now brew Normal Melee Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalRangedAttackPotion, description: "You can now brew Normal Ranged Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalRangedDefensePotion, description: "You can now brew Normal Ranged Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalMagicAttackPotion, description: "You can now brew Normal Magic Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalMagicDefensePotion, description: "You can now brew Normal Magic Defense Potions!")
            },
            [key: 30] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.ApprenticeAlchemist, description: "You've earned the title of Apprentice Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.RevivalPotion, description: "You can now brew Revival Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalGatheringPotion, description: "You can now brew Normal Gathering Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalCraftingPotion, description: "You can now brew Normal Crafting Potions!"),
                new BasicSkillUnlock(name: ItemNames.NormalElixir, description: "You can now brew Normal Elixirs!")
            },
            [key: 40] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.JourneymanAlchemist, description: "You've earned the title of Journeyman Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.StrongHealthPotion, description: "You can now brew Strong Health Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongManaPotion, description: "You can now brew Strong Mana Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongStaminaPotion, description: "You can now brew Strong Action Potions!")
            },
            [key: 50] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.ExpertAlchemist, description: "You've earned the title of Expert Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.StrongMeleeAttackPotion, description: "You can now brew Strong Melee Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongMeleeDefensePotion, description: "You can now brew Strong Melee Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongRangedAttackPotion, description: "You can now brew Strong Ranged Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongRangedDefensePotion, description: "You can now brew Strong Ranged Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongMagicAttackPotion, description: "You can now brew Strong Magic Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongMagicDefensePotion, description: "You can now brew Strong Magic Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongElixir, description: "You can now brew Strong Elixirs!")
            },
            [key: 60] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.AdeptAlchemist, description: "You've earned the title of Adept Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.StrongGatheringPotion, description: "You can now brew Strong Gathering Potions!"),
                new BasicSkillUnlock(name: ItemNames.StrongCraftingPotion, description: "You can now brew Strong Crafting Potions!")
            },
            [key: 70] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.VeteranAlchemist, description: "You've earned the title of Veteran Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.UltraHealthPotion, description: "You can now brew Ultra Health Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraManaPotion, description: "You can now brew Ultra Mana Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraStaminaPotion, description: "You can now brew Ultra Action Potions!")
            },
            [key: 80] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.MasterAlchemist, description: "You've earned the title of Master Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.UltraMeleeAttackPotion, description: "You can now brew Ultra Melee Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraMeleeDefensePotion, description: "You can now brew Ultra Melee Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraRangedAttackPotion, description: "You can now brew Ultra Ranged Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraRangedDefensePotion, description: "You can now brew Ultra Ranged Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraMagicAttackPotion, description: "You can now brew Ultra Magic Attack Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraMagicDefensePotion, description: "You can now brew Ultra Magic Defense Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraElixir, description: "You can now brew Ultra Elixirs!")
            },
            [key: 90] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.IllustriousAlchemist, description: "You've earned the title of Illustrious Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.UltraGatheringPotion, description: "You can now brew Ultra Gathering Potions!"),
                new BasicSkillUnlock(name: ItemNames.UltraCraftingPotion, description: "You can now brew Ultra Crafting Potions!")
            },
            [key: 100] = new List<ILockable>
            {
                new BasicSkillUnlock(name: AlchemyTitles.ElderAlchemist, description: "You've earned the title of Elder Alchemist!"),
                new BasicSkillUnlock(name: ItemNames.ElixirOfLife, description: "You can now brew Elixirs of Life!"),
                new BasicSkillUnlock(name: ItemNames.ElixirOfPower, description: "You can now brew Elixirs of Power!"),
                new BasicSkillUnlock(name: ItemNames.ElixerOfResilience, description: "You can now brew Elixirs of Resilience!"),
                new BasicSkillUnlock(name: ItemNames.ElixerOfTheAncients, description: "You can now brew Elixirs of the Ancients!"),
                new BasicSkillUnlock(name: ItemNames.PhoenixDown, description: "You can now create Phoenix Downs!")
            }
        };
    }
}