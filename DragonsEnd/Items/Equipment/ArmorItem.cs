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
        public ArmorItem
        (
            string name,
            string description,
            long price,
            ItemType type,
            List<EquipmentSlot> slots,
            ActorStats stats,
            GearTier gearTier,
            CharacterClassType allowedClasses,
            int requiredLevel,
            CombatStyle combatStyle,
            ArmorType armorType,
            double damageReductionMultiplier,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        ) : base(name: name, description: description, price: price, type: type, slots: slots, stats: stats,
            gearTier: gearTier,
            allowedClasses: allowedClasses, requiredLevel: requiredLevel, stackable: stackable, quantity: quantity, dropRate: dropRate)
        {
            CombatStyle = combatStyle;
            ArmorType = armorType;
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
        }

        public virtual CombatStyle CombatStyle { get; set; }
        public virtual ArmorType ArmorType { get; set; }
        public virtual DoubleStat DamageReductionMultiplier { get; set; }

        public override IItem Copy()
        {
            return new ArmorItem(name: Name, description: Description, price: Price, type: Type, slots: Slots, stats: Stats, gearTier: GearTier,
                allowedClasses: AllowedClasses, requiredLevel: RequiredLevel,
                combatStyle: CombatStyle, armorType: ArmorType, damageReductionMultiplier: DamageReductionMultiplier.BaseValue, stackable: Stackable,
                quantity: Quantity);
        }
    }
}