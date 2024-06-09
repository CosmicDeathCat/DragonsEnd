using System;
using WildQuest.Actor;
using WildQuest.Enemy;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest;

public static class Program
{
	public static void Main()
	{
		var player = new Player("Mony", Gender.Nonbinary, CharacterClassType.Freelancer,
			new ActorStats(100, 8, 4, 1, 1, 1, 1));

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
