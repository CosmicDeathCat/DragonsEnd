using WildQuest.Enums;

namespace WildQuest.Interfaces;

public interface IWeaponItem : IEquipmentItem
{
    CombatStyle CombatStyle { get; set; }
    WeaponType WeaponType { get; set; }
    double DamageMultiplier { get; set; }
}