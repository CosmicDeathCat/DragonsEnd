using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Enums.WildQuest.Enums;
using WildQuest.Stats;

namespace WildQuest.Interfaces;

public interface IEquipmentItem : IItem
{
    List<EquipmentSlot> Slots { get; set; }
    ActorStats Stats {get;set;}
    GearTier GearTier {get;set;}
    void Equip(IActor? source, IActor? target);
    void Unequip(IActor? source, IActor? target);
}