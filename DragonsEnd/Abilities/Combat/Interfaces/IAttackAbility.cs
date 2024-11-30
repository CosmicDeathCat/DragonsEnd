using DragonsEnd.Abilities.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Abilities.Combat.Interfaces
{
    public interface IAttackAbility : IAbility
    {
        CombatStyle? Style { get; set; }
        int BaseDamage { get; set; }
        double DamageMultiplier { get; set; }
        bool IgnoreDefense { get; set; }
        bool AlwaysHits { get; set; }
        
        (bool hasHit, bool isBlocked, bool hasKilled, int damage, bool isCriticalHit) Attack        
        (
            IActor source,
            IActor target,
            CombatStyle? style = null
        );

        (bool hit, bool blocked, bool killed, int damage, bool isCriticalHit) HandleAttack
        (
            IActor source,
            IActor target,
            int attackValue,
            int defenseValue
        );

        int CalculateAttackValue(IActor target, CombatStyle? style);
        int CalculateDefenseValue(IActor source, CombatStyle? style);
    }
}