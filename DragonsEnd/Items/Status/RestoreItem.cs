using System;
using System.Collections.Generic;
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
        public virtual double HealthHealthRestorePercentage { get; set; }
        public virtual double ManaRestorePercentage { get; set; }
        public virtual double StaminaRestorePercentage { get; set; }

        public RestoreItem
        (
            string name,
            string description,
            long price,
            ItemType type,
            double healthRestorePercentage = 0,
            double manaRestorePercentage = 0,
            double staminaRestorePercentage = 0,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1,
            TargetingType targetingType = TargetingType.None,
            TargetingScope targetingScope = TargetingScope.None,
            ActorScopeType actorScopeType = ActorScopeType.None
        ) : base(name: name, description: description, price: price, type: type,
            stackable: stackable,
            quantity: quantity, dropRate: dropRate, targetingType: targetingType, targetingScope: targetingScope, actorScopeType: actorScopeType)
        {
            HealthHealthRestorePercentage = healthRestorePercentage;
            ManaRestorePercentage = manaRestorePercentage;
            StaminaRestorePercentage = staminaRestorePercentage;
        }

        public override void Use(IActor? source, List<IActor?>? targets = null)
        {
            if (targets != null)
            {
                foreach (IActor? target in targets)
                {
                    target.ActorStats.Health.CurrentValue += (int)Math.Round(value: target.ActorStats.Health.MaxValue * HealthHealthRestorePercentage / 100, mode: MidpointRounding.AwayFromZero);
                    target.ActorStats.Mana.CurrentValue += (int)Math.Round(value: target.ActorStats.Mana.MaxValue * ManaRestorePercentage / 100, mode: MidpointRounding.AwayFromZero);
                    target.ActorStats.Stamina.CurrentValue += (int)Math.Round(value: target.ActorStats.Stamina.MaxValue * StaminaRestorePercentage / 100, mode: MidpointRounding.AwayFromZero);                }
            }

            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Items,
                message: new ItemMessage(item: this, source: source, targets: targets));
        }

        public override IItem Copy()
        {
            return new RestoreItem(name: Name, description: Description, price: Price, type: Type, healthRestorePercentage: HealthHealthRestorePercentage,
                stackable: Stackable, quantity: Quantity, dropRate: DropRate, targetingType: TargetingType, targetingScope: TargetingScope, actorScopeType: ActorScopeType);
        }
    }
}