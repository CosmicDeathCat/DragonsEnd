using DragonsEnd.Actor.Enemy.Constants;
using DragonsEnd.Actor.Enemy.Database;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Actor.Player;
using DragonsEnd.Combat;
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Constants;
using DragonsEnd.Items.Database;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory;
using DragonsEnd.Items.Status.Interfaces;
using DragonsEnd.Party.Enemy;
using DragonsEnd.Party.Player;
using DragonsEnd.Stats;

namespace DragonsEnd.ConsoleUI;

public static class Program
{
    public static void Main()
    {
        var mony = new Player(
            name: "Mony",
            gender: Gender.Nonbinary,
            characterClass: CharacterClassType.Freelancer,
            actorStats: new ActorStats(
                health: 1000,
                mana: 100,
                stamina: 100,
                meleeAttack: 5,
                meleeDefense: 5,
                rangedAttack: 5,
                rangedDefense: 5,
                magicAttack: 5,
                magicDefense: 5),
            equipment:
            [
                (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeDagger)
            ],
            inventory: new Inventory(
                gold: 0,
                [
                    ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixirOfLife),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixerOfTheAncients, 20)
                ])
        );
        
        var stormi = new Player(
            name: "Stormi",
            gender: Gender.Female,
            characterClass: CharacterClassType.Warrior,
            actorStats: new ActorStats(
                health: 1000,
                mana: 100,
                stamina: 100,
                meleeAttack: 5,
                meleeDefense: 5,
                rangedAttack: 5,
                rangedDefense: 5,
                magicAttack: 5,
                magicDefense: 5),
            equipment:
            [
                (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.BronzeSword)
            ],
            inventory: new Inventory(
                gold: 0,
                [
                    ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixirOfLife),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixerOfTheAncients, 20)
                ])
        );
        
        var coty = new Player(
            name: "Coty",
            gender: Gender.Male,
            characterClass: CharacterClassType.Mage,
            actorStats: new ActorStats(
                health: 1000,
                mana: 100,
                stamina: 100,
                meleeAttack: 5,
                meleeDefense: 5,
                rangedAttack: 5,
                rangedDefense: 5,
                magicAttack: 5,
                magicDefense: 5),
            equipment:
            [
                (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.OldMagicStaff)
            ],
            inventory: new Inventory(
                gold: 0,
                [
                    ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixirOfLife),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixerOfTheAncients, 20)
                ])
        );
        
        var daerpyre = new Player(
            name: "Daerpyre",
            gender: Gender.Male,
            characterClass: CharacterClassType.Archer,
            actorStats: new ActorStats(
                health: 1000,
                mana: 100,
                stamina: 100,
                meleeAttack: 5,
                meleeDefense: 5,
                rangedAttack: 5,
                rangedDefense: 5,
                magicAttack: 5,
                magicDefense: 5),
            equipment:
            [
                (IWeaponItem)ItemDatabase.GetItems(itemName: ItemNames.FeebleWoodShortbow)
            ],
            inventory: new Inventory(
                gold: 0,
                [
                    ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixirOfLife),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixerOfTheAncients, 20)
                ])
        );
        
        var playerManager = new PlayerPartyManager(
            members:
            [
                mony,
                stormi,
                coty,
                daerpyre
            ],
            sharedInventory: new Inventory(
                gold: 0,
                [
                    ItemDatabase.GetItems(itemName: ItemNames.WeakHealthPotion),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixirOfLife),
                    ItemDatabase.GetItems(itemName: ItemNames.ElixerOfTheAncients, 20)
                ])
        );
        
        playerManager.SyncInventories();
        
        var slimeWarriorGroup = new EnemyPartyManager(
            members:
            [
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage),
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage)
                // EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior)
            ]);

        var enemyGroups = new List<EnemyPartyManager>
        {
            new(
                members:
                [
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeArcher),
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage)
                ]
            ),
            new(
                members:
                [
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior)
                ]
            ),
            new(
                members:
                [
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage),
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage),
                    EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage)
                ]
            ),
        };
        
        var enemyPartyManager = new EnemyPartyManager(
            members:
            [
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeArcher),
                EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeMage)
            ]
        );
        // var players = new List<IActor>()
        // {
        //     mony,
        //     stormi,
        //     coty,
        //     daerpyre,
        // };

        // var enemies = new List<IActor>()
        // {
        //     EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
        //     EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
        //     EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
        //     EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior),
        // };

        ICombatSystem combatSystem = new CombatSystem(playerManager.Members, slimeWarriorGroup.Members);
        combatSystem.StartCombat();
        
        // ICombatSystem combatSystem = new CombatSystem(playerManager.Members, enemyPartyManager.Members);
        // combatSystem.StartCombat();
        // foreach (var enemyGroup in enemyGroups)
        // {
        //     combatSystem.Setup(playerManager.Members, enemyGroup.Members);
        //     combatSystem.StartCombat();
        // }
        
        Console.WriteLine(playerManager.Members.Count);
        
        // var wep = player.Inventory?[ItemNames.BronzeDagger] as IWeaponItem;
        // var bronzeHelm = (IArmorItem)ItemDatabase.GetItems(ItemNames.BronzeHelmet);
        // bronzeHelm.Equip(player, player);

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

        // var killCount = 0;
        // while (player.IsAlive && killCount < 5)
        // {
        //     var enemy = EnemyDatabase.GetEnemy(enemyName: EnemyNames.PunySlimeWarrior);
        //     while (player.IsAlive && enemy.IsAlive)
        //     {
        //         var enemyHit = player.Attack(source: player, target: enemy);
        //         if (enemyHit.hasKilled)
        //         {
        //             killCount++;
        //             break; // Breaks out of the inner while loop
        //         }
        //
        //         var playerHit = enemy.Attack(source: enemy, target: player);
        //         if (playerHit.hasKilled)
        //         {
        //             break; // Breaks out of the inner while loop
        //         }
        //     }
        //
        //     if (!enemy.IsAlive)
        //     {
        //         continue; // Continues with the next iteration of the outer while loop
        //     }
        //
        //     if (!player.IsAlive)
        //     {
        //         break; // Breaks out of the outer while loop
        //     }
        // }
    }
}