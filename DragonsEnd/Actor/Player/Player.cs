using System;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Messages;
using DragonsEnd.Actor.Player.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
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
            ActorStats actorStats,
            IActorSkills? actorSkills = null,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            IEquipmentItem[]? equipment = null,
            IInventory? inventory = null,
            params IItem[] dropItems
        )
        {
            Name = name;
            Gender = gender;
            CharacterClass = characterClass;
            ActorStats = actorStats;
            ActorSkills = actorSkills;
            IsAlive = true;
            DamageMultiplier = new DoubleStat(baseValue: damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(baseValue: criticalHitMultiplier);
            Inventory = inventory;
            
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

            if (dropItems.Length > 0)
            {
                LootContainer.Items = dropItems.ToList();
            }
            else
            {
                // Default to using the provided inventory and equipment as drop items with default drop rates
                foreach (var item in Inventory?.Items)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    LootContainer.Items.Add(item: new Item(item: item));
                }

                if (equipment != null)
                {
                    foreach (var item in equipment)
                    {
                        LootContainer.Items.Add(item: new Item(item: item));
                    }
                }
            }

            MessageSystem.MessageManager.RegisterForChannel<ActorDeathMessage>(channel: MessageChannels.Combat,
                handler: ActorDeathMessageHandler);
        }


        ~Player()
        {
            MessageSystem.MessageManager.UnregisterForChannel<ActorDeathMessage>(channel: MessageChannels.Combat,
                handler: ActorDeathMessageHandler);
        }
    }
}