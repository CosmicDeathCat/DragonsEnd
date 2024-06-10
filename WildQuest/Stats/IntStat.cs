using System;

[Serializable]
public class IntStat 
{         
    private int _currentValue;
    private int _baseValue;
    private int _modifierValue;

    public int CurrentValue
    {
        get => _currentValue;
        set => _currentValue = Math.Clamp(value, int.MinValue, MaxValue); // Ensures CurrentValue never exceeds MaxValue.
    }
   
    public int MaxValue => _baseValue + _modifierValue; // Calculates MaxValue on the fly.

    public int BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            _currentValue = Math.Min(_currentValue, MaxValue); // Adjust CurrentValue if necessary.
        }
    }

    public int ModifierValue
    {
        get => _modifierValue;
        set
        {
            _modifierValue = value;
            _currentValue = Math.Min(_currentValue, MaxValue); // Adjust CurrentValue if necessary.
        }
    }

    public IntStat(int baseValue)
    {	
        BaseValue = baseValue;
        CurrentValue = baseValue; // Initialize CurrentValue to BaseValue.
    }
   
    public IntStat(int currentValue, int baseValue)
    {
        _baseValue = baseValue;
        CurrentValue = currentValue; // Set CurrentValue, which will clamp to the calculated MaxValue.
    }

    public static IntStat operator +(IntStat a, IntStat b)
    {
        return new IntStat(a.CurrentValue + b.CurrentValue, a.BaseValue + b.BaseValue);
    }
   
    public static IntStat operator -(IntStat a, IntStat b)
    {
        return new IntStat(a.CurrentValue - b.CurrentValue, a.BaseValue - b.BaseValue);
    }
   
    public static IntStat operator *(IntStat a, IntStat b)
    {
        return new IntStat(a.CurrentValue * b.CurrentValue, a.BaseValue * b.BaseValue);
    }
   
    public static IntStat operator /(IntStat a, IntStat b)
    {
        return b.CurrentValue != 0 ? // Prevent division by zero.
            new IntStat(a.CurrentValue / b.CurrentValue, a.BaseValue / b.BaseValue) : new IntStat(0, a.BaseValue / (b.BaseValue == 0 ? 1 : b.BaseValue)); // Handle zero division safely.
    }

    public void AddModifier(int modifier)
    {
        ModifierValue += modifier;
    }

    public void RemoveModifier(int modifier)
    {
        ModifierValue -= modifier;
    }
}
