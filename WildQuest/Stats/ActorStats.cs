using System;

namespace WildQuest.Stats;

[Serializable]
public class ActorStats 
{         
    public virtual IntStat Health { get; set; }    
    public virtual IntStat MeleeAttack { get; set; }
    public virtual IntStat RangedAttack { get; set; }
    public virtual IntStat MagicAttack { get; set; }
    public virtual IntStat MeleeDefense { get; set; }
    public virtual IntStat RangedDefense { get; set; }
    public virtual IntStat MagicDefense { get; set; }
    public virtual DoubleStat CriticalHitChance { get; set; } // Configurable critical hit chance
	
    public ActorStats(int health, int meleeAttack, int meleeDefense, int rangedAttack, int rangedDefense, int magicAttack, int magicDefense, double criticalHitChance = 0.05)
    {
        Health = new IntStat(health);
        MeleeAttack = new IntStat(meleeAttack);
        RangedAttack = new IntStat(rangedAttack);
        MagicAttack = new IntStat(magicAttack);
        MeleeDefense = new IntStat(meleeDefense);
        RangedDefense = new IntStat(rangedDefense);
        MagicDefense = new IntStat(magicDefense);
        CriticalHitChance = new DoubleStat(criticalHitChance);
    }
	
    public ActorStats(IntStat health, IntStat meleeAttack, IntStat meleeDefense, IntStat rangedAttack, IntStat rangedDefense, IntStat magicAttack, IntStat magicDefense, DoubleStat criticalHitChance)
    {
        Health = health;
        MeleeAttack = meleeAttack;
        RangedAttack = rangedAttack;
        MagicAttack = magicAttack;
        MeleeDefense = meleeDefense;
        RangedDefense = rangedDefense;
        MagicDefense = magicDefense;
        CriticalHitChance = criticalHitChance;
    }
	
    public static ActorStats operator +(ActorStats a, ActorStats b)
    {
        return new ActorStats(a.Health + b.Health, a.MeleeAttack + b.MeleeAttack,
            a.MeleeDefense + b.MeleeDefense, a.RangedAttack + b.RangedAttack,
            a.RangedDefense + b.RangedDefense, a.MagicAttack + b.MagicAttack,
            a.MagicDefense + b.MagicDefense, a.CriticalHitChance + b.CriticalHitChance);
    }
	
    public static ActorStats operator -(ActorStats a, ActorStats b)
    {
        return new ActorStats(a.Health - b.Health, a.MeleeAttack - b.MeleeAttack,
            a.MeleeDefense - b.MeleeDefense, a.RangedAttack - b.RangedAttack,
            a.RangedDefense - b.RangedDefense, a.MagicAttack - b.MagicAttack,
            a.MagicDefense - b.MagicDefense, a.CriticalHitChance - b.CriticalHitChance);
    }
	
    public static ActorStats operator *(ActorStats a, ActorStats b)
    {
        return new ActorStats(a.Health * b.Health, a.MeleeAttack * b.MeleeAttack,
            a.MeleeDefense * b.MeleeDefense, a.RangedAttack * b.RangedAttack,
            a.RangedDefense * b.RangedDefense, a.MagicAttack * b.MagicAttack,
            a.MagicDefense * b.MagicDefense, a.CriticalHitChance * b.CriticalHitChance);
    }
	
    public static ActorStats operator /(ActorStats a, ActorStats b)
    {
        return new ActorStats(a.Health / b.Health, a.MeleeAttack / b.MeleeAttack,
            a.MeleeDefense / b.MeleeDefense, a.RangedAttack / b.RangedAttack,
            a.RangedDefense / b.RangedDefense, a.MagicAttack / b.MagicAttack,
            a.MagicDefense / b.MagicDefense, a.CriticalHitChance / b.CriticalHitChance);
    }

    public void AddModifier(ActorStats modifier)
    {
        Health.AddModifier(modifier.Health.CurrentValue);
        MeleeAttack.AddModifier(modifier.MeleeAttack.CurrentValue);
        RangedAttack.AddModifier(modifier.RangedAttack.CurrentValue);
        MagicAttack.AddModifier(modifier.MagicAttack.CurrentValue);
        MeleeDefense.AddModifier(modifier.MeleeDefense.CurrentValue);
        RangedDefense.AddModifier(modifier.RangedDefense.CurrentValue);
        MagicDefense.AddModifier(modifier.MagicDefense.CurrentValue);
        CriticalHitChance.AddModifier(modifier.CriticalHitChance.CurrentValue);
    }

    public void RemoveModifier(ActorStats modifier)
    {
        Health.RemoveModifier(modifier.Health.CurrentValue);
        MeleeAttack.RemoveModifier(modifier.MeleeAttack.CurrentValue);
        RangedAttack.RemoveModifier(modifier.RangedAttack.CurrentValue);
        MagicAttack.RemoveModifier(modifier.MagicAttack.CurrentValue);
        MeleeDefense.RemoveModifier(modifier.MeleeDefense.CurrentValue);
        RangedDefense.RemoveModifier(modifier.RangedDefense.CurrentValue);
        MagicDefense.RemoveModifier(modifier.MagicDefense.CurrentValue);
        CriticalHitChance.RemoveModifier(modifier.CriticalHitChance.CurrentValue);
    }
}
