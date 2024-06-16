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
    public class WeaponItem : EquipmentItem, IWeaponItem
    {
        public WeaponItem(string name, string description, long price, ItemType type, List<EquipmentSlot> slots,
            ActorStats stats, GearTier gearTier, CharacterClassType allowedClasses, int requiredLevel,
            CombatStyle combatStyle, WeaponType weaponType, double damageMultiplier, bool stackable = true,
            long quantity = 1, double dropRate = 1) : base(name, description, price, type, slots, stats, gearTier,
            allowedClasses, requiredLevel, stackable, quantity, dropRate)
        {
            CombatStyle = combatStyle;
            WeaponType = weaponType;
            DamageMultiplier = new DoubleStat(damageMultiplier);
        }

        public virtual CombatStyle CombatStyle { get; set; }
        public virtual WeaponType WeaponType { get; set; }
        public virtual DoubleStat DamageMultiplier { get; set; }

        public override IItem Copy()
        {
            return new WeaponItem(Name, Description, Price, Type, Slots, Stats, GearTier, AllowedClasses, RequiredLevel,
                CombatStyle, WeaponType, DamageMultiplier.BaseValue, Stackable, Quantity);
        }
    }
}