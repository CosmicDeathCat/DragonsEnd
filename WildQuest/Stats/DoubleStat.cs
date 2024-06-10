using System;

namespace WildQuest.Stats;

[Serializable]
public class DoubleStat
{
    private double _currentValue;
    private double _baseValue;
    private double _modifierValue;

    public double CurrentValue
    {
        get => _currentValue;
        set => _currentValue = Math.Clamp(value, double.MinValue, MaxValue); // Ensures CurrentValue never exceeds MaxValue.
    }
   
    public double MaxValue => _baseValue + _modifierValue; // Calculates MaxValue on the fly.

    public double BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            _currentValue = MaxValue; // Directly set CurrentValue to MaxValue.
        }
    }

    public double ModifierValue
    {
        get => _modifierValue;
        set
        {
            _modifierValue = value;
            _currentValue = MaxValue; // Directly set CurrentValue to MaxValue.
        }
    }

    public DoubleStat(double baseValue)
    {	
        BaseValue = baseValue;
        CurrentValue = baseValue; // Initialize CurrentValue to BaseValue.
    }
   
    public DoubleStat(double currentValue, double baseValue)
    {
        _baseValue = baseValue;
        CurrentValue = currentValue; // Set CurrentValue, which will clamp to the calculated MaxValue.
    }

    public static DoubleStat operator +(DoubleStat a, DoubleStat b)
    {
        return new DoubleStat(a.CurrentValue + b.CurrentValue, a.BaseValue + b.BaseValue);
    }
   
    public static DoubleStat operator -(DoubleStat a, DoubleStat b)
    {
        return new DoubleStat(a.CurrentValue - b.CurrentValue, a.BaseValue - b.BaseValue);
    }
   
    public static DoubleStat operator *(DoubleStat a, DoubleStat b)
    {
        return new DoubleStat(a.CurrentValue * b.CurrentValue, a.BaseValue * b.BaseValue);
    }
   
    public static DoubleStat operator /(DoubleStat a, DoubleStat b)
    {
        return b.CurrentValue != 0 ? // Prevent division by zero.
            new DoubleStat(a.CurrentValue / b.CurrentValue, a.BaseValue / (b.BaseValue == 0 ? 1 : b.BaseValue)) : new DoubleStat(0, a.BaseValue); // Handle zero division safely.
    }

    public void AddModifier(double modifier)
    {
        ModifierValue += modifier;
    }

    public void RemoveModifier(double modifier)
    {
        ModifierValue -= modifier;
    }
}
