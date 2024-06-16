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
            : base(name, gender, characterClass, actorStats, combatStyle, damageMultiplier, damageReductionMultiplier,
                criticalHitMultiplier, level, experience, gold, equipment, inventory, dropItems)
        {
            EnemyTier = enemyTier;
        }

        public virtual EnemyTierType EnemyTier { get; set; }

        public override IActor Copy()
        {
            return new BasicEnemy(
                Name,
                Gender,
                CharacterClass,
                new ActorStats(
                    ActorStats.Health.BaseValue,
                    ActorStats.MeleeAttack.BaseValue,
                    ActorStats.MeleeDefense.BaseValue,
                    ActorStats.RangedAttack.BaseValue,
                    ActorStats.RangedDefense.BaseValue,
                    ActorStats.MagicAttack.BaseValue,
                    ActorStats.MagicDefense.BaseValue,
                    ActorStats.CriticalHitChance.BaseValue),
                CombatStyle,
                EnemyTier,
                Leveling.CurrentLevel,
                Leveling.Experience,
                DamageMultiplier.CurrentValue,
                DamageReductionMultiplier.CurrentValue,
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