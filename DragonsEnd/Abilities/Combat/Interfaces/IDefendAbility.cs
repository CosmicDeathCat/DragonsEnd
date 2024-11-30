using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Abilities.Combat.Interfaces
{
    public interface IDefendAbility : IDurationAbility
    {
        int BaseDefense { get; set; }
        double DefenseMultiplier { get; set; }
        bool IgnoreAttack { get; set; }
        bool AlwaysBlocks { get; set; }
        
        (bool hasBlocked, bool isHit, bool hasKilled, int damage, bool isCriticalHit) Defend
        (
            IActor source,
            IActor target,
            CombatStyle? style = null
        );

        (bool blocked, bool hit, bool killed, int damage, bool isCriticalHit) HandleDefense
        (
            IActor source,
            IActor target,
            int defenseValue,
            int attackValue
        );
        
        int CalculateAttackValue(IActor target, CombatStyle? style);
        int CalculateDefenseValue(IActor source, CombatStyle? style);
    }
}