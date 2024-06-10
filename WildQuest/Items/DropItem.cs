using System;
using WildQuest.Interfaces;

namespace WildQuest.Items;

[Serializable]
public class DropItem : IDropItem
{
    public IItem Item { get; }
    public double DropRate { get; }
    
    public DropItem(IItem item, double dropRate)
    {
        Item = item;
        DropRate = dropRate;
    }
}