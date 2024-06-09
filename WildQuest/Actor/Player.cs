using System;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using WildQuest.Enums;
using WildQuest.Interfaces;
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
        params IItem[] inventory)
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
        Inventory.AddRange(inventory);
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
        Console.WriteLine($"{Name} has looted {loot.Gold} gold and {loot.Items.Count} items from {data.Target.Name}.");
        foreach (var item in loot.Items)
        {
            Console.WriteLine($"Looted {item.Quantity} {item.Name}.");
            Inventory.Add(item);
        }
        
    }
}