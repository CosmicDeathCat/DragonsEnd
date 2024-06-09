using WildQuest.Interfaces;

namespace WildQuest.Messaging.Messages;

public struct ActorDeathMessage
{
    public IActor Target { get; }
    
    public ActorDeathMessage(IActor target)
    {
        Target = target;
    }
}