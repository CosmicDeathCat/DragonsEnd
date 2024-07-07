using System;
using System.Collections.Generic;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Messages;

namespace DragonsEnd.Items
{
    [Serializable]
    public class Item : IItem
    {
        public virtual string Name { get; set; }
        public virtual Guid ID { get; set; } = Guid.NewGuid();
        public virtual string Description { get; set; }
        public virtual long Price { get; set; }
        public virtual ItemType Type { get; set; }
        public virtual bool Stackable { get; set; }
        public virtual long Quantity { get; set; }
        public virtual double DropRate { get; set; }
        public TargetingType TargetingType { get; set; }
        public TargetingScope TargetingScope { get; set; }
        public ActorScopeType ActorScopeType { get; set; }

        public Item
        (
            string name,
            string description,
            long price,
            ItemType type,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1,
            TargetingType targetingType = TargetingType.None,
            TargetingScope targetingScope = TargetingScope.None,
            ActorScopeType actorScopeType = ActorScopeType.None
        )
        {
            Name = name;
            Description = description;
            Price = price;
            Type = type;
            Stackable = stackable;
            Quantity = quantity;
            DropRate = dropRate;
            TargetingType = targetingType;
            TargetingScope = targetingScope;
            ActorScopeType = actorScopeType;
        }

        public Item
        (
            IItem item
        )
        {
            Name = item.Name;
            ID = item.ID;
            Description = item.Description;
            Price = item.Price;
            Type = item.Type;
            Stackable = item.Stackable;
            Quantity = item.Quantity;
            DropRate = item.DropRate;
            TargetingType = item.TargetingType;
            TargetingScope = item.TargetingScope;
            ActorScopeType = item.ActorScopeType;
        }


        public virtual void Use(IActor? source, List<IActor?>? targets = null)
        {
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Items,
                message: new ItemMessage(item: this, source: source, targets: targets));
        }

        public virtual IItem Copy()
        {
            return new Item(name: Name, description: Description, price: Price, type: Type, stackable: Stackable, quantity: Quantity, dropRate: DropRate, targetingType: TargetingType, targetingScope: TargetingScope, actorScopeType: ActorScopeType);
        }
    }
}