namespace WildQuest.Interfaces;

public interface ICurrency
{
    string Name {get;set;}
    string Description {get;set;}
    long BaseValue {get;set;}
    long CurrentValue {get;}
    long Quantity {get;set;}
}