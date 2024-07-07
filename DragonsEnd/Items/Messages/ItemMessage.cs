using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Messages
{
    public struct ItemMessage
    {
        public IItem Item { get; }
        public IActor? Source { get; }
        public List<IActor?>? Targets { get; }
        
        public ItemMessage(IItem item, IActor? source, List<IActor?>? targets)
        {
            Item = item;
            Source = source;
            Targets = targets;
        }
    }
}