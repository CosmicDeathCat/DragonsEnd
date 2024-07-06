using System.Linq;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Lists;

namespace DragonsEnd.Items.Inventory
{
    public class Inventory : IInventory
    {
        public virtual GoldCurrency Gold { get; set; } = new(quantity: 0);
        public virtual ItemList<IItem> Items { get; set; } = new();

        public IItem? this[string name] { get => Items.FirstOrDefault(predicate: x => x != null && x.Name.Equals(value: name)); }

        public Inventory()
        {
        }

        public Inventory(long gold, params IItem[] items)
        {
            Gold = new GoldCurrency(quantity: gold);
            Items.AddRange(collection: items);
        }
    }
}