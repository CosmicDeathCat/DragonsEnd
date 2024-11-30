using System;
using System.Collections.Generic;
using DragonsEnd.Abilities.Combat.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat_Resources.Interfaces;

namespace DragonsEnd.Abilities.Combat
{
    public class BasicAttackAbility : BasicAbility, IAttackAbility
    {
        public CombatStyle? Style { get; set; } 
        public int BaseDamage { get; set; } = 1;
        public double DamageMultiplier { get; set; } = 1.0;
        public bool IgnoreDefense { get; set; } = false;
        public bool AlwaysHits { get; set; } = false;
        
        public virtual (bool hasHit, bool isBlocked, bool hasKilled, int damage, bool isCriticalHit) Attack(IActor source, IActor target, CombatStyle? style = null)
        {
            var hit = false;
            var blocked = false;
            var killed = false;
            var damage = 0;
            var isCriticalHit = false;
            
            Style = style ?? source.CombatStyle;
            
            var attackValue = CalculateAttackValue(target, style);
            var defenseValue = CalculateDefenseValue(source, style);
            
            return HandleAttack(source: source, target: target, attackValue: attackValue, defenseValue: defenseValue);
        }

        public virtual (bool hit, bool blocked, bool killed, int damage, bool isCriticalHit) HandleAttack(IActor source, IActor target, int attackValue, int defenseValue)
        {
            var rnd = new Random();
            var hit = false;
            var blocked = false;
            var killed = false;
            var damage = 0;
            var critRoll = rnd.NextDouble();
            var isCriticalHit = critRoll < source.ActorStats.CriticalHitChance.CurrentValue;

            if (IgnoreDefense)
            {
                defenseValue = 0;
            }
            // Ensure the roll is between 1 and the effective attack value minus defense value plus 1
            var attackRoll = rnd.Next(minValue: 1, maxValue: Math.Max(val1: 2, val2: attackValue - defenseValue + 1)); // Adjusted to ensure at least 1
            if (AlwaysHits)
            {
                attackRoll = 1;
            }

            if (attackRoll > 0)
            {
                hit = true;
                var maxSourceDamage = Math.Round(value: attackValue * source.DamageMultiplier.CurrentValue * DamageMultiplier,
                    mode: MidpointRounding.AwayFromZero);
                var maxTargetDefense = Math.Round(value: defenseValue * target.DamageReductionMultiplier.CurrentValue,
                    mode: MidpointRounding.AwayFromZero);
                blocked = maxSourceDamage - maxTargetDefense <= 0;
                var roundedMinDamage = Math.Round(value: Math.Clamp(value: maxSourceDamage - maxTargetDefense, min: BaseDamage, max: maxSourceDamage),
                    mode: MidpointRounding.AwayFromZero);
                var roundedMaxDamage = Math.Round(value: Math.Clamp(value: maxSourceDamage, min: roundedMinDamage, max: maxSourceDamage),
                    mode: MidpointRounding.AwayFromZero);
                var randomDamage = 0;
                if (blocked)
                {
                    randomDamage = rnd.Next(minValue: 0, maxValue: (int)roundedMinDamage + 1);
                }
                else
                {
                    randomDamage = rnd.Next(minValue: (int)roundedMinDamage, maxValue: (int)roundedMaxDamage + 1);
                }

                damage = Math.Clamp(value: randomDamage, min: 0, max: (int)maxSourceDamage);

                if (isCriticalHit)
                {
                    damage = (int)(damage * source.CriticalHitMultiplier.CurrentValue); // Apply critical hit multiplier
                    Console.WriteLine(
                        value: $"{source.Name} lands a critical hit on {target.Name} for {damage} {source.CombatStyle} damage!");
                }
                else
                {
                    if (blocked)
                    {
                        if (damage <= 0)
                        {
                            Console.WriteLine(
                                value: $"{source.Name} attacks {target.Name}, but the attack is completely blocked!");
                        }
                        else
                        {
                            Console.WriteLine(
                                value:
                                $"{source.Name} attacks {target.Name}, but {target.Name} blocks most of the damage. However, {source.Name} still manages to deal {damage} {source.CombatStyle} damage.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            value: $"{source.Name} attacks {target.Name} for {damage} {source.CombatStyle} damage.");
                    }
                }

                if (damage > 0)
                {
                    target.TakeDamage(sourceActor: source, damage: damage);

                    if (target.ActorStats.Health.CurrentValue <= 0)
                    {
                        killed = true;
                        return (hit, blocked, killed, damage, isCriticalHit);
                    }
                }
            }
            else
            {
                Console.WriteLine(value: $"{source.Name} attacks {target.Name}, but misses!");
            }

            return (hit, blocked, killed, damage, isCriticalHit);
        }
        
        public virtual int CalculateAttackValue(IActor target, CombatStyle? style)
        {
            switch (style)
            {
                case CombatStyle.Melee:
                    return target.ActorStats.MeleeAttack.CurrentValue;
                case CombatStyle.Ranged:
                    return target.ActorStats.RangedAttack.CurrentValue;
                case CombatStyle.Magic:
                    return target.ActorStats.MagicAttack.CurrentValue;
                case CombatStyle.Hybrid:
                    // Assuming hybrid uses an average of all attack types; adjust as necessary for your game mechanics
                    return (target.ActorStats.MeleeAttack.CurrentValue + target.ActorStats.RangedAttack.CurrentValue + target.ActorStats.MagicAttack.CurrentValue) / 3;
                default:
                    return 0; // Default to 0 if no style is specified or it doesn't match any case
            }
        }
        
        public virtual int CalculateDefenseValue(IActor source, CombatStyle? style)
        {
            switch (style)
            {
                case CombatStyle.Melee:
                    return source.ActorStats.MeleeDefense.CurrentValue;
                case CombatStyle.Ranged:
                    return source.ActorStats.RangedDefense.CurrentValue;
                case CombatStyle.Magic:
                    return source.ActorStats.MagicDefense.CurrentValue;
                case CombatStyle.Hybrid:
                    // Assuming hybrid uses an average of all defense types; adjust as necessary for your game mechanics
                    return (source.ActorStats.MeleeDefense.CurrentValue + source.ActorStats.RangedDefense.CurrentValue + source.ActorStats.MagicDefense.CurrentValue) / 3;
                default:
                    return 0; // Default to 0 if no style is specified or it doesn't match any case
            }
        }

        public override bool Use(IActor? source, List<IActor?> targets)
        {
            if (source == null) return false;
            if (!Useable(source)) return false;
            SetSource(source);
            SetTargets(targets);
            if (targets.Count == 0) return false;
            
            foreach (var target in targets)
            {
                if(target == null) continue;
                var (hasHit, isBlocked, hasKilled, damage, isCriticalHit) = Attack(source: source, target: target, style: Style);
            }

            if (RequiredStatResourceCost != null)
            {
                var sourceStats = source.ActorStats!;
                foreach (var requiredResource in RequiredStatResourceCost.RequiredResources)
                {
                    switch (requiredResource.Type)
                    {
                        case StatResourceType.Health:
                            sourceStats.Health.CurrentValue -= requiredResource.RequiredAmount;
                            break;
                        case StatResourceType.Mana:
                            sourceStats.Mana.CurrentValue -= requiredResource.RequiredAmount;
                            break;
                        case StatResourceType.Stamina:
                            sourceStats.Stamina.CurrentValue -= requiredResource.RequiredAmount;
                            break;

                    }
                }
            }

            return true;
        }

        public BasicAttackAbility
        (
            string name,
            string description,
            AbilityType type,
            IRequiredStatResources? requiredStatResourceCost,
            TargetingType targetingType,
            TargetingScope targetingScope,
            ActorScopeType actorScopeType,
            CombatStyle? style = null,
            int baseDamage = 1,
            bool ignoreDefense = false,
            bool alwaysHits = false
        ) : base(name, description, type, requiredStatResourceCost, targetingType, targetingScope, actorScopeType)
        {
            Style = style;
            BaseDamage = baseDamage;
            IgnoreDefense = ignoreDefense;
            AlwaysHits = alwaysHits;
        }
    }
}