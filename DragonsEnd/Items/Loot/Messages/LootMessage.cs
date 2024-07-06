using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;

namespace DragonsEnd.Items.Loot.Messages
{
    public struct LootMessage
    {
        public List<IActor> SourceActors { get; }
        public List<IActor> TargetActors { get; }
        public List<ILootContainer> IndividualLoot { get; }
        public ILootContainer? MergedLoot { get; set; }

        public LootMessage(List<IActor> sourceActors, List<IActor> targetActors, List<ILootContainer?> individualLoot, ILootContainer? mergedLoot)
        {
            SourceActors = sourceActors;
            TargetActors = targetActors;
            IndividualLoot = individualLoot;
            MergedLoot = mergedLoot;
        }
    }
}