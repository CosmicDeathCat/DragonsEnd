using System.Collections.Concurrent;
using System.Collections.Generic;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Identity.Interfaces;
using DragonsEnd.Leveling.Interfaces;
using DragonsEnd.Lockable.Interfaces;

namespace DragonsEnd.Skills.Interfaces
{
    public interface ISkill : IIdentity
    {
        ILeveling Leveling { get; set; }
        IActor Actor { get; set; }
        ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; }
        void HandleUnlocks(int level);
        void LevelingMessageHandler(IMessageEnvelope message);
    }
}