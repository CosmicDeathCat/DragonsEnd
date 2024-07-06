using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Actor.Player.Interfaces;
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Lists;
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
            ActorSkills = actorSkills;
            ActorSkills?.UpdateActor(actor: this);
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
                var target = ChooseTarget(targets: targets);
                if (target == null)
                {
                    return false;
                }

                var attackResult = Attack(source: this, target: target);
                return true;
            }

            return false;
        }

        public virtual IActor? ChooseTarget(List<IActor> targets)
        {
            Console.WriteLine(value: "Choose a target to attack:");
            for (var i = 1; i <= targets.Count; i++)
            {
                var target = targets[index: i - 1];
                if (target.IsAlive)
                {
                    Console.WriteLine(value: $"{i}: {target.Name} (Combat Level: {target?.CombatLevel} HP: {target?.ActorStats?.Health.CurrentValue})");
                }
            }

            var chosenIndex = -1;
            while (chosenIndex < 0 || chosenIndex > targets.Count || !targets[index: chosenIndex].IsAlive)
            {
                Console.Write(value: "Enter the number of the target: ");
                var input = Console.ReadLine();
                if (int.TryParse(s: input, result: out var index) && index >= 1 && index <= targets.Count && targets[index: index - 1].IsAlive)
                {
                    chosenIndex = index - 1;
                }
                else
                {
                    Console.WriteLine(value: "Invalid choice. Please choose a valid target.");
                }
            }

            return targets[index: chosenIndex];
        }

        // private void VictoryMessageHandler(IMessageEnvelope message)
        // {
        //     if(!message.Message<VictoryMessage>().HasValue)
        //     {
        //         return;
        //     }
        //
        //     var data = message.Message<VictoryMessage>().GetValueOrDefault();
        //     var player = data.Players.FirstOrDefault(x=> x.ID.Equals(ID));
        //     if(player == null)
        //     {
        //         return;
        //     }
        //
        //     if (player.IsAlive)
        //     {
        //         foreach (var enemy in data.Enemies)
        //         {
        //             var loot = enemy.Loot();
        //             if (loot == null)
        //             {
        //                 continue;
        //             }
        //             if(ActorSkills == null)
        //             {
        //                 continue;
        //             }
        //             switch (CombatStyle)
        //             {
        //                 case CombatStyle.Melee:
        //                     ActorSkills.MeleeSkill.Leveling.GainExperience(loot.CombatExperience);
        //                     break;
        //                 case CombatStyle.Ranged:
        //                     ActorSkills.RangedSkill.Leveling.GainExperience(loot.CombatExperience);
        //                     break;
        //                 case CombatStyle.Magic:
        //                     ActorSkills.MagicSkill.Leveling.GainExperience(loot.CombatExperience);
        //                     break;
        //                 case CombatStyle.Hybrid:
        //                     var sharedExp = loot.CombatExperience / 3;
        //                     ActorSkills.MeleeSkill.Leveling.GainExperience(sharedExp);
        //                     ActorSkills.RangedSkill.Leveling.GainExperience(sharedExp);
        //                     ActorSkills.MagicSkill.Leveling.GainExperience(sharedExp);
        //                     break;
        //             }
        //             foreach (var skill in loot.SkillExperiences)
        //             {
        //                 switch (skill.SkillType)
        //                 {
        //                     case SkillType.None:
        //                         break;
        //                     case SkillType.Melee:
        //                         ActorSkills.MeleeSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Ranged:
        //                         ActorSkills.RangedSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Magic:
        //                         ActorSkills.MagicSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Alchemy:
        //                         ActorSkills.AlchemySkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Cooking:
        //                         ActorSkills.CookingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Crafting:
        //                         ActorSkills.CraftingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Enchanting:
        //                         ActorSkills.EnchantingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Fishing:
        //                         ActorSkills.FishingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Fletching:
        //                         ActorSkills.FletchingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Foraging:
        //                         ActorSkills.ForagingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Mining:
        //                         ActorSkills.MiningSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Smithing:
        //                         ActorSkills.SmithingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Ranching:
        //                         ActorSkills.RanchingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                     case SkillType.Woodcutting:
        //                         ActorSkills.WoodcuttingSkill.Leveling.GainExperience(skill.Experience);
        //                         break;
        //                 }
        //             }
        //             
        //             if(!enemy.HasAlreadyBeenLooted)
        //             {
        //                 MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat, message: new LootMessage(source: enemy, target: this, loot: loot ));
        //             }
        //             // Inventory?.Gold.Add(otherCurrency: loot.Gold);
        //             // Inventory?.Items.AddRange(collection: loot.Items);
        //             // var itemsString = loot.Items.Count is > 0 and > 1 ? "Items" : "Item";
        //             // var lootItemsDisplay = loot.Items.Count > 0
        //             //     ? $"{loot.Items.Count} {itemsString} from {enemy.Name}\n" +
        //             //       string.Join(separator: ", \n", values: loot.Items.Select(selector: x => "Looted " + x.Name))
        //             //     : "No Items";
        //             // Console.WriteLine(value: $"{Name} has looted {loot.Gold} and {lootItemsDisplay}");
        //         }
        //         
        //
        //     }
        // }


        ~Player()
        {
            // MessageSystem.MessageManager.UnregisterForChannel<ActorDeathMessage>(channel: MessageChannels.Combat,
            //     handler: ActorDeathMessageHandler);
        }
    }
}