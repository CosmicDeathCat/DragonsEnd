using System;
using System.Collections.Generic;
using System.Linq;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Items.Equipment;

[Serializable]
public class EquipmentItem : Item, IEquipmentItem
{
    public virtual List<EquipmentSlot> Slots { get; set; }
    public virtual ActorStats Stats { get; set; }

    public virtual GearTier GearTier { get; set; }

    public virtual CharacterClassType AllowedClasses { get; set; }

    public virtual int RequiredLevel { get; set; }

    public EquipmentItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, GearTier gearTier,
        CharacterClassType allowedClasses, int requiredLevel, bool stackable = true, long quantity = 1, double dropRate = 1)
        : base(name, description, price, type, stackable, quantity, dropRate)
    {
        Slots = slots;
        Stats = stats;
        GearTier = gearTier;
        AllowedClasses = allowedClasses;
        RequiredLevel = requiredLevel;
    }
    
    public virtual bool Equip(IActor? source, IActor? target)
{
    if (target == null) return false;
    
    if(!AllowedClasses.HasFlag(target.CharacterClass))
    {
        Console.WriteLine($"{target.Name} cannot equip {Name}. {Name} is only for {AllowedClasses}.");
        return false;
    }
    
    
    if (target.Leveling.CurrentLevel < RequiredLevel)
    {
        Console.WriteLine($"{target.Name} is not high enough level to equip {Name}. must be at least level {RequiredLevel}.");
        return false;
    }
    


    bool isEquipped = false;
    int mainHandIndex = Array.IndexOf(Enum.GetNames(typeof(EquipmentSlot)), EquipmentSlot.MainHand.ToString());
    int offHandIndex = Array.IndexOf(Enum.GetNames(typeof(EquipmentSlot)), EquipmentSlot.OffHand.ToString());

    // First, handle items intended for both main and off hand.
    if (Slots.Contains(EquipmentSlot.MainHand) && Slots.Contains(EquipmentSlot.OffHand))
    {
        if (target.Equipment[mainHandIndex] == null && target.Equipment[offHandIndex] == null)
        {
            target.Equipment[mainHandIndex] = this;
        }
        else if (target.Equipment[mainHandIndex] != null && target.Equipment[offHandIndex] == null)
        {
            target.Equipment[offHandIndex] = this;
        }
        else if (target.Equipment[mainHandIndex] != null && target.Equipment[offHandIndex] != null)
        {
            if (target.Equipment[mainHandIndex] != this)
            {
                target.Equipment[mainHandIndex]?.Unequip(source, target, EquipmentSlot.MainHand);
                target.Equipment[mainHandIndex] = this;
            }
            else
            {
                target.Equipment[offHandIndex]?.Unequip(source, target, EquipmentSlot.OffHand);
                target.Equipment[offHandIndex] = this;

            }
        }

        isEquipped = true;
    }
    else if (Slots.Contains(EquipmentSlot.TwoHanded))
    {
        if (target.Equipment[mainHandIndex] != null && target.Equipment[mainHandIndex] != this)
        {
            target.Equipment[mainHandIndex]?.Unequip(source, target, EquipmentSlot.MainHand);
        }

        if (target.Equipment[offHandIndex] != null && target.Equipment[offHandIndex] != this)
        {
            target.Equipment[mainHandIndex]?.Unequip(source, target, EquipmentSlot.OffHand);

        }
        // Unequip(source, target, EquipmentSlot.TwoHanded);

        if (target.Equipment[mainHandIndex] == null && target.Equipment[offHandIndex] == null)
        {
            target.Equipment[mainHandIndex] = this;
            target.Equipment[offHandIndex] = this;
            isEquipped = true;
        }

    }
    else 
    {
        // Then handle each slot individually
        foreach (var slot in Slots)
        {
            int slotIndex = Array.IndexOf(Enum.GetValues(typeof(EquipmentSlot)), slot);
            
            if (target.Equipment[slotIndex] == null || target.Equipment[slotIndex] != this)
            {
                // Equip in specific slot if it's empty
                if (target.Equipment[slotIndex] != null)
                {
                    target.Equipment[slotIndex]?.Unequip(source, target, slot); // Unequip current item in the slot if any
                }
                target.Equipment[slotIndex] = this;
                isEquipped = true;
                break;
            }
        }
    }

    if (isEquipped)
    {
        source?.Inventory.Remove(this);
        ApplyModifiers(target, true);
    }
    else
    {
        Console.WriteLine($"{target.Name} could not equip {Name}.");
    }

    return isEquipped;
}
    
    public virtual bool Unequip(IActor? source, IActor? target, EquipmentSlot slot)
{
    if (target == null) return false;
    
    int mainHandIndex = Array.IndexOf(Enum.GetNames(typeof(EquipmentSlot)), EquipmentSlot.MainHand.ToString());
    int offHandIndex = Array.IndexOf(Enum.GetNames(typeof(EquipmentSlot)), EquipmentSlot.OffHand.ToString());
    bool isUnequipped = false;
    
    int slotIndex = Array.IndexOf(Enum.GetValues(typeof(EquipmentSlot)), slot);
    
    if ((slot & EquipmentSlot.TwoHanded) == EquipmentSlot.TwoHanded)
    {
        if (target.Equipment[mainHandIndex] == this)
        {
            target.Equipment[mainHandIndex] = null;
        }

        if (target.Equipment[offHandIndex] == this)
        {
            target.Equipment[offHandIndex] = null;
        }
    }
    else
    {
        if (target.Equipment[slotIndex] == this)
        {
            target.Equipment[slotIndex] = null;
        }
    }
    ApplyModifiers(target, false);

    source?.Inventory.Add(this);

    isUnequipped = true;
    return isUnequipped;
}
    
    
    
