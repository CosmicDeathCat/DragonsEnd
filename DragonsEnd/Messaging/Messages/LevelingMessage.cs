using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Messaging.Messages
{
    //TODO: Add more information and bne able to handle types of levels like skills and regular stats
    public struct LevelingMessage
    {
        public object Sender { get; }
        public IActor Actor { get; }
        public long Experience { get; }
        public int Level { get; }
        public LevelingType Type { get; }

        public LevelingMessage(object sender, IActor actor, long experience, int level, LevelingType type)
        {
            Sender = sender;
            Actor = actor;
            Experience = experience;
            Level = level;
            Type = type;
        }
    }
}