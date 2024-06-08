using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;

namespace WildQuest.Interfaces;

public interface IPlayer : ICombatant
{
    void ActorDeathMessageHandler(IMessageEnvelope message);
}