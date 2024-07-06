using System;
using DragonsEnd.Lockable.Interfaces;

namespace DragonsEnd.Skills.Unlocks
{
    public class BasicSkillUnlock : ILockable
    {
        public virtual string Name { get; set; }
        public virtual Guid ID { get; set; } = Guid.NewGuid();
        public virtual string Description { get; set; }
        public virtual bool IsLocked { get; set; } = true;

        public BasicSkillUnlock(string name, string description, bool isLocked = true)
        {
            Name = name;
            Description = description;
        }

        public virtual bool Lock()
        {
            if (IsLocked)
            {
                return false;
            }

            IsLocked = true;
            return true;
        }

        public virtual bool Unlock()
        {
            if (!IsLocked)
            {
                return false;
            }

            IsLocked = false;
            return true;
        }
    }
}