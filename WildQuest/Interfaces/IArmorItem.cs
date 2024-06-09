using WildQuest.Enums;

namespace WildQuest.Interfaces;

public interface IArmorItem : IEquipmentItem
{
    CombatStyle CombatStyle { get; set; }
    ArmorType ArmorType { get; set; }
    double DamageReductionMultiplier { get; set; }
}