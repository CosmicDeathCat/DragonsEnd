using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Messaging.Messages;

namespace WildQuest.Items;

[System.Serializable]
public class Item : IItem
{
    public virtual string Name {get;set;}
    public virtual string Description {get;set;}
    public virtual long Price {get;set;}
    public virtual ItemType Type {get;set;}
    public virtual bool Stackable {get;set;}
    public virtual long Quantity {get;set;}
    
    public virtual double DropRate {get;set;}
    
    public Item(string name, string description, long price, ItemType type, bool stackable = true, long quantity = 1, double dropRate = 1)
    {
        Name = name;
        Description = description;
        Price = price;
        Type = type;
        Stackable = stackable;
        Quantity = quantity;
        DropRate = dropRate;
    }

    public virtual void Use(IActor? source, IActor? target)
    {
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Items, new ItemMessage(this, source, target));
    }
    
    public virtual IItem Copy()
    {
        return new Item(Name, Description, Price, Type, Stackable, Quantity);
    }

}