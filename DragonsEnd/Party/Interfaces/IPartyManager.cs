using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;

namespace DragonsEnd.Party.Interfaces
{
    public interface IPartyManager
    {
        List<IActor> Members { get; set; }
        IInventory? SharedInventory { get; set; }
        bool UseMaxMembers { get; set; }
        int MaxMembers { get; set; }
        bool IsFull { get; }
        bool AddMember(IActor actor);
        bool RemoveMember(IActor actor);
        bool AddMembers(params IActor[] actors);
        bool RemoveMembers(params IActor[] actors);
        void SyncInventories();
    }
}