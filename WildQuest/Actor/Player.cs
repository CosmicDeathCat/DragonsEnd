using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using WildQuest.Enums;
using WildQuest.Interfaces;
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
        params IItem[] inventory)
        : base()
    {
        Name = name;
        Gender = gender;
        CharacterClass = characterClass;
        ActorStats = actorStats;
        IsAlive = true;
        Leveling = new Leveling(this);
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
        var lootable = data.Target as ILootable;
        if(lootable == null) return;
        var loot = lootable.Loot(data.Source);
        Leveling.Experience += loot.Experience;
        Gold.Quantity += loot.Gold.Quantity;
        Inventory.AddRange(loot.Items);
    }
}