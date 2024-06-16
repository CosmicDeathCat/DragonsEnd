using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Status.Interfaces
{
    public interface IHealthItem : IItem
    {
        int HealPercentage { get; set; }
    }
}