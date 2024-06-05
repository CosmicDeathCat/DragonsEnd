using WildQuest.Enums;

namespace WildQuest.Interfaces;

public interface IItem
{
    string Name {get;set;}
    string Description {get;set;}
    int Price {get;set;}
    ItemType Type {get;set;}
    bool Stackable {get;set;}
    int Quantity {get;set;}
    void Use(IActor? source, IActor? target);
}