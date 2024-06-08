using WildQuest.Interfaces;

namespace WildQuest.Items.Currency.Extensions;

public static class CurrencyExtensions
{
    public static void Add(this ICurrency currency, long amount)
    {
        currency.Quantity += amount;
    }
    
    public static void Subtract(this ICurrency currency, long amount)
    {
        currency.Quantity -= amount;
    }
    
    public static void Multiply(this ICurrency currency, long amount)
    {
        currency.Quantity *= amount;
    }
    
    public static void Divide(this ICurrency currency, long amount)
    {
        currency.Quantity /= amount;
    }
    
    public static void Set(this ICurrency currency, long amount)
    {
        currency.Quantity = amount;
    }
    
    public static void Reset(this ICurrency currency)
    {
        currency.Quantity = 0;
    }
    
    public static void Add(this ICurrency currency, ICurrency otherCurrency)
    {
        currency.Quantity += otherCurrency.Quantity;
    }
    
    public static void Subtract(this ICurrency currency, ICurrency otherCurrency)
    {
        currency.Quantity -= otherCurrency.Quantity;
    }
    
    public static void Multiply(this ICurrency currency, ICurrency otherCurrency)
    {
        currency.Quantity *= otherCurrency.Quantity;
    }
    
    public static void Divide(this ICurrency currency, ICurrency otherCurrency)
    {
        currency.Quantity /= otherCurrency.Quantity;
    }
    
    public static void Set(this ICurrency currency, ICurrency otherCurrency)
    {
        currency.Quantity = otherCurrency.Quantity;
    }
    

}