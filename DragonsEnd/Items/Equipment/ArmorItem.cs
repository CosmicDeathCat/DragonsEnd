using System;
using System.Collections.Generic;
using DragonsEnd.Enums;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Items.Equipment
{
    [Serializable]
    public class ArmorItem : EquipmentItem, IArmorItem
    {
        public ArmorItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots,
            ActorStats stats, GearTier gearTier, CharacterClassType allowedClasses, int requiredLevel,
            CombatStyle combatStyle, ArmorType armorType, double damageReductionMultiplier, bool stackable = true,
            long quantity = 1, double dropRate = 1) : base(name, description, price, type, slots, stats, gearTier,
            allowedClasses, requiredLevel, stackable, quantity, dropRate)
        {
            CombatStyle = combatStyle;
            ArmorType = armorType;
            DamageReductionMultiplier = new DoubleStat(damageReductionMultiplier);
        }

        public virtual CombatStyle CombatStyle { get; set; }
        public virtual ArmorType ArmorType { get; set; }
        public virtual DoubleStat DamageReductionMultiplier { get; set; }

        public override IItem Copy()
        {
            return new ArmorItem(Name, Description, Price, Type, Slots, Stats, GearTier, AllowedClasses, RequiredLevel,
                CombatStyle, ArmorType, DamageReductionMultiplier.BaseValue, Stackable, Quantity);
        }
    }
}