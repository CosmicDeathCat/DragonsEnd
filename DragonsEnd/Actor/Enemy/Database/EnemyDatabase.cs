using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Enemy.Constants;
using DragonsEnd.Actor.Enemy.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Items.Database;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory;
using DragonsEnd.Items.Loot;
using DragonsEnd.Skills;
using DragonsEnd.Skills.Combat.Magic;
using DragonsEnd.Skills.Combat.Melee;
using DragonsEnd.Skills.Combat.Ranged;
using DragonsEnd.Skills.Constants;
using DragonsEnd.Stats;

namespace DragonsEnd.Actor.Enemy.Database
{
    public static class EnemyDatabase
    {
        public static ConcurrentDictionary<string, IEnemy> Enemies { get; set; } =
            new(comparer: StringComparer.OrdinalIgnoreCase)
            {
                // Puny Slime Warrior
                [key: EnemyNames.PunySlimeWarrior] = new BasicEnemy(
                    name: EnemyNames.PunySlimeWarrior,
                    gender: Gender.Nonbinary,
                    characterClass: CharacterClassType.Warrior,
                    actorStats: new ActorStats(
                        health: 150,
                        mana: 0,
                        stamina: 20,
                        meleeAttack: 1,
                        meleeDefense: 0,
                        rangedAttack: 0,
                        rangedDefense: 1,
                        magicAttack: 0,
                        magicDefense: -1),
                    actorSkills: new ActorSkills
                    {
                        MeleeSkill = new MeleeSkill(name: SkillNames.MeleeSkill, startingLevel: 5)
                    },
                    combatStyle: CombatStyle.Melee,
                    enemyTier: EnemyTierType.Puny,
                    equipment: new IEquipmentItem[]
                    {
                        (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger)
                    },
                    inventory: new Inventory
                    (
                        gold: 10,
                        items: new[]
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion, quantity: 2),
                            ItemDatabase.GetItems(itemName: ItemNames.StrongHealthPotion, quantity: 3)
                        }
                    ),
                    lootConfig: new LootConfig
                    (
                        minItemAmountDrop: 1,
                        maxItemAmountDrop: 2,
                        minGold: 5,
                        maxGold: 15,
                        minCombatExperience: 50,
                        maxCombatExperience: 150,
                        skillExperiences: new List<SkillExperience>
                        {
                            new(skillType: SkillType.Crafting, minExperience: 10, maxExperience: 50)
                        },
                        lootableItems: new List<IItem>()
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                            ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger, quantity: 1, dropRate: 0.75),
                            ItemDatabase.GetItems(itemName: ItemNames.BronzeSword, quantity: 1, dropRate: 0.50)
                        }
                    )
                ),
                // Puny Slime Mage
                [key: EnemyNames.PunySlimeMage] = new BasicEnemy(
                    name: EnemyNames.PunySlimeMage,
                    gender: Gender.Nonbinary,
                    characterClass: CharacterClassType.Mage,
                    actorStats: new ActorStats(
                        health: 100,
                        mana: 50,
                        stamina: 10,
                        meleeAttack: 0,
                        meleeDefense: -1,
                        rangedAttack: 0,
                        rangedDefense: 0,
                        magicAttack: 1,
                        magicDefense: 1),
                    actorSkills: new ActorSkills
                    {
                        MagicSkill = new MagicSkill(name: SkillNames.MagicSkill, actor: null, startingLevel: 5)
                    },
                    combatStyle: CombatStyle.Magic,
                    enemyTier: EnemyTierType.Puny,
                    equipment: new IEquipmentItem[]
                    {
                        (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.OldMagicStaff)
                    },
                    inventory: new Inventory
                    (
                        gold: 5,
                        items: new[]
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakManaPotion, quantity: 2),
                            ItemDatabase.GetItems(itemName: ItemNames.StrongManaPotion, quantity: 3)
                        }
                    ),
                    lootContainer: new LootContainer
                    (
                        gold: 5,
                        combatExperience: 125,
                        experiences: new List<SkillExperience>
                        {
                            new(skillType: SkillType.Magic, experience: 10)
                        },
                        items: new[]
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakManaPotion, quantity: 5),
                            ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion, quantity: 3),
                            ItemDatabase.GetItems(itemName: ItemNames.OldMagicStaff, quantity: 1, dropRate: 0.50)
                        }
                    )),
                // Puny Slime Archer
                [key: EnemyNames.PunySlimeArcher] = new BasicEnemy(
                    name: EnemyNames.PunySlimeArcher,
                    gender: Gender.Nonbinary,
                    characterClass: CharacterClassType.Archer,
                    actorStats: new ActorStats(
                        health: 100,
                        mana: 10,
                        stamina: 50,
                        meleeAttack: 0,
                        meleeDefense: -1,
                        rangedAttack: 1,
                        rangedDefense: 1,
                        magicAttack: 0,
                        magicDefense: 2),
                    actorSkills: new ActorSkills
                    {
                        RangedSkill = new RangedSkill(name: SkillNames.RangedSkill, actor: null, startingLevel: 5)
                    },
                    combatStyle: CombatStyle.Ranged,
                    enemyTier: EnemyTierType.Puny,
                    equipment: new IEquipmentItem[]
                    {
                        (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.FeebleWoodShortbow)
                    },
                    inventory: new Inventory
                    (
                        gold: 5,
                        items: new[]
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakStaminaPotion, quantity: 2),
                            ItemDatabase.GetItems(itemName: ItemNames.StrongStaminaPotion, quantity: 3)
                        }
                    ),
                    lootContainer: new LootContainer
                    (
                        gold: 5,
                        combatExperience: 150,
                        experiences: new List<SkillExperience>
                        {
                            new(skillType: SkillType.Ranged, experience: 10)
                        },
                        items: new[]
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakStaminaPotion),
                            ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                            ItemDatabase.GetItems(itemName: ItemNames.FeebleWoodShortbow, quantity: 1, dropRate: 0.50)
                        }
                    ))
            };

        public static IEnemy GetEnemy(string enemyName)
        {
            if (Enemies.TryGetValue(key: enemyName, value: out var enemy))
            {
                return enemy.Copy() ?? throw new InvalidOperationException();
            }

            throw new ArgumentException(message: $"Enemy with name '{enemyName}' not found.");
        }
    }
}