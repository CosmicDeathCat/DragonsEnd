using WildQuest.Enums;

namespace WildQuest.Interfaces;

public interface IItem
{
    string Name {get;set;}
    string Description {get;set;}
    long Price {get;set;}
    ItemType Type {get;set;}
    bool Stackable {get;set;}
    long Quantity {get;set;}
    double DropRate {get;set;}
    void Use(IActor? source, IActor? target);
    IItem Copy();
}