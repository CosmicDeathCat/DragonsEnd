using WildQuest.Enums;
using WildQuest.Stats;

namespace WildQuest.Interfaces;

public interface IWeaponItem : IEquipmentItem
{
    CombatStyle CombatStyle { get; set; }
    WeaponType WeaponType { get; set; }
    DoubleStat DamageMultiplier { get; set; }
}