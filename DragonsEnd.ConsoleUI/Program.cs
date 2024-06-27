using DragonsEnd.Actor.Enemy.Constants;
using DragonsEnd.Actor.Enemy.Database;
using DragonsEnd.Actor.Player;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Items.Database;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory;
using DragonsEnd.Items.Status.Interfaces;
using DragonsEnd.Stats;

namespace DragonsEnd.ConsoleUI;

public static class Program
{
    public static void Main()
    {
        var player = new Player(
            name: "Mony",
            gender: Gender.Nonbinary,
            characterClass: CharacterClassType.Mage,
            actorStats: new ActorStats(
                health: 100,
                mana: 100,
                stamina: 100,
                meleeAttack: 5,
                meleeDefense: 5,
                rangedAttack: 5,
                rangedDefense: 5,
                magicAttack: 5,
                magicDefense: 5),
            equipment: new IEquipmentItem[]
            {
                (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.OldMagicStaff)
            },
            inventory: new Inventory(
                gold: 0, 
                new []
            {
                ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                ItemDatabase.GetItems(itemName: ItemNames.ElixirOfLife),
                ItemDatabase.GetItems(itemName: ItemNames.ElixerOfTheAncients, 20),
            })
        );

        // var exl = player.Inventory?[ItemNames.ElixerOfTheAncients];

        // if (player.Inventory.FirstOrDefault(item => item?.Name == ItemNames.ElixerOfTheAncients) is ILevelingItem elixer)
        // {
        //     for (long i = elixer.Quantity; i >= 1; i--)
        //     {
        //         elixer.Use(player,player);
        //     }
        //     // elixer.Use(source: player, target: player);
        // }
        
        //player.ActorSkills.CookingSkill.Leveling.SetLevel(15);

        // Console.WriteLine(value: player);
        
        //player.ActorSkills.CookingSkill.Leveling.SetLevel(2);
        // Console.WriteLine(value: player);
        
        // var equippedWeapon = player.Equipment.OfType<IWeaponItem>().FirstOrDefault();
        // equippedWeapon?.Unequip(source: player, target: player, slot: EquipmentSlot.TwoHanded);
        //
        // Console.WriteLine(value: player);
        // var dagger = (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger);
        // dagger.Equip(source: player, target: player);
        //
        // Console.WriteLine(value: player);
        // var dagger2 = (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger);
        // dagger2.Equip(source: player, target: player);
        //
        // Console.WriteLine(value: player);
        // var bronzeSword = (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeSword);
        // bronzeSword.Equip(source: player, target: player);
        //
        // Console.WriteLine(value: player);
        // var bronzeShield = (IArmorItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeShield);
        // bronzeShield.Equip(source: player, target: player);
        //
        //
        // Console.WriteLine(value: player);
        // var dagger3 = (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger);
        // dagger3.Equip(source: player, target: player);

        // player.Leveling.SetLevel(100);

        var killCount = 0;
        while (player.IsAlive && killCount < 5)
        {
            var enemy = EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior);
            while (player.IsAlive && enemy.IsAlive)
            {
                var enemyHit = player.Attack(source: player, target: enemy);
                if (enemyHit.hasKilled)
                {
                    killCount++;
                    break; // Breaks out of the inner while loop
                }

                var playerHit = enemy.Attack(source: enemy, target: player);
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