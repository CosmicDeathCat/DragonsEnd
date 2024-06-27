using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;

namespace DragonsEnd.Items.Inventory
{
    public class Inventory : IInventory
    {
        public virtual GoldCurrency Gold { get; set; } = new(0);
        public virtual List<IItem> Items { get; set; } = new();

        public IItem? this[string name] => Items.FirstOrDefault(x => x != null && x.Name.Equals(name));

        public Inventory()
        {
            
        }

        public Inventory(long gold, params IItem[] items)
        {
            Gold = new GoldCurrency(gold);
            Items.AddRange(items);
        }
        
    }
}