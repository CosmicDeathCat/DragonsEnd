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
		int level = 1,
		double damageMultiplier = 1.00,
		double damageReductionMultiplier = 1.00,
		long gold = 0,
		IEquipmentItem[]? equipment = null,
		IItem[]? inventory = null,
		params IDropItem[] dropItems) 
		: base(name, gender, characterClass, actorStats, level, gold, equipment, inventory, dropItems)
	{
		DamageMultiplier = damageMultiplier;
		DamageReductionMultiplier = damageReductionMultiplier;
	}


	public virtual (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target)
	{
        var rnd = new Random();
		bool hit = false;
		bool meleeBlocked = false;
		bool killed = false;
		int meleeDamage = 0;
		var weapons = source.GetWeapons();
		var attackType = weapons.Length > 0 ? weapons[0]!.CombatStyle : CombatStyle.Melee;

		switch (attackType)
		{
			case CombatStyle.Melee:
				var meleeHit = rnd.Next(0,source.ActorStats.MeleeAttack.CurrentValue + 1);
				if(meleeHit > 0)
				{
					hit = true;
					var maxSourceMeleeDamage = Math.Round(source.ActorStats.MeleeAttack.CurrentValue * source.DamageMultiplier, MidpointRounding.AwayFromZero);
					var maxTargetMeleeDefense = Math.Round(target.ActorStats.MeleeDefense.CurrentValue * target.DamageReductionMultiplier, MidpointRounding.AwayFromZero);
					meleeBlocked = maxSourceMeleeDamage - maxTargetMeleeDefense <= 0;
					var roundedMinMeleeDamage = Math.Round(double.Clamp(maxSourceMeleeDamage - maxTargetMeleeDefense, 1, maxSourceMeleeDamage), MidpointRounding.AwayFromZero);
					var roundedMaxMeleeDamage = Math.Round(double.Clamp(maxSourceMeleeDamage, roundedMinMeleeDamage, maxSourceMeleeDamage), MidpointRounding.AwayFromZero);
					int randomMeleeDamage = 0;
					if(meleeBlocked)
					{
						randomMeleeDamage = rnd.Next(0, (int)roundedMinMeleeDamage + 1);
					}
					else
					{
						randomMeleeDamage = rnd.Next((int)roundedMinMeleeDamage, (int)roundedMaxMeleeDamage + 1);
					}
					meleeDamage = int.Clamp(randomMeleeDamage,0, (int)maxSourceMeleeDamage);
					if(meleeDamage > 0)
					{	
						target.ActorStats.Health.CurrentValue -= meleeDamage;
						if(target.ActorStats.Health.CurrentValue <= 0)
						{
							killed = true;
							return (hit, meleeBlocked, killed, meleeDamage);
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
		return (hit, meleeBlocked, killed, meleeDamage);
    }
}
