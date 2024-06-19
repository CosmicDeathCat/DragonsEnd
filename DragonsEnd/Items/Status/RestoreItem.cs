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
    public class RestoreItem : Item, IRestoreItem
    {
        public RestoreItem
        (
            string name,
            string description,
            long price,
            ItemType type,
            int healthRestorePercentage = 0,
            int manaRestorePercentage = 0,
            int staminaRestorePercentage = 0,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        ) : base(name: name, description: description, price: price, type: type,
            stackable: stackable,
            quantity: quantity, dropRate: dropRate)
        {
            HealthHealthRestorePercentage = healthRestorePercentage;
            ManaRestorePercentage = manaRestorePercentage;
            StaminaRestorePercentage = staminaRestorePercentage;
        }

        public virtual int HealthHealthRestorePercentage { get; set; }
        public virtual int ManaRestorePercentage { get; set; }
        public virtual int StaminaRestorePercentage { get; set; }

        public override void Use(IActor? source, IActor? target)
        {
            if (target != null)
            {
                target.ActorStats.Health.CurrentValue += target.ActorStats.Health.MaxValue * HealthHealthRestorePercentage / 100;
                target.ActorStats.Mana.CurrentValue += target.ActorStats.Mana.MaxValue * ManaRestorePercentage / 100;
                target.ActorStats.Stamina.CurrentValue += target.ActorStats.Stamina.MaxValue * StaminaRestorePercentage / 100;
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
            return new RestoreItem(name: Name, description: Description, price: Price, type: Type, healthRestorePercentage: HealthHealthRestorePercentage,
                stackable: Stackable, quantity: Quantity);
        }
    }
}