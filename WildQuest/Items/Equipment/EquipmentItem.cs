using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Items.Equipment;

[System.Serializable]
public class EquipmentItem : Item, IEquipmentItem
{
    public virtual List<EquipmentSlot> Slots { get; set; }
    public virtual ActorStats Stats { get; set; }
    
    public virtual GearTier GearTier { get; set; }
    
    public EquipmentItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, GearTier gearTier, bool stackable = true, long quantity = 1, double dropRate = 1) 
        : base(name, description, price, type, stackable, quantity, dropRate)
    {
        Slots = slots;
        Stats = stats;
        GearTier = gearTier;
    }
    
    public virtual void Equip(IActor? source, IActor? target)
    {
        foreach (var slot in Slots)
        {
            if (target?.Equipment[(int)slot] != this)
            {
                target?.Equipment[(int)slot]?.Unequip(source, target);
            }

            if (target != null)
            {
                target.Equipment[(int)slot] = this;
                source?.Inventory.Remove(this);
            }
        }

        if (target != null)
        {
            target.ActorStats += Stats;
        }
    }

    public virtual void Unequip(IActor? source, IActor? target)
    {
        foreach (var slot in Slots)
        {
            if (target != null)
            {
                target.Equipment[(int)slot] = null;
            }
        }

        if (target != null)
        {
            target.ActorStats -= Stats;
            source?.Inventory.Add(this);
        }
    }

    public override IItem Copy()
    {
        return new EquipmentItem(Name, Description, Price, Type, Slots, Stats, GearTier, Stackable, Quantity);
    }
}