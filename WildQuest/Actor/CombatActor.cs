using System;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class CombatActor(string name, Gender gender,CharacterClassType characterClass, ActorStats actorStats) : Actor(name, gender, characterClass, actorStats), ICombatant
{
	public virtual (bool hasHit, bool isBlocked,bool hasKilled, int damage) Attack(ICombatant source, ICombatant target, params IWeaponItem?[] weapons)
	{
        var rnd = new Random();
		bool hit = false;
		bool blocked = false;
		bool killed = false;
		int damage = 0;
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
