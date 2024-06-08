using System;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class CombatActor : Actor, ICombatant
{
	public virtual double DamageMultiplier { get; set; }

	public CombatActor() : base()
	{
		
	}
	public CombatActor(
		string name,
		Gender gender,
		CharacterClassType characterClass,
		ActorStats actorStats,
		int level = 1,
		double damageMultiplier = 10.00,
		long gold = 0,
		IEquipmentItem[]? equipment = null,
		IItem[]? inventory = null,
		params IDropItem[] dropItems) 
		: base(name, gender, characterClass, actorStats, level, gold, equipment, inventory, dropItems)
	{
		DamageMultiplier = damageMultiplier;
	}


	public virtual (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target)
	{
        var rnd = new Random();
		bool hit = false;
		bool blocked = false;
		bool killed = false;
		int damage = 0;
		var weapons = source.GetWeapons();
		var attackType = weapons.Length > 0 ? weapons[0]!.CombatStyle : CombatStyle.Melee;

		switch (attackType)
		{
			case CombatStyle.Melee:
				var meleeHit = rnd.Next(0,source.ActorStats.MeleeAttack.CurrentValue + 1);
				if(meleeHit > 0)
				{
					hit = true;
					var defense = target.ActorStats.MeleeDefense.CurrentValue;
					blocked = meleeHit - defense <= 0;
					damage = int.Clamp(meleeHit - defense,0, meleeHit);
					if(damage > 0)
					{	
						target.ActorStats.Health.CurrentValue -= damage;
						if(target.ActorStats.Health.CurrentValue <= 0)
						{
							killed = true; 
							target.IsAlive = false;
						}
					}
				}
				break;
			case CombatStyle.Ranged:
				break;
			case CombatStyle.Magic:
				break;
			case CombatStyle.Hybrid:
				break;
		}
		
		
		
		// var attackHit = rnd.Next(0,ActorStats.Attack.CurrentValue + 1);
		// if(attackHit > 0)
		// {
  //           hit = true;
  //           var defense = target.ActorStats.Defense.CurrentValue;
		// 	blocked = attackHit - defense <= 0;
		// 	damage = int.Clamp(attackHit - defense,0, attackHit);
		// 	if(damage > 0)
		// 	{	
  //               target.ActorStats.Health.CurrentValue -= damage;
		// 		if(target.ActorStats.Health.CurrentValue <= 0)
		// 		{
  //                  killed = true; 
		// 		   target.IsAlive = false;
  //               }
  //           }
  //       }
		return (hit, blocked, killed, damage);
    }
}
