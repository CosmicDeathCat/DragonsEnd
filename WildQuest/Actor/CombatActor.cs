using System;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class CombatActor : Actor, ICombatant
{
    public virtual double DamageMultiplier { get; set; }
    public virtual double DamageReductionMultiplier { get; set; }

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
        long gold = 0L,
        IEquipmentItem[]? equipment = null,
        IItem[]? inventory = null,
        params IDropItem[] dropItems)
        : base(name, gender, characterClass, actorStats, combatStyle, damageMultiplier, damageReductionMultiplier, level, experience, gold, equipment, inventory, dropItems)
    {
    }

    public virtual (bool hasHit, bool isBlocked, bool hasKilled, int damage) Attack(ICombatant source, ICombatant target)
    {
        if (Target == null || Target != target)
        {
            Target = target;
        }
        bool hit = false;
        bool blocked = false;
        bool killed = false;
        int damage = 0;

        switch (CombatStyle)
        {
            case CombatStyle.Melee:
                (hit, blocked, killed, damage) = HandleAttack(source, target, source.ActorStats.MeleeAttack.CurrentValue, target.ActorStats.MeleeDefense.CurrentValue);
                break;
            case CombatStyle.Ranged:
                (hit, blocked, killed, damage) = HandleAttack(source, target, source.ActorStats.RangedAttack.CurrentValue, target.ActorStats.RangedDefense.CurrentValue);
                break;
            case CombatStyle.Magic:
                (hit, blocked, killed, damage) = HandleAttack(source, target, source.ActorStats.MagicAttack.CurrentValue, target.ActorStats.MagicDefense.CurrentValue);
                break;
            case CombatStyle.Hybrid:
                int avgAttack = (int)Math.Round((source.ActorStats.MeleeAttack.CurrentValue + source.ActorStats.RangedAttack.CurrentValue + source.ActorStats.MagicAttack.CurrentValue) / 3.0, MidpointRounding.AwayFromZero);
                int avgDefense = (int)Math.Round((target.ActorStats.MeleeDefense.CurrentValue + target.ActorStats.RangedDefense.CurrentValue + target.ActorStats.MagicDefense.CurrentValue) / 3.0, MidpointRounding.AwayFromZero);
                (hit, blocked, killed, damage) = HandleAttack(source, target, avgAttack, avgDefense);
                break;
        }

        return (hit, blocked, killed, damage);
    }

    public virtual (bool hit, bool blocked, bool killed, int damage) HandleAttack(ICombatant source, ICombatant target, int attackValue, int defenseValue)
    {
        if (Target == null || Target != target)
        {
            Target = target;
        }
        var rnd = new Random();
        bool hit = false;
        bool blocked = false;
        bool killed = false;
        int damage = 0;

        var attackRoll = rnd.Next(0, attackValue + 1);
        if (attackRoll > 0)
        {
            hit = true;
            var maxSourceDamage = Math.Round(attackValue * source.DamageMultiplier, MidpointRounding.AwayFromZero);
            var maxTargetDefense = Math.Round(defenseValue * target.DamageReductionMultiplier, MidpointRounding.AwayFromZero);
            blocked = maxSourceDamage - maxTargetDefense <= 0;
            var roundedMinDamage = Math.Round(double.Clamp(maxSourceDamage - maxTargetDefense, 1, maxSourceDamage), MidpointRounding.AwayFromZero);
            var roundedMaxDamage = Math.Round(double.Clamp(maxSourceDamage, roundedMinDamage, maxSourceDamage), MidpointRounding.AwayFromZero);
            int randomDamage = 0;
            if (blocked)
            {
                randomDamage = rnd.Next(0, (int)roundedMinDamage + 1);
            }
            else
            {
                randomDamage = rnd.Next((int)roundedMinDamage, (int)roundedMaxDamage + 1);
            }
            damage = int.Clamp(randomDamage, 0, (int)maxSourceDamage);
            string attackMessage = $"{source.Name} attacks {target.Name} for {damage} {source.CombatStyle} damage.";
            string blockedMessage = damage <= 0
                ? $"{source.Name} attacks {target.Name}, but the attack is completely blocked!"
                : $"{source.Name} attacks {target.Name}, but {target.Name} blocks most of the damage. However, {source.Name} still manages to deal {damage} {source.CombatStyle} damage.";
            if (blocked)
            {
                Console.WriteLine(blockedMessage);
            }
            else if (damage > 0)
            {
                Console.WriteLine(attackMessage);
                target.TakeDamage(source, damage);

                if (target.ActorStats.Health.CurrentValue <= 0)
                {
                    killed = true;
                    return (hit, blocked, killed, damage);
                }
            }
        }
        else
        {
            Console.WriteLine($"{source.Name} attacks {target.Name}, but misses!");
        }

        return (hit, blocked, killed, damage);
    }
}
