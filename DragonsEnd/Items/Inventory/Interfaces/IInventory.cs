using System.Collections.Generic;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Inventory.Interfaces
{
    public interface IInventory
    {
        GoldCurrency Gold { get; set; }
        List<IItem> Items { get; set; }
        IItem? this[string name] { get; }
    }
}