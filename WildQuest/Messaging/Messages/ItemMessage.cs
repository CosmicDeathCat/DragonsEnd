using WildQuest.Interfaces;

namespace WildQuest.Messaging.Messages;

public struct ItemMessage
{
    public IItem Item { get; }
    public IActor? Source { get; }
    public IActor? Target { get; }
    
    public ItemMessage(IItem item, IActor? source, IActor? target)
    {
        Item = item;
        Source = source;
        Target = target;
    }
}