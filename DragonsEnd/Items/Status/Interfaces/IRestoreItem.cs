using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Status.Interfaces
{
    public interface IRestoreItem : IItem
    {
        int HealthHealthRestorePercentage { get; set; }
        int ManaRestorePercentage { get; set; }
        int StaminaRestorePercentage { get; set; }
    }
}