using DragonsEnd.Enums;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Items.Equipment.Interfaces
{
    public interface IArmorItem : IEquipmentItem
    {
        CombatStyle CombatStyle { get; set; }
        ArmorType ArmorType { get; set; }
        DoubleStat DamageReductionMultiplier { get; set; }
    }
}