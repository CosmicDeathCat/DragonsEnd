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
    public class WeaponItem : EquipmentItem, IWeaponItem
    {
        public virtual CombatStyle CombatStyle { get; set; }
        public virtual WeaponType WeaponType { get; set; }
        public virtual DoubleStat DamageMultiplier { get; set; }

        public WeaponItem
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
            WeaponType weaponType,
            double damageMultiplier,
            List<SkillLevels>? requiredSkills = null,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        ) : base(name: name, description: description, price: price, type: type, slots: slots, stats: stats,
            gearTier: gearTier,
            allowedClasses: allowedClasses, requiredSkills: requiredSkills, stackable: stackable, quantity: quantity, dropRate: dropRate)
        {
            CombatStyle = combatStyle;
            WeaponType = weaponType;
            DamageMultiplier = new DoubleStat(baseValue: damageMultiplier);
        }

        public override IItem Copy()
        {
            return new WeaponItem(name: Name, description: Description, price: Price, type: Type, slots: Slots, stats: Stats, gearTier: GearTier,
                allowedClasses: AllowedClasses, requiredSkills: RequiredSkills,
                combatStyle: CombatStyle, weaponType: WeaponType, damageMultiplier: DamageMultiplier.BaseValue, stackable: Stackable,
                quantity: Quantity);
        }
    }
}