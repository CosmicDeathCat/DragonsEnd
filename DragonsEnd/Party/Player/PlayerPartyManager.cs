using System;
using System.Collections.Generic;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Messages;
using DragonsEnd.Enums;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Loot;
using DragonsEnd.Items.Loot.Messages;

namespace DragonsEnd.Party.Player
{
    public class PlayerPartyManager : BasicPartyManager
    {
        public PlayerPartyManager()
        {
            MessageSystem.MessageManager.RegisterForChannel<LootMessage>(channel: MessageChannels.Combat, handler: LootMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<VictoryMessage>(channel: MessageChannels.Combat, handler: VictoryMessageHandler);
        }

        public PlayerPartyManager
            (List<IActor> members, IInventory? sharedInventory, bool useMaxMembers = true, int maxMembers = 4) : base(members: members, sharedInventory: sharedInventory, useMaxMembers: useMaxMembers,
            maxMembers: maxMembers)
        {
            MessageSystem.MessageManager.RegisterForChannel<LootMessage>(channel: MessageChannels.Combat, handler: LootMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<VictoryMessage>(channel: MessageChannels.Combat, handler: VictoryMessageHandler);
        }

        public virtual void VictoryMessageHandler(IMessageEnvelope message)
        {
            if (!message.Message<VictoryMessage>().HasValue)
            {
                return;
            }

            var data = message.Message<VictoryMessage>().GetValueOrDefault();
            var individualLoot = data.Enemies.Select(selector: x => x.Loot(x.LootConfig)).ToList();
            if (individualLoot.Count <= 0)
            {
                return;
            }

            var mergedLoot = LootContainer.MergeLootContainers(lootContainers: individualLoot);
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat,
                message: new LootMessage(sourceActors: data.Enemies, targetActors: data.Players, individualLoot: individualLoot, mergedLoot: mergedLoot));
        }

        public virtual void LootMessageHandler(IMessageEnvelope message)
        {
            if (!message.Message<LootMessage>().HasValue)
            {
                return;
            }

            var data = message.Message<LootMessage>().GetValueOrDefault();
            foreach (var targetActor in data.TargetActors)
            {
                if (!Members.Contains(item: targetActor))
                {
                    continue;
                }

                if (data.MergedLoot != null)
                {
                    if (targetActor.IsAlive)
                    {
                        switch (targetActor.CombatStyle)
                        {
                            case CombatStyle.Melee:
                                targetActor.ActorSkills?.MeleeSkill.Leveling.GainExperience(amount: data.MergedLoot.CombatExperience);
                                break;
                            case CombatStyle.Ranged:
                                targetActor.ActorSkills?.RangedSkill.Leveling.GainExperience(amount: data.MergedLoot.CombatExperience);
                                break;
                            case CombatStyle.Magic:
                                targetActor.ActorSkills?.MagicSkill.Leveling.GainExperience(amount: data.MergedLoot.CombatExperience);
                                break;
                            case CombatStyle.Hybrid:
                                var sharedExp = data.MergedLoot.CombatExperience / 3;
                                targetActor.ActorSkills?.MeleeSkill.Leveling.GainExperience(amount: sharedExp);
                                targetActor.ActorSkills?.RangedSkill.Leveling.GainExperience(amount: sharedExp);
                                targetActor.ActorSkills?.MagicSkill.Leveling.GainExperience(amount: sharedExp);
                                break;
                        }

                        foreach (var skill in data.MergedLoot.SkillExperiences)
                        {
                            switch (skill.SkillType)
                            {
                                case SkillType.None:
                                    break;
                                case SkillType.Melee:
                                    targetActor.ActorSkills?.MeleeSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Ranged:
                                    targetActor.ActorSkills?.RangedSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Magic:
                                    targetActor.ActorSkills?.MagicSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Alchemy:
                                    targetActor.ActorSkills?.AlchemySkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Cooking:
                                    targetActor.ActorSkills?.CookingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Crafting:
                                    targetActor.ActorSkills?.CraftingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Enchanting:
                                    targetActor.ActorSkills?.EnchantingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Fishing:
                                    targetActor.ActorSkills?.FishingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Fletching:
                                    targetActor.ActorSkills?.FletchingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Foraging:
                                    targetActor.ActorSkills?.ForagingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Mining:
                                    targetActor.ActorSkills?.MiningSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Smithing:
                                    targetActor.ActorSkills?.SmithingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Ranching:
                                    targetActor.ActorSkills?.RanchingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                                case SkillType.Woodcutting:
                                    targetActor.ActorSkills?.WoodcuttingSkill.Leveling.GainExperience(amount: skill.Experience);
                                    break;
                            }
                        }
                    }
                }
            }

            if (data.MergedLoot != null)
            {
                SharedInventory?.Gold.Add(otherCurrency: data.MergedLoot.Gold);
                SharedInventory?.Items.AddRange(collection: data.MergedLoot.Items);
                var totalItemCount = data.MergedLoot.Items.Sum(selector: x => x.Quantity);
                var itemsString = totalItemCount is > 0 and > 1 ? "Items" : "Item";
                var lootItemsDisplay = data.MergedLoot.Items.Count > 0
                    ? $"{totalItemCount} {itemsString} from the battle\n" + string.Join(separator: ", \n", values: data.MergedLoot.Items.Select(selector: x => $"Looted {x.Quantity} {x.Name}"))
                    : "No Items";
                Console.WriteLine(value: $"The party has looted {data.MergedLoot.Gold} and {lootItemsDisplay}");
            }
        }

        ~PlayerPartyManager()
        {
            MessageSystem.MessageManager.UnregisterForChannel<LootMessage>(channel: MessageChannels.Combat, handler: LootMessageHandler);
            MessageSystem.MessageManager.UnregisterForChannel<VictoryMessage>(channel: MessageChannels.Combat, handler: VictoryMessageHandler);
        }
    }
}