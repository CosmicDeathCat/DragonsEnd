using System;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Messages;
using DragonsEnd.Items.Status.Interfaces;

namespace DragonsEnd.Items.Status
{
    [Serializable]
    public class HealthItem : Item, IHealthItem
    {
        public HealthItem(string name, string description, long price, ItemType type, int healPercentage,
            bool stackable = true, long quantity = 1, double dropRate = 1) : base(name: name, description: description, price: price, type: type,
            stackable: stackable,
            quantity: quantity, dropRate: dropRate)
        {
            HealPercentage = healPercentage;
        }

        public virtual int HealPercentage { get; set; }

        public override void Use(IActor? source, IActor? target)
        {
            if (target != null)
            {
                target.ActorStats.Health.CurrentValue += target.ActorStats.Health.MaxValue * HealPercentage / 100;
            }

            switch (Type)
            {
                case ItemType.NonConsumable:
                    break;
                case ItemType.Consumable:
                    Quantity--;
                    if (Quantity <= 0)
                    {
                        source?.Inventory.Remove(item: this);
                    }

                    break;
            }

            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Items,
                message: new ItemMessage(item: this, source: source, target: target));
        }

        public override IItem Copy()
        {
            return new HealthItem(name: Name, description: Description, price: Price, type: Type, healPercentage: HealPercentage,
                stackable: Stackable, quantity: Quantity);
        }
    }
}