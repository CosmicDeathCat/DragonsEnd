using System;
using System.Collections.Generic;
using DragonsEnd.Abilities.Combat.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Abilities.Combat
{
    public class BasicDefenseAbility : DurationAbility, IDefendAbility
    {
        public virtual int BaseDefense { get; set; }
        public double DefenseMultiplier { get; set; }
        public virtual bool IgnoreAttack { get; set; }
        public virtual bool AlwaysBlocks { get; set; }
        public virtual (bool hasBlocked, bool isHit, bool hasKilled, int damage, bool isCriticalHit) Defend(IActor source, IActor target, CombatStyle? style = null)
        {
            if (!Useable(source) || IsDurationComplete)
                return (false, false, false, 0, false);

            style ??= source.CombatStyle;  // Use the source's combat style if no override provided

            var attackValue = CalculateAttackValue(target, style);
            var defenseValue = CalculateDefenseValue(source, style);

            return HandleDefense(source, target, defenseValue, attackValue);
        }

        public virtual (bool blocked, bool hit, bool killed, int damage, bool isCriticalHit) HandleDefense(IActor source, IActor target, int defenseValue, int attackValue)
        {
            var rnd = new Random();
            var blocked = false;
            var hit = false;
            var killed = false;
            var damage = 0;
            var isCriticalHit = rnd.NextDouble() < target.ActorStats.CriticalHitChance.CurrentValue;

            if (IgnoreAttack)
            {
                attackValue = 0;  // Nullify all incoming attack values
            }

            if (AlwaysBlocks || defenseValue >= attackValue)
            {
                blocked = true;
            }

            if (blocked)
            {
                Console.WriteLine($"{source.Name} blocks the attack from {target.Name}!");
                return (true, false, false, 0, false);
            }

            // Calculate damage
            var effectiveDefense = Math.Round(defenseValue * source.DamageReductionMultiplier.CurrentValue * DefenseMultiplier, MidpointRounding.AwayFromZero);
            damage = Math.Max(0, (int)(attackValue - effectiveDefense));

            hit = damage > 0;
            if (hit)
            {
                Console.WriteLine($"{source.Name} fails to block the attack and receives {damage} damage from {target.Name}.");
                source.TakeDamage(target, damage);

                if (source.ActorStats.Health.CurrentValue <= 0)
                {
                    killed = true;
                    Console.WriteLine($"{source.Name} has been killed by {target.Name}.");
                }
            }

            return (blocked, hit, killed, damage, isCriticalHit);
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
    
            foreach (var target in targets)
            {
                if (target == null) continue;
                var (hasBlocked, isHit, hasKilled, damage, isCriticalHit) = Defend(source, target);
            }

            CurrentDuration++;
            return true;
        }
    }
}