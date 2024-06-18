using DragonsEnd.Actor.Enemy.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Stats;

namespace DragonsEnd.Actor.Enemy
{
    public class BasicEnemy : Actor, IEnemy
    {
        public BasicEnemy()
        {
        }

        public BasicEnemy
        (
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
            params IDropItem[] dropItems
        )
            : base(name: name, gender: gender, characterClass: characterClass, actorStats: actorStats, combatStyle: combatStyle,
                damageMultiplier: damageMultiplier, damageReductionMultiplier: damageReductionMultiplier,
                criticalHitMultiplier: criticalHitMultiplier, level: level, experience: experience, gold: gold, equipment: equipment,
                inventory: inventory, dropItems: dropItems)
        {
            EnemyTier = enemyTier;
        }

        public virtual EnemyTierType EnemyTier { get; set; }

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

        IEnemy IEnemy.Copy()
        {
            return (IEnemy)Copy();
            // Explicit interface implementation to return IEnemy
        }
    }
}