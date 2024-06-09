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
		long experience = -1L,
		double damageMultiplier = 1.00,
		double damageReductionMultiplier = 1.00,
		long gold = 0L,
		IEquipmentItem[]? equipment = null,
		IItem[]? inventory = null,
		params IDropItem[] dropItems) 
		: base(name, gender, characterClass, actorStats, level, experience, gold, equipment, inventory, dropItems)
	{
		DamageMultiplier = damageMultiplier;
		DamageReductionMultiplier = damageReductionMultiplier;
	}

	public virtual (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target)
	{
		Target = target;
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
					string attackMessage = $"{source.Name} attacks {target.Name} for {meleeDamage} {attackType.ToString()} damage.";
					string blockedMessage = meleeDamage <= 0 
						? $"{source.Name} attacks {target.Name}, but the attack is completely blocked!" 
						: $"{source.Name} attacks {target.Name}, but {target.Name} blocks most of the damage. However, {source.Name} still manages to deal {meleeDamage} {attackType.ToString()} damage.";
					if (meleeBlocked)
					{
						Console.WriteLine(blockedMessage);
					}
					else if(meleeDamage > 0)
					{
						Console.WriteLine(attackMessage);
						target.TakeDamage(source, meleeDamage);

						if(target.ActorStats.Health.CurrentValue <= 0)
						{
							killed = true;
							return (hit, meleeBlocked, killed, meleeDamage);
						}
					}
				}
				else
				{
					Console.WriteLine($"{source.Name} attacks {target.Name}, but misses!");
				}
				break;
			case CombatStyle.Ranged:
				break;
			case CombatStyle.Magic:
				break;
			case CombatStyle.Hybrid:
				break;
		}
		
		return (hit, meleeBlocked, killed, meleeDamage);
    }
}
