using System;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Drops
{
    [Serializable]
    public class DropItem : IDropItem
    {
        public DropItem(IItem item, double dropRate)
        {
            Item = item;
            DropRate = dropRate;
        }

        public IItem Item { get; }
        public double DropRate { get; }
    }
}