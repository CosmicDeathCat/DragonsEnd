using DragonsEnd.Identity.Interfaces;

namespace DragonsEnd.Items.Currency.Interfaces
{
    public interface ICurrency : IIdentity
    {
        string Description { get; set; }
        long BaseValue { get; set; }
        long CurrentValue { get; }
        long Quantity { get; set; }
    }
}