using System;
using System.Collections.Generic;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Messages;
using DragonsEnd.Actor.Player.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Drops;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
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
            long gold = 0,
            IEquipmentItem[]? equipment = null,
            List<IItem?>? inventory = null,
            params IDropItem[] dropItems
        )
        {
            Name = name;
            Gender = gender;
            CharacterClass = characterClass;
            ActorStats = actorStats;
            ActorSkills = actorSkills;
            IsAlive = true;
            Leveling = new Leveling.Leveling(actor: this, name: "Level");
            DamageMultiplier = new DoubleStat(baseValue: damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(baseValue: criticalHitMultiplier);
            Gold = new GoldCurrency(quantity: gold);

            if (actorSkills != null)
            {
                ActorSkills = actorSkills;
            }
            else
            {
                ActorSkills = new ActorSkills(actor: this);
            }

            if (inventory != null)
            {
                Inventory.AddRange(collection: inventory);
            }
            else
            {
                Inventory = new List<IItem?>();
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

                    DropItems.Add(item: new DropItem(item: item, dropRate: item.DropRate));
                }

                if (equipment != null)
                {
                    foreach (var item in equipment)
                    {
                        DropItems.Add(item: new DropItem(item: item, dropRate: item.DropRate));
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