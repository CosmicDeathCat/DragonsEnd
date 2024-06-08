using WildQuest.Enums;
using WildQuest.Interfaces;

namespace WildQuest.Messaging.Messages;

public struct LevelingMessage
{
    public IActor Actor { get; }
    public long Experience { get; }
    public int Level { get; }
    public LevelingType Type { get; }
    
    public LevelingMessage(IActor actor, long experience, int level, LevelingType type)
    {
        Actor = actor;
        Experience = experience;
        Level = level;
        Type = type;
    }
    
}