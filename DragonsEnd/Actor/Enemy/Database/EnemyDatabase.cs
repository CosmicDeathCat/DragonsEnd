using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Actor.Enemy.Constants;
using DragonsEnd.Actor.Enemy.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Items.Database;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Inventory;
using DragonsEnd.Items.Loot;
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
                        health: 20,
                        mana: 0,
                        stamina: 20,
                        meleeAttack: 1,
                        meleeDefense: 0,
                        rangedAttack: 0,
                        rangedDefense: 1,
                        magicAttack: 0,
                        magicDefense: -1),
                    combatStyle: CombatStyle.Melee,
                    enemyTier: EnemyTierType.Puny,
                    level: -1,
                    experience: 25,
                    equipment: new IEquipmentItem[]
                    {
                        (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger)
                    },
                    inventory: new Inventory
                    (
                        gold: 10,
                        items: new []
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion, quantity: 2),
                            ItemDatabase.GetItems(itemName: ItemNames.StrongHealthPotion, quantity: 3)
                        }
                    ),
                    lootContainer: new LootContainer
                    (
                        gold: 10, 
                        combatExperience: 500,
                        experiences: new List<SkillExperience>()
                        {
                            new(SkillType.Crafting, 10)
                        },
                        items: new []
                        {
                            ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                            ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger, quantity: 1, dropRate: 0.75),
                            ItemDatabase.GetItems(itemName: ItemNames.BronzeSword, quantity: 1, dropRate: 0.50)
                        }
                    )

                )
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