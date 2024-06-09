using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Items.Equipment;

[System.Serializable]
public class WeaponItem : EquipmentItem, IWeaponItem
{
    public virtual CombatStyle CombatStyle { get; set; }
    public virtual WeaponType WeaponType { get; set; }
    public virtual double DamageMultiplier { get; set; }
    
    public WeaponItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, GearTier gearTier, CombatStyle combatStyle, WeaponType weaponType, double damageMultiplier, bool stackable = true, long quantity = 1, double dropRate = 1) : base(name, description, price, type, slots, stats, gearTier, stackable, quantity, dropRate)
    {
        CombatStyle = combatStyle;
        WeaponType = weaponType;
        DamageMultiplier = damageMultiplier;
    }

    public override void Equip(IActor? source, IActor? target)
    {
        base.Equip(source, target);
        if (target == null) return;
        if (target is ICombatant combatant)
        {
            combatant.DamageMultiplier += DamageMultiplier;
        }
    }
    
    public override void Unequip(IActor? source, IActor? target)
    {
        base.Unequip(source, target);
        if (target == null) return;
        if (target is ICombatant combatant)
        {
            combatant.DamageMultiplier -= DamageMultiplier;
        }
    }

    public override IItem Copy()
    {
        return new WeaponItem(Name, Description, Price, Type, Slots, Stats, GearTier, CombatStyle, WeaponType, DamageMultiplier, Stackable, Quantity);
    }
}