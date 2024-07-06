using System;
using System.Collections.Generic;
using DragonsEnd.Enums;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Skills;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Items.Equipment
{
    [Serializable]
    public class ArmorItem : EquipmentItem, IArmorItem
    {
        public virtual CombatStyle CombatStyle { get; set; }
        public virtual ArmorType ArmorType { get; set; }
        public virtual DoubleStat DamageReductionMultiplier { get; set; }

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
            CombatStyle combatStyle,
            ArmorType armorType,
            double damageReductionMultiplier,
            List<SkillLevels>? requiredSkills = null,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        ) : base(name: name, description: description, price: price, type: type, slots: slots, stats: stats,
            gearTier: gearTier,
            allowedClasses: allowedClasses, requiredSkills: requiredSkills, stackable: stackable, quantity: quantity, dropRate: dropRate)
        {
            CombatStyle = combatStyle;
            ArmorType = armorType;
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
        }

        public override IItem Copy()
        {
            return new ArmorItem(name: Name, description: Description, price: Price, type: Type, slots: Slots, stats: Stats, gearTier: GearTier,
                allowedClasses: AllowedClasses, requiredSkills: RequiredSkills,
                combatStyle: CombatStyle, armorType: ArmorType, damageReductionMultiplier: DamageReductionMultiplier.BaseValue, stackable: Stackable,
                quantity: Quantity);
        }
    }
}