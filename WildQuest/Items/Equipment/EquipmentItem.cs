using System;
using System.Collections.Generic;
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

    public EquipmentItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, GearTier gearTier, CharacterClassType allowedClasses, int requiredLevel,  bool stackable = true, long quantity = 1, double dropRate = 1) 
        : base(name, description, price, type, stackable, quantity, dropRate)
    {
        Slots = slots;
        Stats = stats;
        GearTier = gearTier;
        AllowedClasses = allowedClasses;
        RequiredLevel = requiredLevel;
    }
    
    public virtual void Equip(IActor? source, IActor? target)
    {
        foreach (var slot in Slots)
        {
            if (target?.Equipment[(int)slot] != null && target?.Equipment[(int)slot] != this)
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
            target.ActorStats.AddModifier(Stats);
            if (this is IWeaponItem weaponItem)
            {
                target.DamageMultiplier.AddModifier(weaponItem.DamageMultiplier.CurrentValue);
            }

            if (this is IArmorItem armorItem)
            {
                target.DamageReductionMultiplier.AddModifier(armorItem.DamageReductionMultiplier.CurrentValue);
            }
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
            target.ActorStats.RemoveModifier(Stats);
            if (this is IWeaponItem weaponItem)
            {
                target.DamageMultiplier.RemoveModifier(weaponItem.DamageMultiplier.CurrentValue);
            }

            if (this is IArmorItem armorItem)
            {
                target.DamageReductionMultiplier.RemoveModifier(armorItem.DamageReductionMultiplier.CurrentValue);
            }
            source?.Inventory.Add(this);
        }
    }

    public override IItem Copy()
    {
        return new EquipmentItem(Name, Description, Price, Type, Slots, Stats, GearTier, AllowedClasses, RequiredLevel,  Stackable, Quantity);
    }
}