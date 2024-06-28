using DragonsEnd.Actor.Enemy.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Loot;
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
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            IEquipmentItem[]? equipment = null,
            IInventory? inventory = null,
            LootContainer? lootContainer = null
        )
            : base(name: name, gender: gender, characterClass: characterClass, actorStats: actorStats, combatStyle: combatStyle,
                damageMultiplier: damageMultiplier, damageReductionMultiplier: damageReductionMultiplier,
                criticalHitMultiplier: criticalHitMultiplier, equipment: equipment,
                inventory: inventory, lootContainer: lootContainer)
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
                    mana: ActorStats.Mana.BaseValue,
                    stamina: ActorStats.Stamina.BaseValue,
                    meleeAttack: ActorStats.MeleeAttack.BaseValue,
                    meleeDefense: ActorStats.MeleeDefense.BaseValue,
                    rangedAttack: ActorStats.RangedAttack.BaseValue,
                    rangedDefense: ActorStats.RangedDefense.BaseValue,
                    magicAttack: ActorStats.MagicAttack.BaseValue,
                    magicDefense: ActorStats.MagicDefense.BaseValue,
                    criticalHitChance: ActorStats.CriticalHitChance.BaseValue),
                combatStyle: CombatStyle,
                enemyTier: EnemyTier,
                damageMultiplier: DamageMultiplier.CurrentValue,
                damageReductionMultiplier: DamageReductionMultiplier.CurrentValue,
                equipment: Equipment!,
                inventory: Inventory,
                lootContainer: LootContainer
                );
        }

        IEnemy IEnemy.Copy()
        {
            return (IEnemy)Copy();
            // Explicit interface implementation to return IEnemy
        }
    }
}