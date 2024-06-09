namespace WildQuest.Interfaces;

public interface ICombatant : IActor
{
    public double DamageMultiplier { get; set; }
    public double DamageReductionMultiplier { get; set; }
    (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target);
}
