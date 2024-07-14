using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Actions.Player.Constants;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Actor.Player.Interfaces;
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.ConsoleHandlers.Input;
using DragonsEnd.Enums;
using DragonsEnd.Items;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Actor.Player
{
    public class Player : Actor, IPlayer
    {
        public Player
        (
            string name,
            Gender gender,
            CharacterClassType characterClass,
            ActorStats? actorStats = null,
            IActorSkills? actorSkills = null,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            IEquipmentItem[]? equipment = null,
            IInventory? inventory = null,
            ILootConfig? lootConfig = null,
            ILootContainer? lootContainer = null
        )
        {
            Name = name;
            Gender = gender;
            CharacterClass = characterClass;
            ActorStats = actorStats;
            IsAlive = true;
            DamageMultiplier = new DoubleStat(baseValue: damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(baseValue: criticalHitMultiplier);
            Inventory = inventory;
            LootConfig = lootConfig;
            LootContainer = lootContainer;

            if (actorSkills != null)
            {
                ActorSkills = actorSkills;
                ActorSkills?.UpdateActor(actor: this);
            }
            else
            {
                ActorSkills = new ActorSkills(actor: this);
            }

            if (equipment != null)
            {
                foreach (var item in equipment)
                {
                    item.Equip(source: this, target: this);
                }
            }
            else
            {
                Equipment = new IEquipmentItem[Enum.GetNames(enumType: typeof(EquipmentSlot)).Length];
            }

            if (LootContainer != null)
            {
                if (LootContainer?.Items.Count > 0)
                {
                }
                else
                {
                    // Default to using the provided inventory and equipment as drop items with default drop rates
                    foreach (var item in Inventory.Items)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        LootContainer?.Items.Add(item: new Item(item: item));
                    }

                    if (equipment != null)
                    {
                        foreach (var item in equipment)
                        {
                            lootContainer?.Items.Add(item: new Item(item: item));
                        }
                    }
                }
            }
        }

        public override bool TakeTurn(ICombatContext combatContext, List<IActor> targets, List<IActor> allies)
        {
            if (IsAlive && targets.Any(predicate: e => e.IsAlive))
            {
                Console.WriteLine(value: $"{Name} is taking their turn.");
                TurnCount++;
                PerformCombatAction(combatContext: combatContext, targets: targets, allies: allies);
                // var target = ChooseTarget(targets: targets);
                // if (target == null)
                // {
                //     return false;
                // }
                //
                // var attackResult = Attack(source: this, target: target);
                return true;
            }

            return false;
        }
        
        public virtual bool PerformCombatAction(ICombatContext combatContext, List<IActor> targets, List<IActor> allies)
        {
            var input = ConsoleInputHandler.GetInput<string> (message: "Choose an action:", allowNumberInput: true, availableStringOptions: new [] 
            {
                CombatActionNames.Attack,
                CombatActionNames.Defend,
                CombatActionNames.Skills,
                CombatActionNames.UseItem,
                CombatActionNames.UnequipItem,
                CombatActionNames.Flee,
                NonCombatActionNames.DisplayInventory,
                NonCombatActionNames.DisplayEquipment,
                NonCombatActionNames.DisplayStats,
                NonCombatActionNames.DisplaySkills
            });
            
            // var input = Console.ReadLine();
            switch (input)
            {
                case CombatActionNames.Attack: // Attack
                    //TODO: Implement choosing a target based on weapon and skills make Attack a ability that can be customized
                    var attackTargets = ChooseTargets(enemies: targets, allies: allies);
                    if (!attackTargets.Any())
                    {
                        return false;
                    }

                    foreach (var attackTarget in attackTargets)
                    {
                        if (attackTarget == null)
                        {
                            continue;
                        }

                        var attackResult = Attack(source: this, target: attackTarget);
                    }
                    break;
                case CombatActionNames.Defend: // Defend
                    Console.WriteLine(value: "You defend yourself. (Not yet implemented)");
                    break;
                case CombatActionNames.Skills: // Skills
                    Console.WriteLine(value: "Skills (Not yet implemented)");
                    break;
                case CombatActionNames.UseItem: // Use Item
                    DisplayInventory();
                    var item = ChooseItem();
                    if (item != null)
                    {
                        var useItemTargets = ChooseTargets(enemies: targets, allies: allies, targetingScope: item.TargetingScope, targetingType: item.TargetingType, actorScopeType: item.ActorScopeType);
                        item.Use(source: this, useItemTargets);
                    }
                    break;
                case CombatActionNames.UnequipItem: // Equip/Unequip Item
                    Console.WriteLine(value: "Unequip Item (Not yet implemented)");
                    break;
                case CombatActionNames.Flee: // Flee
                    Console.WriteLine(value: "You attempt to flee. (Not yet implemented)");
                    break;
                case NonCombatActionNames.DisplayInventory: // Display Inventory
                    DisplayInventory();
                    PerformCombatAction(combatContext: combatContext, targets: targets, allies: allies);
                    break;
                case NonCombatActionNames.DisplayEquipment: // Display Equipment
                    DisplayEquipment();
                    PerformCombatAction(combatContext: combatContext, targets: targets, allies: allies);
                    break;
                case NonCombatActionNames.DisplayStats: // Display Stats
                    DisplayStats();
                    PerformCombatAction(combatContext: combatContext, targets: targets, allies: allies);
                    break;
                case NonCombatActionNames.DisplaySkills: // Display Skills
                    DisplaySkills();
                    PerformCombatAction(combatContext: combatContext, targets: targets, allies: allies);                    break;
                default:
                    Console.WriteLine(value: "Invalid choice. Please choose a valid action.");
                    PerformCombatAction(combatContext: combatContext, targets: targets, allies: allies);
                    break;
            }

            return true;
        }

        public virtual IItem? ChooseItem()
        {
            Console.WriteLine(value: "Choose an item to use:");
            if (Inventory == null || Inventory.Items.Count == 0)
            {
                Console.WriteLine(value: "No items in inventory.");
                return null;
            }

            var chosenIndex = -1;
            while (chosenIndex < 0 || chosenIndex > Inventory.Items.Count)
            {
                Console.Write(value: "Enter the number of the item: ");
                var input = Console.ReadLine();
                if (int.TryParse(s: input, result: out var index) && index >= 1 && index <= Inventory.Items.Count)
                {
                    chosenIndex = index - 1;
                }
                else
                {
                    Console.WriteLine(value: "Invalid choice. Please choose a valid item.");
                }
            }

            return Inventory.Items[index: chosenIndex];
        }

        public virtual void DisplayInventory()
        {
            Console.WriteLine(value: "Inventory:");
            if (Inventory == null)
            {
                Console.WriteLine(value: "No items in inventory.");
                return;
            }

            for (var index = 1; index <= Inventory.Items.Count; index++)
            {
                var item = Inventory.Items[index -1];
                if (item == null)
                {
                    continue;
                }

                Console.WriteLine(value: $"[{index}]: {item.Name} x{item.Quantity}");
            }
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine(value: "Stats:");
            if (ActorStats == null)
            {
                Console.WriteLine(value: "No stats available.");
                return;
            }

            Console.WriteLine(value: $"Health: {ActorStats.Health.CurrentValue}/{ActorStats.Health.MaxValue}");
            Console.WriteLine(value: $"Mana: {ActorStats.Mana.CurrentValue}/{ActorStats.Mana.MaxValue}");
            Console.WriteLine(value: $"Stamina: {ActorStats.Stamina.CurrentValue}/{ActorStats.Stamina.MaxValue}");
            Console.WriteLine(value: $"Combat Level: {CombatLevel}");
            Console.WriteLine(value: $"Melee Attack: {ActorStats.MeleeAttack.CurrentValue}/{ActorStats.MeleeAttack.MaxValue}");
            Console.WriteLine(value: $"Melee Defense: {ActorStats.MeleeDefense.CurrentValue}/{ActorStats.MeleeDefense.MaxValue}");
            Console.WriteLine(value: $"Ranged Attack: {ActorStats.RangedAttack.CurrentValue}/{ActorStats.RangedAttack.MaxValue}");
            Console.WriteLine(value: $"Ranged Defense: {ActorStats.RangedDefense.CurrentValue}/{ActorStats.RangedDefense.MaxValue}");
            Console.WriteLine(value: $"Magic Attack: {ActorStats.MagicAttack.CurrentValue}/{ActorStats.MagicAttack.MaxValue}");
            Console.WriteLine(value: $"Magic Defense: {ActorStats.MagicDefense.CurrentValue}/{ActorStats.MagicDefense.MaxValue}");
            Console.WriteLine(value: $"Critical Hit Chance: {ActorStats.CriticalHitChance.CurrentValue}/{ActorStats.CriticalHitChance.MaxValue}");
        }

        public virtual void DisplayEquipment()
        {
            Console.WriteLine(value: "Equipment:");

            var slotNames = Enum.GetNames(enumType: typeof(EquipmentSlot));
            for (int i = 2; i <= Equipment.Length; i++)
            {
                var slotName = slotNames[i-1];
                var item = Equipment[i-1];
                if (item == null)
                {
                    Console.WriteLine(value: $"[{i-1} - {slotName}]: Empty");
                    continue;
                }
                Console.WriteLine(value: $"[{i-1} - {slotName}]: {item.Name}");
            } 
            
        }

        public virtual void DisplaySkills()
        {
            Console.WriteLine(value: "Skills:");
            if (ActorSkills == null)
            {
                Console.WriteLine(value: "No skills available.");
                return;
            }
            
            Console.WriteLine(value: $"Melee: {ActorSkills.MeleeSkill.Leveling.CurrentLevel} Experience: {ActorSkills.MeleeSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Ranged: {ActorSkills.RangedSkill.Leveling.CurrentLevel} Experience: {ActorSkills.RangedSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Magic: {ActorSkills.MagicSkill.Leveling.CurrentLevel} Experience: {ActorSkills.MagicSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Alchemy: {ActorSkills.AlchemySkill.Leveling.CurrentLevel} Experience: {ActorSkills.AlchemySkill.Leveling.Experience}");
            Console.WriteLine(value: $"Cooking: {ActorSkills.CookingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.CookingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Crafting: {ActorSkills.CraftingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.CraftingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Enchanting: {ActorSkills.EnchantingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.EnchantingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Fishing: {ActorSkills.FishingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.FishingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Fletching: {ActorSkills.FletchingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.FletchingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Foraging: {ActorSkills.ForagingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.ForagingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Mining: {ActorSkills.MiningSkill.Leveling.CurrentLevel} Experience: {ActorSkills.MiningSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Smithing: {ActorSkills.SmithingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.SmithingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Ranching: {ActorSkills.RanchingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.RanchingSkill.Leveling.Experience}");
            Console.WriteLine(value: $"Woodcutting: {ActorSkills.WoodcuttingSkill.Leveling.CurrentLevel} Experience: {ActorSkills.WoodcuttingSkill.Leveling.Experience}");
        }

        public virtual List<IActor?> ChooseTargets(List<IActor> enemies, List<IActor> allies, TargetingScope targetingScope = TargetingScope.Single, TargetingType targetingType = TargetingType.Enemy, ActorScopeType actorScopeType = ActorScopeType.Alive)
        {
            var targets = new List<IActor?>();
            var selectedTargets = new List<IActor?>();
            switch (targetingType)
            {
                case TargetingType.Self:
                    switch (actorScopeType)
                    {
                        case ActorScopeType.Alive:
                            if (IsAlive)
                            {
                                targets.Add(item: this);
                            }
                            break;
                        case ActorScopeType.Dead:
                            if (!IsAlive)
                            {
                                targets.Add(item: this);
                            }
                            break;
                        case ActorScopeType.All:
                            targets.Add(item: this);
                            break;
                    }
                    break;
                case TargetingType.Ally:
                    switch (actorScopeType)
                    {
                        case ActorScopeType.Alive:
                            targets.AddRange(collection: allies.Where(x=> x.IsAlive));
                            break;
                        case ActorScopeType.Dead:
                            targets.AddRange(collection: allies.Where(x=> !x.IsAlive));
                            break;
                        case ActorScopeType.All:
                            targets.AddRange(collection: allies);
                            break;
                    }
                    break;
                case TargetingType.Enemy:
                    switch (actorScopeType)
                    {
                        case ActorScopeType.Alive:
                            targets.AddRange(collection: enemies.Where(x=> x.IsAlive));
                            break;
                        case ActorScopeType.Dead:
                            targets.AddRange(collection: enemies.Where(x=> !x.IsAlive));
                            break;
                        case ActorScopeType.All:
                            targets.AddRange(collection: enemies);
                            break;
                    }
                    break;
                case TargetingType.All:
                    switch (actorScopeType)
                    {
                        case ActorScopeType.Alive:
                            targets.AddRange(collection: allies.Where(x=> x.IsAlive));
                            targets.AddRange(collection: enemies.Where(x=> x.IsAlive));
                            break;
                        case ActorScopeType.Dead:
                            targets.AddRange(collection: allies.Where(x=> !x.IsAlive));
                            targets.AddRange(collection: enemies.Where(x=> !x.IsAlive));
                            break;
                        case ActorScopeType.All:
                            targets.AddRange(collection: allies);
                            targets.AddRange(collection: enemies);
                            break;
                    }
                    break;
            }
            switch (targetingScope)
            {
                case TargetingScope.Single:
                    Console.WriteLine(value: "Choose a single target");
                    for (var i = 1; i <= targets.Count; i++)
                    {
                        var target = targets[index: i - 1];
                        Console.WriteLine(value: $"{i}: {target?.Name} (Combat Level: {target?.CombatLevel} HP: {target?.ActorStats?.Health.CurrentValue}/{target?.ActorStats?.Health.MaxValue})");
                    }
                    var singleSelectIndex = -1;
                    while (singleSelectIndex < 0 || singleSelectIndex > targets.Count)
                    {
                        Console.Write(value: "Enter the number of the target: ");
                        var input = Console.ReadLine();
                        if (int.TryParse(s: input, result: out var index) && index >= 1 && index <= targets.Count)
                        {
                            singleSelectIndex = index - 1;
                            selectedTargets.Add(item: targets[index: singleSelectIndex]);
                        }
                        else
                        {
                            Console.WriteLine(value: "Invalid choice. Please choose a valid target.");
                        }
                    }
                    break;
                case TargetingScope.Multiple:
                    Console.WriteLine(value: "Choose multiple targets");
                    for (var i = 1; i <= targets.Count; i++)
                    {
                        var target = targets[index: i - 1];
                        Console.WriteLine(value: $"{i}: {target?.Name} (Combat Level: {target?.CombatLevel} HP: {target?.ActorStats?.Health.CurrentValue}/{target?.ActorStats?.Health.MaxValue})");
                    }
                    var muliselectIndex = -1;
                    var doneSelecting = false;
                    var availableTargets = targets.Count;
                    while (muliselectIndex < 0 || muliselectIndex > targets.Count || selectedTargets.Count < availableTargets || !doneSelecting)
                    {
                        Console.Write(value: "Enter the number of the target: ");
                        var input = Console.ReadLine();
                        if (int.TryParse(s: input, result: out var index) && index >= 1 && index <= targets.Count)
                        {
                            muliselectIndex = index - 1;
                            if (!selectedTargets.Contains(item: targets[index: muliselectIndex]))
                            {
                                selectedTargets.Add(item: targets[index: muliselectIndex]);
                            }
                            else
                            {
                                Console.WriteLine(value: "Target already selected.");
                            }
                        }
                        else
                        {
                            Console.WriteLine(value: "Invalid choice. Please choose a valid target.");
                        }
                        Console.WriteLine(value: "Select another target? (Y/N)");
                        var selectAnother = Console.ReadLine()?.ToUpper();
                        if(selectAnother?.Equals("N", StringComparison.OrdinalIgnoreCase) == true || selectAnother?.Equals("Y", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            if (selectAnother?.Equals("N", StringComparison.OrdinalIgnoreCase) == false) continue;
                            doneSelecting = true;
                        }
                        else
                        {
                            Console.WriteLine(value: "Invalid choice. Please choose Y or N.");
                        }
                    }
                    break;
                case TargetingScope.All:
                    Console.WriteLine(value: "Selecting all available targets");
                    selectedTargets.AddRange(collection: targets);
                    break;
            }

            return selectedTargets;
        }

        ~Player()
        {
            // MessageSystem.MessageManager.UnregisterForChannel<ActorDeathMessage>(channel: MessageChannels.Combat,
            //     handler: ActorDeathMessageHandler);
        }
    }
}