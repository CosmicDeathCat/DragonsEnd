using System;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Inventory;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Party.Interfaces;

namespace DragonsEnd.Party
{
    public class BasicPartyManager : IPartyManager
    {
        protected List<IActor> _members;

        public BasicPartyManager()
        {
            Members = new List<IActor>();
            SharedInventory = new Inventory();
        }

        public BasicPartyManager(List<IActor> members, IInventory? sharedInventory, bool useMaxMembers = true, int maxMembers = 4)
        {
            Members = members;
            SharedInventory = sharedInventory;
            UseMaxMembers = useMaxMembers;
            MaxMembers = maxMembers;
        }

        public virtual List<IActor> Members
        {
            get => _members;
            set
            {
                _members = value;
                SyncInventories();
            }
        }

        public virtual IInventory? SharedInventory { get; set; } = new Inventory();
        public virtual bool UseMaxMembers { get; set; } = true;
        public virtual int MaxMembers { get; set; } = 4;
        public virtual bool IsFull => UseMaxMembers && Members.Count >= MaxMembers;

        public virtual bool AddMember(IActor actor)
        {
            var success = true;
            if (IsFull)
            {
                Console.WriteLine(value: "Party is full and cannot add more members.");
                success = false;
                return success;
            }

            Members.Add(item: actor);
            Console.WriteLine(value: $"{actor.Name} has joined the party.");
            success = true;
            SyncInventories();
            return success;
        }

        public virtual bool RemoveMember(IActor actor)
        {
            var success = true;
            if (Members.Contains(item: actor))
            {
                Members.Remove(item: actor);
                Console.WriteLine(value: $"{actor.Name} has left the party.");
                success = true;
            }
            else
            {
                Console.WriteLine(value: $"{actor.Name} is not a member of the party.");
                success = false;
            }

            SyncInventories();
            return success;
        }

        public virtual bool AddMembers(params IActor[] actors)
        {
            var success = true;
            if (IsFull)
            {
                Console.WriteLine(value: "Party is full and cannot add more members.");
                success = false;
            }

            foreach (var actor in actors)
            {
                if (AddMember(actor: actor))
                {
                    continue;
                }

                success = false;
            }

            return success;
        }

        public virtual bool RemoveMembers(params IActor[] actors)
        {
            var success = true;
            foreach (var actor in actors)
            {
                if (RemoveMember(actor: actor))
                {
                    continue;
                }

                success = false;
            }

            return success;
        }

        public virtual void SyncInventories()
        {
            foreach (var member in Members)
            {
                member.Inventory = SharedInventory;
            }
        }
    }
}