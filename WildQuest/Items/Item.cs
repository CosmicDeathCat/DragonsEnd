using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Messaging.Messages;

namespace WildQuest.Items;

public class Item : IItem
{
    public virtual string Name {get;set;}
    public virtual string Description {get;set;}
    public virtual int Price {get;set;}
    public virtual ItemType Type {get;set;}
    public virtual bool Stackable {get;set;}
    public virtual int Quantity {get;set;}
    
    public Item(string name, string description, int price, ItemType type, bool stackable = true, int quantity = 1)
    {
        Name = name;
        Description = description;
        Price = price;
        Type = type;
        Stackable = stackable;
        Quantity = quantity;
    }

    public virtual void Use(IActor? source, IActor? target)
    {
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Items, new ItemMessage(this, source, target));
    }

}