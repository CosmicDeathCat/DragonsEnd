using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Identity.Interfaces;

namespace DragonsEnd.Leveling.Messages
{
    //TODO: Add more information and bne able to handle types of levels like skills and regular stats
    public struct LevelingMessage
    {
        public IIdentity SenderIdentity { get; }
        public IActor Actor { get; }
        public long Experience { get; }
        public int Level { get; }
        public LevelingType Type { get; }

        public LevelingMessage(IIdentity senderIdentity, IActor actor, long experience, int level, LevelingType type)
        {
            SenderIdentity = senderIdentity;
            Actor = actor;
            Experience = experience;
            Level = level;
            Type = type;
        }
    }
}