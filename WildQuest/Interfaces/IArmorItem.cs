using WildQuest.Enums;
using WildQuest.Stats;

namespace WildQuest.Interfaces;

public interface IArmorItem : IEquipmentItem
{
    CombatStyle CombatStyle { get; set; }
    ArmorType ArmorType { get; set; }
    DoubleStat DamageReductionMultiplier { get; set; }
}