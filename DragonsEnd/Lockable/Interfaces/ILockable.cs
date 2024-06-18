using DragonsEnd.Identity.Interfaces;

namespace DragonsEnd.Lockable.Interfaces
{
    public interface ILockable : IIdentity
    {
        string Description { get; set; }
        bool IsLocked { get; set; }
        bool Lock();
        bool Unlock();
    }
}