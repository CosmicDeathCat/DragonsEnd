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
        [EnemyNames.Rat] = new BasicEnemy(
            name: EnemyNames.Rat,
            gender:Gender.Male,
            characterClass:CharacterClassType.Freelancer,
            new ActorStats(
            health: 50,
            meleeAttack: 1,
            meleeDefense: 5,
            rangedAttack: 0,
            rangedDefense: 0,
            magicAttack: 0,
            magicDefense: 0),
            level: 1,
            experience: 25,
            damageMultiplier: 1.00,
            damageReductionMultiplier: 1.00,
            gold: 50,
            equipment:
            [
                (IWeaponItem)ItemDatabase.Items[ItemNames.BronzeDagger]
            ],
            inventory:
            [
                ItemDatabase.GetItem(ItemNames.WeakHealthPotion, 2),
                ItemDatabase.GetItem(ItemNames.StrongHealthPotion, 3)
            ],
            dropItems:
            [
                new DropItem(ItemDatabase.GetItem(ItemNames.WeakHealthPotion), 1),
                new DropItem(ItemDatabase.GetItem(ItemNames.StrongHealthPotion, 2), 0.75),
                new DropItem(ItemDatabase.GetItem(ItemNames.BronzeDagger), 0.50),
                new DropItem(ItemDatabase.GetItem(ItemNames.BronzeSword), 0.25),
                new DropItem(ItemDatabase.GetItem(ItemNames.BronzeShield), 0.10),
            ]
        ),
    };

}