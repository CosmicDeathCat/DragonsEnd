using System;
using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Stats;

namespace DragonsEnd.Items.Equipment
{
    [Serializable]
    public class EquipmentItem : Item, IEquipmentItem
    {
        public EquipmentItem
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
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        )
            : base(name: name, description: description, price: price, type: type, stackable: stackable, quantity: quantity, dropRate: dropRate)
        {
            Slots = slots;
            Stats = stats;
            GearTier = gearTier;
            AllowedClasses = allowedClasses;
            RequiredLevel = requiredLevel;
        }

        public virtual List<EquipmentSlot> Slots { get; set; }
        public virtual ActorStats Stats { get; set; }

        public virtual GearTier GearTier { get; set; }

        public virtual CharacterClassType AllowedClasses { get; set; }

        public virtual int RequiredLevel { get; set; }

        public virtual bool Equip(IActor? source, IActor? target)
        {
            if (target == null)
            {
                return false;
            }

            if (!AllowedClasses.HasFlag(flag: target.CharacterClass))
            {
                Console.WriteLine(value: $"{target.Name} cannot equip {Name}. {Name} is only for {AllowedClasses}.");
                return false;
            }

            //TODO: Add level requirements based on skill levels for the gear.
            // if (target.Leveling.CurrentLevel < RequiredLevel)
            // {
            //     Console.WriteLine(
            //         value: $"{target.Name} is not high enough level to equip {Name}. must be at least level {RequiredLevel}.");
            //     return false;
            // }


            var isEquipped = false;
            var mainHandIndex = Array.IndexOf(array: Enum.GetNames(enumType: typeof(EquipmentSlot)), value: EquipmentSlot.MainHand.ToString());
            var offHandIndex = Array.IndexOf(array: Enum.GetNames(enumType: typeof(EquipmentSlot)), value: EquipmentSlot.OffHand.ToString());

            // First, handle items intended for both main and off hand.
            if (Slots.Contains(item: EquipmentSlot.MainHand) && Slots.Contains(item: EquipmentSlot.OffHand))
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
                        target.Equipment[mainHandIndex]?.Unequip(source: source, target: target, slot: EquipmentSlot.MainHand);
                        target.Equipment[mainHandIndex] = this;
                    }
                    else
                    {
                        target.Equipment[offHandIndex]?.Unequip(source: source, target: target, slot: EquipmentSlot.OffHand);
                        target.Equipment[offHandIndex] = this;
                    }
                }

                isEquipped = true;
            }
            else if (Slots.Contains(item: EquipmentSlot.TwoHanded))
            {
                if (target.Equipment[mainHandIndex] != null && target.Equipment[mainHandIndex] != this)
                {
                    target.Equipment[mainHandIndex]?.Unequip(source: source, target: target, slot: EquipmentSlot.MainHand);
                }

                if (target.Equipment[offHandIndex] != null && target.Equipment[offHandIndex] != this)
                {
                    target.Equipment[mainHandIndex]?.Unequip(source: source, target: target, slot: EquipmentSlot.OffHand);
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
                    var slotIndex = Array.IndexOf(array: Enum.GetValues(enumType: typeof(EquipmentSlot)), value: slot);

                    if (target.Equipment[slotIndex] == null || target.Equipment[slotIndex] != this)
                    {
                        // Equip in specific slot if it's empty
                        if (target.Equipment[slotIndex] != null)
                        {
                            target.Equipment[slotIndex]
                                ?.Unequip(source: source, target: target, slot: slot); // Unequip current item in the slot if any
                        }

                        target.Equipment[slotIndex] = this;
                        isEquipped = true;
                        break;
                    }
                }
            }

            if (isEquipped)
            {
                source?.Inventory.Items.Remove(item: this);
                ApplyModifiers(target: target, isEquipping: true);
            }
            else
            {
                Console.WriteLine(value: $"{target.Name} could not equip {Name}.");
            }

            return isEquipped;
        }

        public virtual bool Unequip(IActor? source, IActor? target, EquipmentSlot slot)
        {
            if (target == null)
            {
                return false;
            }

            var mainHandIndex = Array.IndexOf(array: Enum.GetNames(enumType: typeof(EquipmentSlot)), value: EquipmentSlot.MainHand.ToString());
            var offHandIndex = Array.IndexOf(array: Enum.GetNames(enumType: typeof(EquipmentSlot)), value: EquipmentSlot.OffHand.ToString());
            var isUnequipped = false;

            var slotIndex = Array.IndexOf(array: Enum.GetValues(enumType: typeof(EquipmentSlot)), value: slot);

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

            ApplyModifiers(target: target, isEquipping: false);

            source?.Inventory.Items.Add(item: this);

            isUnequipped = true;
            return isUnequipped;
        }

        public virtual void ApplyModifiers(IActor target, bool isEquipping)
        {
            if (this is IWeaponItem weaponItem)
            {
                var modifierValue = weaponItem.DamageMultiplier.CurrentValue;
                if (isEquipping)
                {
                    target.DamageMultiplier.AddModifier(modifier: modifierValue);
                }
                else
                {
                    target.DamageMultiplier.RemoveModifier(modifier: modifierValue);
                }
            }

            if (this is IArmorItem armorItem)
            {
                var modifierValue = armorItem.DamageReductionMultiplier.CurrentValue;
                if (isEquipping)
                {
                    target.DamageReductionMultiplier.AddModifier(modifier: modifierValue);
                }
                else
                {
                    target.DamageReductionMultiplier.RemoveModifier(modifier: modifierValue);
                }
            }

            // Assume Stats is an object that affects multiple actor stats
            if (isEquipping)
            {
                target.ActorStats.AddModifier(modifier: Stats);
            }
            else
            {
                target.ActorStats.RemoveModifier(modifier: Stats);
            }
        }

        public override IItem Copy()
        {
            return new EquipmentItem(name: Name, description: Description, price: Price, type: Type, slots: Slots, stats: Stats, gearTier: GearTier,
                allowedClasses: AllowedClasses,
                requiredLevel: RequiredLevel,
                stackable: Stackable, quantity: Quantity);
        }
    }
}