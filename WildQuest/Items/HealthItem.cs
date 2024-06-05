using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Messaging.Messages;

namespace WildQuest.Items;

public class HealthItem : Item, IHealthItem
{
    public virtual int HealPercentage { get; set; }
    
    public HealthItem(string name, string description, int price, ItemType type, int healPercentage, bool stackable = true, int quantity = 1) : base(name, description, price, type, stackable, quantity)
    {
        HealPercentage = healPercentage;
    }
    
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
                    source?.Inventory.Remove(this);
                }
                break;
        }
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Items, new ItemMessage(this, source, target));

    }
}