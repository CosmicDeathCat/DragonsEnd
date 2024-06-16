using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Messaging.Messages
{
    public struct ActorDeathMessage
    {
        public IActor? Source { get; }
        public IActor Target { get; }

        public ActorDeathMessage(IActor? source, IActor target)
        {
            Source = source;
            Target = target;
        }
    }
}