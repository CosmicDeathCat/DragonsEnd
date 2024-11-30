using System.Collections.Generic;
using DragonsEnd.Abilities.Interfaces;
using DragonsEnd.Actor.Enemy.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Stats;

namespace DragonsEnd.Actor.Enemy
{
    public class BasicEnemy : Actor, IEnemy
    {
        public virtual EnemyTierType EnemyTier { get; set; }

        public BasicEnemy()
        {
        }

        public BasicEnemy
        (
            string name,
            Gender gender,
            CharacterClassType characterClass,
            ActorStats actorStats,
            IActorSkills actorSkills,
            CombatStyle combatStyle,
            EnemyTierType enemyTier,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            IEquipmentItem[]? equipment = null,
            IInventory? inventory = null,
            ILootConfig? lootConfig = null,
            ILootContainer? lootContainer = null,
            List<IAbility>? activeAbilities = null
        )
            : base(name: name, gender: gender, characterClass: characterClass, actorStats: actorStats, actorSkills: actorSkills, combatStyle: combatStyle,
                damageMultiplier: damageMultiplier, damageReductionMultiplier: damageReductionMultiplier,
                criticalHitMultiplier: criticalHitMultiplier, equipment: equipment,
                inventory: inventory, lootConfig: lootConfig, lootContainer: lootContainer, activeAbilities: activeAbilities)
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
                    mana: ActorStats.Mana.BaseValue,
                    stamina: ActorStats.Stamina.BaseValue,
                    meleeAttack: ActorStats.MeleeAttack.BaseValue,
                    meleeDefense: ActorStats.MeleeDefense.BaseValue,
                    rangedAttack: ActorStats.RangedAttack.BaseValue,
                    rangedDefense: ActorStats.RangedDefense.BaseValue,
                    magicAttack: ActorStats.MagicAttack.BaseValue,
                    magicDefense: ActorStats.MagicDefense.BaseValue,
                    criticalHitChance: ActorStats.CriticalHitChance.BaseValue),
                actorSkills: new ActorSkills(
                    actor: this,
                    meleeSkill: ActorSkills.MeleeSkill,
                    rangedSkill: ActorSkills.RangedSkill,
                    magicSkill: ActorSkills.MagicSkill,
                    alchemySkill: ActorSkills.AlchemySkill,
                    cookingSkill: ActorSkills.CookingSkill,
                    craftingSkill: ActorSkills.CraftingSkill,
                    enchantingSkill: ActorSkills.EnchantingSkill,
                    fishingSkill: ActorSkills.FishingSkill,
                    fletchingSkill: ActorSkills.FletchingSkill,
                    foragingSkill: ActorSkills.ForagingSkill,
                    miningSkill: ActorSkills.MiningSkill,
                    smithingSkill: ActorSkills.SmithingSkill,
                    ranchingSkill: ActorSkills.RanchingSkill,
                    woodcuttingSkill: ActorSkills.WoodcuttingSkill),
                combatStyle: CombatStyle,
                enemyTier: EnemyTier,
                damageMultiplier: DamageMultiplier.CurrentValue,
                damageReductionMultiplier: DamageReductionMultiplier.CurrentValue,
                equipment: Equipment!,
                inventory: Inventory,
                lootConfig: LootConfig,
                lootContainer: LootContainer,
                activeAbilities: ActiveAbilities
            );
        }

        IEnemy IEnemy.Copy()
        {
            return (IEnemy)Copy();
            // Explicit interface implementation to return IEnemy
        }
    }
}