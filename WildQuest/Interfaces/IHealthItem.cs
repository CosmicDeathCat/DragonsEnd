namespace WildQuest.Interfaces;

public interface IHealthItem : IItem
{
    int HealPercentage {get;set;}
}