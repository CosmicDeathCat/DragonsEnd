using System;
using WildQuest.Interfaces;

namespace WildQuest.Items.Currency;

[Serializable]
public class GoldCurrency : ICurrency
{
    public virtual string Name { get; set; } = "Gold";
    public virtual string Description { get; set; } = "Shiny gold coins. used for purchasing items.";
    public virtual long CurrentValue
    {
        get
        {
            if (Quantity == 0)
            {
                return 0;
            }
            return BaseValue * Quantity;
        }
    }

    public virtual long BaseValue { get; set; } = 1;
    public virtual long Quantity { get; set; } = 0;
    
    public GoldCurrency()
    {
        
    }
    
    public GoldCurrency(long quantity)
    {
        Quantity = quantity;
    }
    
    public GoldCurrency(long quantity, long baseValue)
    {
        Quantity = quantity;
        BaseValue = baseValue;
    }
    
    public static long operator +(GoldCurrency currency, GoldCurrency otherCurrency)
    {
        return currency.Quantity + otherCurrency.Quantity;
    }
    
    public static long operator -(GoldCurrency currency, GoldCurrency otherCurrency)
    {
        return currency.Quantity - otherCurrency.Quantity;
    }
    
    public static long operator *(GoldCurrency currency, GoldCurrency otherCurrency)
    {
        return currency.Quantity * otherCurrency.Quantity;
    }
    
    public static long operator /(GoldCurrency currency, GoldCurrency otherCurrency)
    {
        if(currency.CurrentValue <= 0 || otherCurrency.CurrentValue <= 0)
        {
            return 0;
        }
        return currency.Quantity / otherCurrency.Quantity;
    }

    public override string ToString()
    {
        return $"{Quantity} {Name}";
    }
}