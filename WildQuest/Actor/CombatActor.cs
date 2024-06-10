using System;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Actor
{
    public class CombatActor : Actor, ICombatant
    {
        public CombatActor() : base()
        {
        }

        public CombatActor(
            string name,
            Gender gender,
            CharacterClassType characterClass,
            ActorStats actorStats,
            CombatStyle combatStyle,
            int level = 1,
            long experience = -1L,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            long gold = 0L,
            IEquipmentItem[]? equipment = null,
            IItem[]? inventory = null,
            params IDropItem[] dropItems)
            : base(name, gender, characterClass, actorStats, combatStyle, damageMultiplier, damageReductionMultiplier, criticalHitMultiplier, level,
                experience, gold, equipment, inventory, dropItems)
        {
        }

        public virtual (bool hasHit, bool isBlocked, bool hasKilled, int damage, bool isCriticalHit) Attack(ICombatant source, ICombatant target)
        {
            Target = target;

            bool hit = false;
            bool blocked = false;
            bool killed = false;
            int damage = 0;
            bool isCriticalHit = false;

            switch (CombatStyle)
            {
                case CombatStyle.Melee:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target, source.ActorStats.MeleeAttack.CurrentValue,
                        target.ActorStats.MeleeDefense.CurrentValue);
                    break;
                case CombatStyle.Ranged:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target, source.ActorStats.RangedAttack.CurrentValue,
                        target.ActorStats.RangedDefense.CurrentValue);
                    break;
                case CombatStyle.Magic:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target, source.ActorStats.MagicAttack.CurrentValue,
                        target.ActorStats.MagicDefense.CurrentValue);
                    break;
                case CombatStyle.Hybrid:
                    int avgAttack =
                        (int)Math.Round(
                            (source.ActorStats.MeleeAttack.CurrentValue + source.ActorStats.RangedAttack.CurrentValue +
                             source.ActorStats.MagicAttack.CurrentValue) / 3.0, MidpointRounding.AwayFromZero);
                    int avgDefense =
                        (int)Math.Round(
                            (target.ActorStats.MeleeDefense.CurrentValue + target.ActorStats.RangedAttack.CurrentValue +
                             target.ActorStats.MagicDefense.CurrentValue) / 3.0, MidpointRounding.AwayFromZero);
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target, avgAttack, avgDefense);
                    break;
            }

            return (hit, blocked, killed, damage, isCriticalHit);
        }

        public virtual (bool hit, bool blocked, bool killed, int damage, bool isCriticalHit) HandleAttack(ICombatant source, ICombatant target,
            int attackValue, int defenseValue)
        {
            Target = target;

            var rnd = new Random();
            bool hit = false;
            bool blocked = false;
            bool killed = false;
            int damage = 0;
            double critRoll = rnd.NextDouble();
            bool isCriticalHit = critRoll < source.ActorStats.CriticalHitChance.CurrentValue;

            // Ensure the roll is between 1 and the effective attack value minus defense value plus 1
            var attackRoll = rnd.Next(1, Math.Max(2, attackValue - defenseValue + 1)); // Adjusted to ensure at least 1

            if (attackRoll > 0)
            {
                hit = true;
                var maxSourceDamage = Math.Round(attackValue * source.DamageMultiplier.CurrentValue, MidpointRounding.AwayFromZero);
                var maxTargetDefense = Math.Round(defenseValue * target.DamageReductionMultiplier.CurrentValue, MidpointRounding.AwayFromZero);
                blocked = maxSourceDamage - maxTargetDefense <= 0;
                var roundedMinDamage = Math.Round(Math.Clamp(maxSourceDamage - maxTargetDefense, 1, maxSourceDamage), MidpointRounding.AwayFromZero);
                var roundedMaxDamage = Math.Round(Math.Clamp(maxSourceDamage, roundedMinDamage, maxSourceDamage), MidpointRounding.AwayFromZero);
                int randomDamage = 0;
                if (blocked)
                {
                    randomDamage = rnd.Next(0, (int)roundedMinDamage + 1);
                }
                else
                {
                    randomDamage = rnd.Next((int)roundedMinDamage, (int)roundedMaxDamage + 1);
                }

                damage = Math.Clamp(randomDamage, 0, (int)maxSourceDamage);

                if (isCriticalHit)
                {
                    damage = (int)(damage * CriticalHitMultiplier.CurrentValue); // Apply critical hit multiplier
                    Console.WriteLine($"{source.Name} lands a critical hit on {target.Name} for {damage} {source.CombatStyle} damage!");
                }
                else
                {
                    if (blocked)
                    {
                        if (damage <= 0)
                        {
                            Console.WriteLine($"{source.Name} attacks {target.Name}, but the attack is completely blocked!");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"{source.Name} attacks {target.Name}, but {target.Name} blocks most of the damage. However, {source.Name} still manages to deal {damage} {source.CombatStyle} damage.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{source.Name} attacks {target.Name} for {damage} {source.CombatStyle} damage.");
                    }
                }

                if (damage > 0)
                {
                    target.TakeDamage(source, damage);

                    if (target.ActorStats.Health.CurrentValue <= 0)
                    {
                        killed = true;
                        return (hit, blocked, killed, damage, isCriticalHit);
                    }
                }
            }
            else
            {
                Console.WriteLine($"{source.Name} attacks {target.Name}, but misses!");
            }

            return (hit, blocked, killed, damage, isCriticalHit);
        }
    }
}