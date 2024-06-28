using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Skills;
using DragonsEnd.Stats;

namespace DragonsEnd.Items.Equipment.Interfaces
{
    public interface IEquipmentItem : IItem
    {
        List<EquipmentSlot> Slots { get; set; }
        ActorStats Stats { get; set; }
        GearTier GearTier { get; set; }
        CharacterClassType AllowedClasses { get; set; }
        List<SkillLevels>? RequiredSkills { get; set; }
        bool Equip(IActor? source, IActor? target);
        bool Unequip(IActor? source, IActor? target, EquipmentSlot slot);
        void ApplyModifiers(IActor target, bool isEquipping);
    }
}