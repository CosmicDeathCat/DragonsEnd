namespace WildQuest.Interfaces;

public interface IDropItem
{
    IItem Item { get; }
    double DropRate { get; }
    
}