using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;

namespace DragonsEnd.Party.Enemy
{
    public class EnemyPartyManager : BasicPartyManager
    {
        public EnemyPartyManager()
        {
        }

        public EnemyPartyManager
            (List<IActor> members, IInventory? sharedInventory = null, bool useMaxMembers = true, int maxMembers = 4) : base(members: members, sharedInventory: sharedInventory, useMaxMembers: useMaxMembers,
            maxMembers: maxMembers)
        {
        }
    }
}