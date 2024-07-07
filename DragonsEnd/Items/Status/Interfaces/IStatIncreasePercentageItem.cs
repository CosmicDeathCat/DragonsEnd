using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Status.Interfaces
{
    public interface IStatIncreasePercentageItem : IItem
    {
        double HealthIncreasePercentage { get; set; }
        double ManaIncreasePercentage { get; set; }
        double StaminaIncreasePercentage { get; set; }
        double MeleeAttackIncreasePercentage { get; set; }
        double MeleeDefenseIncreasePercentage { get; set; }
        double RangedAttackIncreasePercentage { get; set; }
        double RangedDefenseIncreasePercentage { get; set; }
        double MagicAttackIncreasePercentage { get; set; }
        double MagicDefenseIncreasePercentage { get; set; }
        double CriticalHitChanceIncreasePercentage { get; set; }
    }
}