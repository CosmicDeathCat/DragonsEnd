using System;
using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Items.Equipment;

[Serializable]
public class ArmorItem : EquipmentItem, IArmorItem
{
    public virtual CombatStyle CombatStyle { get; set; }
    public virtual ArmorType ArmorType { get; set; }
    public virtual DoubleStat DamageReductionMultiplier { get; set; }
    
    public ArmorItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, GearTier gearTier, CharacterClassType allowedClasses, int requiredLevel, CombatStyle combatStyle, ArmorType armorType, double damageReductionMultiplier, bool stackable = true, long quantity = 1, double dropRate = 1) : base(name, description, price, type, slots, stats, gearTier, allowedClasses, requiredLevel, stackable, quantity, dropRate)
    {
        CombatStyle = combatStyle;
        ArmorType = armorType;
        DamageReductionMultiplier = new DoubleStat(damageReductionMultiplier);
    }

    public override IItem Copy()
    {
        return new ArmorItem(Name, Description, Price, Type, Slots, Stats, GearTier, AllowedClasses, RequiredLevel, CombatStyle, ArmorType, DamageReductionMultiplier.BaseValue, Stackable, Quantity);
    }
}