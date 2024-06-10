using System;
using System.Collections.Concurrent;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Items;
using WildQuest.Stats;

namespace WildQuest.Enemy;

public static class EnemyDatabase
{
    public static ConcurrentDictionary<string, IEnemy> Enemies { get; set; } = new(StringComparer.OrdinalIgnoreCase)
    {
        // Puny Slime Warrior
        [EnemyNames.PunySlimeWarrior] = new BasicEnemy(
            name: EnemyNames.PunySlimeWarrior,
            gender: Gender.Nonbinary,
            characterClass: CharacterClassType.Warrior,
            new ActorStats(
                health: 5,
                meleeAttack: 1,
                meleeDefense: 0,
                rangedAttack: 0,
                rangedDefense: 0,
                magicAttack: 0,
                magicDefense: 0),
            combatStyle: CombatStyle.Melee,
            enemyTier: EnemyTierType.Puny,
            level: 1,
            experience: 25,
            damageMultiplier: 1.00,
            damageReductionMultiplier: 1.00,
            gold: 10,
            equipment:
            [
            ],
            inventory:
            [
                ItemDatabase.GetItems(ItemNames.WeakHealthPotion, 2),
                ItemDatabase.GetItems(ItemNames.StrongHealthPotion, 3)
            ],
            dropItems:
            [
                ItemDatabase.GetDropItems(ItemNames.WeakHealthPotion, 1, 1),
                ItemDatabase.GetDropItems(ItemNames.StrongHealthPotion, 2, 0.75),
                ItemDatabase.GetDropItems(ItemNames.BronzeDagger, 1, 0.50),
                ItemDatabase.GetDropItems(ItemNames.BronzeSword, 1, 0.25),
                ItemDatabase.GetDropItems(ItemNames.BronzeShield, 1, 0.10),
            ]
        ),
    };
}