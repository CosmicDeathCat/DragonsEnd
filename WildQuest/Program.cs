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
				health: 100,
				meleeAttack: 5,
				meleeDefense: 5,
				rangedAttack: 5,
				rangedDefense: 5,
				magicAttack: 5,
				magicDefense: 5),
			damageMultiplier: 1.00,
			damageReductionMultiplier: 1.00,
			gold: 0,
			equipment:
			[
				// (IWeaponItem)ItemDatabase.GetItems(ItemNames.BronzeGreatSword, 1),
				(IWeaponItem)ItemDatabase.GetItems(ItemNames.BronzeDagger, 1),
				(IWeaponItem)ItemDatabase.GetItems(ItemNames.BronzeDagger, 1),
				// (IWeaponItem)ItemDatabase.GetItems(ItemNames.BronzeDagger, 1)
				// (IWeaponItem)ItemDatabase.Items[ItemNames.BronzeDagger]
			],
			inventory:
			[
				ItemDatabase.GetItems(ItemNames.WeakHealthPotion, 1),
			]
		);
		
		player.Leveling.SetLevel(100);

		int killCount = 0;
		while (player.IsAlive && killCount < 5)
		{
			var enemy = EnemyDatabase.GetEnemy(EnemyNames.PunySlimeWarrior);
			while (player.IsAlive && enemy.IsAlive)
			{
				var enemyHit = player.Attack(player, enemy);
				if (enemyHit.hasKilled)
				{
					killCount++;
					break; // Breaks out of the inner while loop
				}
				var playerHit = enemy.Attack(enemy, player);
				if (playerHit.hasKilled)
				{
					break; // Breaks out of the inner while loop
				}
			}
			if (!enemy.IsAlive)
			{
				continue; // Continues with the next iteration of the outer while loop
			}
			if (!player.IsAlive)
			{
				break; // Breaks out of the outer while loop
			}
		}


	}
}
