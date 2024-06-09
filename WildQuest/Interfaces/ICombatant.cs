namespace WildQuest.Interfaces;

public interface ICombatant : IActor
{
    (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target);
    (bool hit, bool blocked, bool killed, int damage) HandleAttack(ICombatant source, ICombatant target, int attackValue, int defenseValue);
}
