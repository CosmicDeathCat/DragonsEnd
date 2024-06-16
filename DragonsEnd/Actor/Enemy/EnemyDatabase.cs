using System;
using System.Collections.Concurrent;
using DragonsEnd.Actor.Enemy.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Stats;

namespace DragonsEnd.Actor.Enemy
{
    public static class EnemyDatabase
    {
        public static ConcurrentDictionary<string, IEnemy> Enemies { get; set; } =
            new ConcurrentDictionary<string, IEnemy>(comparer: StringComparer.OrdinalIgnoreCase)
            {
                // Puny Slime Warrior
                [key: EnemyNames.PunySlimeWarrior] = new BasicEnemy(
                    name: EnemyNames.PunySlimeWarrior,
                    gender: Gender.Nonbinary,
                    characterClass: CharacterClassType.Warrior,
                    actorStats: new ActorStats(
                        health: 20,
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
                    gold: 10,
                    equipment: new IEquipmentItem[]
                    {
                        (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger)
                    },
                    inventory: new[]
                    {
                        ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion, quantity: 2),
                        ItemDatabase.GetItems(itemName: ItemNames.StrongHealthPotion, quantity: 3)
                    },
                    dropItems: new[]
                    {
                        ItemDatabase.GetDropItems(itemName: ItemNames.WeakHealthPotion),
                        ItemDatabase.GetDropItems(itemName: ItemNames.BronzeDagger, quantity: 1, dropRate: 0.75),
                        ItemDatabase.GetDropItems(itemName: ItemNames.BronzeSword, quantity: 1, dropRate: 0.50)
                    }
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