namespace WildQuest.Stats;

[System.Serializable]
public class ActorStats 
{         
    public virtual Stat Health {get;set;}    
	public virtual Stat MeleeAttack {get;set;}
	public virtual Stat RangedAttack {get;set;}
	public virtual Stat MagicAttack {get;set;}
	public virtual Stat MeleeDefense {get;set;}
	public virtual Stat RangedDefense {get;set;}
	public virtual Stat MagicDefense {get;set;}
	
	public ActorStats(int health, int meleeAttack, int meleeDefense, int rangedAttack, int rangedDefense, int magicAttack, int magicDefense)
	{
		Health = new Stat(health);
		MeleeAttack = new Stat(meleeAttack);
		RangedAttack = new Stat(rangedAttack);
		MagicAttack = new Stat(magicAttack);
		MeleeDefense = new Stat(meleeDefense);
		RangedDefense = new Stat(rangedDefense);
		MagicDefense = new Stat(magicDefense);
	}
	
	public ActorStats(Stat health, Stat meleeAttack, Stat meleeDefense, Stat rangedAttack, Stat rangedDefense, Stat magicAttack, Stat magicDefense)
	{
		Health = health;
		MeleeAttack = meleeAttack;
		RangedAttack = rangedAttack;
		MagicAttack = magicAttack;
		MeleeDefense = meleeDefense;
		RangedDefense = rangedDefense;
		MagicDefense = magicDefense;
	}
	
	public static ActorStats operator +(ActorStats a, ActorStats b)
	{
		return new ActorStats(a.Health + b.Health, a.MeleeAttack + b.MeleeAttack,
			a.MeleeDefense + b.MeleeDefense, a.RangedAttack + b.RangedAttack,
			a.RangedDefense + b.RangedDefense, a.MagicAttack + b.MagicAttack,
			a.MagicDefense + b.MagicDefense);
	}
	
	public static ActorStats operator -(ActorStats a, ActorStats b)
	{
		return new ActorStats(a.Health - b.Health, a.MeleeAttack - b.MeleeAttack,
			a.MeleeDefense - b.MeleeDefense, a.RangedAttack - b.RangedAttack,
			a.RangedDefense - b.RangedDefense, a.MagicAttack - b.MagicAttack,
			a.MagicDefense - b.MagicDefense);
	}
	
	public static ActorStats operator *(ActorStats a, ActorStats b)
	{
		return new ActorStats(a.Health * b.Health, a.MeleeAttack * b.MeleeAttack,
			a.MeleeDefense * b.MeleeDefense, a.RangedAttack * b.RangedAttack,
			a.RangedDefense * b.RangedDefense, a.MagicAttack * b.MagicAttack,
			a.MagicDefense * b.MagicDefense);
	}
	
	public static ActorStats operator /(ActorStats a, ActorStats b)
	{
		return new ActorStats(a.Health / b.Health, a.MeleeAttack / b.MeleeAttack,
			a.MeleeDefense / b.MeleeDefense, a.RangedAttack / b.RangedAttack,
			a.RangedDefense / b.RangedDefense, a.MagicAttack / b.MagicAttack,
			a.MagicDefense / b.MagicDefense);
	}
}
