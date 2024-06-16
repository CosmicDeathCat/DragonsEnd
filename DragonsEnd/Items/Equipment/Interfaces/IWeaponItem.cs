using DragonsEnd.Enums;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Items.Equipment.Interfaces
{
    public interface IWeaponItem : IEquipmentItem
    {
        CombatStyle CombatStyle { get; set; }
        WeaponType WeaponType { get; set; }
        DoubleStat DamageMultiplier { get; set; }
    }
}