using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Status.Interfaces
{
    public interface IRestoreItem : IItem
    {
        double HealthHealthRestorePercentage { get; set; }
        double ManaRestorePercentage { get; set; }
        double StaminaRestorePercentage { get; set; }
    }
}