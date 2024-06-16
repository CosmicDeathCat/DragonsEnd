using System;
using System.Collections.Generic;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Actor.Player.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Drops;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Messaging.Messages;
using DragonsEnd.Skills;
using DragonsEnd.Skills.Combat;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Skills.NonCombat;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Leveling;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Actor.Player
{
    public class Player : Actor, IPlayer
    {
        public Player(
            string name,
            Gender gender,
            CharacterClassType characterClass,
            ActorStats actorStats,
            IActorSkills? actorSkills = null,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            long gold = 0,
            IEquipmentItem[]? equipment = null,
            List<IItem?>? inventory = null,
            params IDropItem[] dropItems)
        {
            Name = name;
            Gender = gender;
            CharacterClass = characterClass;
            ActorStats = actorStats;
            ActorSkills = actorSkills;
            IsAlive = true;
            Leveling = new Leveling(this);
            DamageMultiplier = new DoubleStat(damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(criticalHitMultiplier);
            Gold = new GoldCurrency(gold);
            
            if(actorSkills != null)
            {
                ActorSkills = actorSkills;
            }
            else
            {
                ActorSkills = new ActorSkills(this);
            }
            
            if (inventory != null)
            {
                Inventory.AddRange(inventory);
            }
            else
            {
                Inventory = new List<IItem?>();
            }

            if (equipment != null)
            {
                foreach (var item in equipment)
                {
                    item.Equip(this, this);
                }
            }
            else
            {
                Equipment = new IEquipmentItem[Enum.GetNames(typeof(EquipmentSlot)).Length];
            }

            if (dropItems.Length > 0)
            {
                DropItems = dropItems.ToList();
            }
            else
            {
                // Default to using the provided inventory and equipment as drop items with default drop rates
                foreach (var item in Inventory)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    DropItems.Add(new DropItem(item, item.DropRate));
                }

                if (equipment != null)
                {
                    foreach (var item in equipment)
                    {
                        DropItems.Add(new DropItem(item, item.DropRate));
                    }
                }
            }

            MessageSystem.MessageManager.RegisterForChannel<ActorDeathMessage>(MessageChannels.Combat,
                ActorDeathMessageHandler);
        }



        ~Player()
        {
            MessageSystem.MessageManager.UnregisterForChannel<ActorDeathMessage>(MessageChannels.Combat,
                ActorDeathMessageHandler);
        }
    }
}