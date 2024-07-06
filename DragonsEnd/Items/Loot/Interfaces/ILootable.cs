using DragonsEnd.Identity.Interfaces;

namespace DragonsEnd.Items.Loot.Interfaces
{
    public interface ILootable : IIdentity
    {
        ILootContainer? LootContainer { get; set; }
        ILootConfig? LootConfig { get; set; }
        bool HasAlreadyBeenLooted { get; set; }

        ILootContainer? Loot
        (
            ILootConfig? lootConfig = null
        );
    }
}