using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Lists;

namespace DragonsEnd.Items.Inventory.Interfaces
{
    public interface IInventory
    {
        GoldCurrency Gold { get; set; }
        ItemList<IItem> Items { get; set; }
        IItem? this[string name] { get; }
    }
}