using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Items.Equipment;

[System.Serializable]
public class ArmorItem : EquipmentItem, IArmorItem
{
    public virtual CombatStyle CombatStyle { get; set; }
    public virtual ArmorType ArmorType { get; set; }
    public virtual double DamageReductionMultiplier { get; set; }
    
    public ArmorItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, GearTier gearTier, CombatStyle combatStyle, ArmorType armorType, double damageReductionMultiplier, bool stackable = true, long quantity = 1, double dropRate = 1) : base(name, description, price, type, slots, stats, gearTier, stackable, quantity, dropRate)
    {
        CombatStyle = combatStyle;
        ArmorType = armorType;
        DamageReductionMultiplier = damageReductionMultiplier;
    }

    public override void Equip(IActor? source, IActor? target)
    {
        base.Equip(source, target);
        if (target == null) return;
        if (target is ICombatant combatant)
        {
            combatant.DamageReductionMultiplier += DamageReductionMultiplier;
        }
    }

    public override void Unequip(IActor? source, IActor? target)
    {
        base.Unequip(source, target);
        if (target == null) return;
        if (target is ICombatant combatant)
        {
            combatant.DamageReductionMultiplier -= DamageReductionMultiplier;
        }
    }

    public override IItem Copy()
    {
        return new ArmorItem(Name, Description, Price, Type, Slots, Stats, GearTier, CombatStyle, ArmorType, DamageReductionMultiplier, Stackable, Quantity);
    }
}