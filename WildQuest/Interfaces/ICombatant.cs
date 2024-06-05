namespace WildQuest.Interfaces;

public interface ICombatant : IActor
{
    (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target, params IWeaponItem?[] weapons);
}
