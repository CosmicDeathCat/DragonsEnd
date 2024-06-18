using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Identity.Interfaces;

namespace DragonsEnd.Items.Interfaces
{
    public interface IItem : IIdentity
    {
        string Description { get; set; }
        long Price { get; set; }
        ItemType Type { get; set; }
        bool Stackable { get; set; }
        long Quantity { get; set; }
        double DropRate { get; set; }
        void Use(IActor? source, IActor? target);
        IItem Copy();
    }
}