// public virtual bool Unequip(IActor? source, IActor? target)
// {
//     if (target == null) return false;
//     
//     int mainHandIndex = Array.IndexOf(Enum.GetNames(typeof(EquipmentSlot)), EquipmentSlot.MainHand.ToString());
//     int offHandIndex = Array.IndexOf(Enum.GetNames(typeof(EquipmentSlot)), EquipmentSlot.OffHand.ToString());
//     bool isUnequipped = false;
//
//     foreach (var slot in Slots)
//     {
//         int slotIndex = Array.IndexOf(Enum.GetValues(typeof(EquipmentSlot)), slot);
//
//         if ((slot & EquipmentSlot.TwoHanded) == EquipmentSlot.TwoHanded)
//         {
//             if (target.Equipment[mainHandIndex] == this)
//             {
//                 target.Equipment[mainHandIndex] = null;
//             }
//
//             if (target.Equipment[offHandIndex] == this)
//             {
//                 target.Equipment[offHandIndex] = null;
//             }
//         }
//         else
//         {
//             if (target.Equipment[slotIndex] == this)
//             {
//                 target.Equipment[slotIndex] = null;
//             }
//         }
//     }
//     ApplyModifiers(target, false);
//
//     source?.Inventory.Add(this);
//
//     isUnequipped = true;
//     return isUnequipped;
// }

public virtual void ApplyModifiers(IActor target, bool add)
{
    if (this is IWeaponItem weaponItem)
    {
        double modifierValue = weaponItem.DamageMultiplier.CurrentValue;
        if (add) target.DamageMultiplier.AddModifier(modifierValue);
        else target.DamageMultiplier.RemoveModifier(modifierValue);
    }

    if (this is IArmorItem armorItem)
    {
        double modifierValue = armorItem.DamageReductionMultiplier.CurrentValue;
        if (add) target.DamageReductionMultiplier.AddModifier(modifierValue);
        else target.DamageReductionMultiplier.RemoveModifier(modifierValue);
    }

    // Assume Stats is an object that affects multiple actor stats
    if (add) target.ActorStats.AddModifier(Stats);
    else target.ActorStats.RemoveModifier(Stats);
}

    public override IItem Copy()
    {
        return new EquipmentItem(Name, Description, Price, Type, Slots, Stats, GearTier, AllowedClasses, RequiredLevel, Stackable, Quantity);
    }
}