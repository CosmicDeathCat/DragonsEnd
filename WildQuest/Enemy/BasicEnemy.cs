using System;
using WildQuest.Actor;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Items;
using WildQuest.Stats;

namespace WildQuest.Enemy;

public class BasicEnemy : CombatActor, IEnemy
{
    public virtual EnemyTierType EnemyTier { get; set; }
    public BasicEnemy() :base()
    {
        
    }
    
    public BasicEnemy(
        string name,
        Gender gender,
        CharacterClassType characterClass,
        ActorStats actorStats,
        CombatStyle combatStyle,
        EnemyTierType enemyTier,
        int level = 1,
        long experience = -1L,
        double damageMultiplier = 1.00,
        double damageReductionMultiplier = 1.00,
        double criticalHitMultiplier = 2.00,
        long gold = 0,
        IEquipmentItem[]? equipment = null,
        IItem[]? inventory = null,
        params IDropItem[] dropItems) 
        : base(name, gender, characterClass, actorStats, combatStyle, level, experience, damageMultiplier, damageReductionMultiplier, criticalHitMultiplier, gold, equipment, inventory, dropItems)
    {
        EnemyTier = enemyTier;
    }
    
    public override IActor Copy()
    {
        return new BasicEnemy(
            name: Name,
            gender: Gender,
            characterClass: CharacterClass,
            actorStats: new ActorStats(
                health: ActorStats.Health.BaseValue,
                meleeAttack: ActorStats.MeleeAttack.BaseValue,
                meleeDefense: ActorStats.MeleeDefense.BaseValue,
                rangedAttack: ActorStats.RangedAttack.BaseValue,
                rangedDefense: ActorStats.RangedDefense.BaseValue,
                magicAttack: ActorStats.MagicAttack.BaseValue,
                magicDefense: ActorStats.MagicDefense.BaseValue,
                criticalHitChance: ActorStats.CriticalHitChance.BaseValue),
            combatStyle: CombatStyle,
            enemyTier: EnemyTier,
            level: Leveling.CurrentLevel,
            experience: Leveling.Experience,
            damageMultiplier: DamageMultiplier.CurrentValue,
            damageReductionMultiplier: DamageReductionMultiplier.CurrentValue,
            gold: Gold.CurrentValue,
            equipment: Equipment!,
            inventory: Inventory?.ToArray()!,
            dropItems: DropItems.ToArray());
    }

    IEnemy IEnemy.Copy() => (IEnemy)Copy(); // Explicit interface implementation to return IEnemy
}