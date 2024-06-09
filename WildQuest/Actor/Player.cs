using System;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Items;
using WildQuest.Items.Currency;
using WildQuest.Items.Currency.Extensions;
using WildQuest.Messaging.Messages;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class Player : CombatActor, IPlayer
{
    public Player(
        string name,
        Gender gender,
        CharacterClassType characterClass,
        ActorStats actorStats,
        double damageMultiplier = 1.00,
        double damageReductionMultiplier = 1.00,
        long gold = 0,
        IEquipmentItem[]? equipment = null,
        IItem[]? inventory = null,
        params IDropItem[] dropItems)
        : base()
    {
        Name = name;
        Gender = gender;
        CharacterClass = characterClass;
        ActorStats = actorStats;
        IsAlive = true;
        Leveling = new Leveling(this);
        DamageMultiplier = damageMultiplier;
        DamageReductionMultiplier = damageReductionMultiplier;
        Gold = new GoldCurrency(gold);
        if (inventory != null)
        {
            Inventory.AddRange(inventory);
        }
        else
        {
            Inventory = [];
        }
        if (equipment != null)
        {
            foreach (var item in equipment)
            {
                item.Equip(this,this);
            }
        }
        else
        {
            Equipment = new IEquipmentItem[Enum.GetNames<EquipmentSlot>().Length];
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
                if(item == null) continue;
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
        MessageSystem.MessageManager.RegisterForChannel<ActorDeathMessage>(MessageChannels.Combat, ActorDeathMessageHandler);
    }
    
    ~Player()
    {
        MessageSystem.MessageManager.UnregisterForChannel<ActorDeathMessage>(MessageChannels.Combat, ActorDeathMessageHandler);
    }

    public virtual void ActorDeathMessageHandler(IMessageEnvelope message)
    {
        if(!message.Message<ActorDeathMessage>().HasValue) return;
        var data = message.Message<ActorDeathMessage>().GetValueOrDefault();
        if(data.Source != this) return;
        if(data.Target == this) return;
        var loot = data.Target.Loot();
        Leveling.GainExperience(loot.Experience);
        Gold.Add(loot.Gold);
        Inventory.AddRange(loot.Items);
        var lootItemsDisplay = loot.Items.Count > 0 ? $"{loot.Items.Count} Items from {data.Target.Name}\n" + string.Join(", \n", loot.Items.Select(x=> "Looted " + x.Name)) : "No Items";
        Console.WriteLine($"{Name} has looted {loot.Gold} and {lootItemsDisplay}");
    }
}