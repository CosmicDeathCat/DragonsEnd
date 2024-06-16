using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Drops.Interfaces
{
    public interface IDropItem
    {
        IItem Item { get; }
        double DropRate { get; }
    }
}