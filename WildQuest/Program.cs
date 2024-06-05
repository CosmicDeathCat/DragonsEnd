using System;
using WildQuest.Actor;
using WildQuest.Enums;
using WildQuest.Stats;

namespace WildQuest;

public static class Program
{
	public static void Main()
	{
		var player = new Player("Mony", Gender.Nonbinary, CharacterClassType.Freelancer, 
			new ActorStats(10,1,1,1,1,1,1));

		Console.WriteLine(player.Leveling);
		player.Leveling.GainExperience(25);
		Console.WriteLine(player.Leveling);

		player.Leveling.GainExperience(25);
		Console.WriteLine(player.Leveling);
		player.Leveling.LoseExperience(25);
		Console.WriteLine(player.Leveling);
		player.Leveling.LoseExperience(25);
		Console.WriteLine(player.Leveling);
		player.Leveling.LoseExperience(25);
		Console.WriteLine(player.Leveling);

		// player.Leveling.LevelUp();
		// Console.WriteLine(player.Leveling);
		// player.Leveling.LevelUp();
		// Console.WriteLine(player.Leveling);
		//
		// player.Leveling.LevelDown();
		// Console.WriteLine(player.Leveling);
		//
		// player.Leveling.LevelUp();
		// Console.WriteLine(player.Leveling);
		// player.Leveling.LevelUp();
		// Console.WriteLine(player.Leveling);
		// Console.ReadLine();

		//       var player = new Player("Hero", Gender.Male, CharacterClassType.Freelancer, new ActorStats(10,3,2));
		// var enemy = new CombatActor("Enemy", Gender.Male, CharacterClassType.Warrior, new ActorStats(5,1,1));
		//
		// while(player.IsAlive && enemy.IsAlive)
		// {
		//           Console.WriteLine($"{player.Name} attacks {enemy.Name}");
		// 	var enemyHit = player.Attack(player,enemy);
		//           if(!enemyHit.hasHit)
		// 	{
		//               Console.WriteLine($"{player.Name}'s attack misses!");
		//           }
		// 	if(enemyHit.isBlocked)
		// 	{
		//               Console.WriteLine($"{enemy.Name} blocked attack!");
		//           }
		// 	if(enemyHit.damage > 0)
		// 	{
		//               Console.WriteLine($"{player.Name} deals {enemyHit.damage} damage to {enemy.Name}");
		//           }
		//           //Console.WriteLine($"Hit?{enemyHit.hasHit} Blocked?{enemyHit.isBlocked} Damage:{enemyHit.damage}");
		// 	if(enemyHit.hasKilled)
		// 	{
		//               Console.WriteLine($"{enemy.Name} killed!");
		// 		break;
		//           }
		// 	Console.WriteLine($"{enemy.Name} attacks {player.Name}");
		// 	var playerHit = enemy.Attack(enemy,player);
		// 	if(!playerHit.hasHit)
		// 	{
		//               Console.WriteLine($"{enemy.Name}'s attack misses!");
		//           }
		// 	if(playerHit.isBlocked)
		// 	{
		//               Console.WriteLine($"{player.Name} blocked attack!");
		//           }
		// 	if(playerHit.damage > 0)
		// 	{
		//               Console.WriteLine($"{enemy.Name} deals {playerHit.damage} damage to {player.Name}");
		//           }
		// 	//Console.WriteLine($"Hit?{playerHit.hasHit} Blocked?{playerHit.isBlocked} Damage:{playerHit.damage}");
		// 	if(playerHit.hasKilled)
		// 	{
		//               Console.WriteLine($"{player.Name} has died!");
		// 		break;
		//           }
		//       }

	}
}
