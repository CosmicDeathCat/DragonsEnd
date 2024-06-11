using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Stats;

namespace WildQuest.Interfaces;

public interface IEquipmentItem : IItem
{
    List<EquipmentSlot> Slots { get; set; }
    ActorStats Stats {get;set;}
    GearTier GearTier {get;set;}
    CharacterClassType AllowedClasses {get;set;}
    int RequiredLevel {get;set;}
    bool Equip(IActor? source, IActor? target);
    bool Unequip(IActor? source, IActor? target, EquipmentSlot slot);
}