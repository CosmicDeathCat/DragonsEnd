using System;
using WildQuest.Actor;
using WildQuest.Enemy;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Items;
using WildQuest.Stats;

namespace WildQuest;

public static class Program
{
	public static void Main()
	{
		var player = new Player(
			name: "Mony",
			gender: Gender.Nonbinary,
			characterClass: CharacterClassType.Freelancer,
			actorStats: new ActorStats(
				health: 10000,
				meleeAttack: 8,
				meleeDefense: 4,
				rangedAttack: 1,
				rangedDefense: 1,
				magicAttack: 1,
				magicDefense: 1),
			damageMultiplier: 1.00,
			damageReductionMultiplier: 1.00,
			gold: 100,
			equipment:
			[
				(IWeaponItem)ItemDatabase.Items[ItemNames.BronzeDagger]
			],
			inventory:
			[
				ItemDatabase.GetItems(ItemNames.WeakHealthPotion, 1),
			]
		);

		var enemy = EnemyDatabase.Enemies[EnemyNames.Rat];

		// rat.IsAlive = false;

		// var ratLoot = rat.Loot();

		while (player.IsAlive && enemy.IsAlive)
		{
			var enemyHit = player.Attack(player, enemy);
			if (enemyHit.hasKilled) break;
			var playerHit = enemy.Attack(enemy, player);
			if(playerHit.hasKilled) break;
		}
	}
}
