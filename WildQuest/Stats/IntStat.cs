using System;

namespace WildQuest.Stats;
using System;

[Serializable]
public class IntStat 
{         
    protected int _maxValue;
    protected int _currentValue;
    protected int _baseValue;
    protected int _modifierValue;

    public virtual int CurrentValue
    {
        get => _currentValue;
        set => _currentValue = int.Clamp(value, 0, _maxValue);
    }
   
    public virtual int MaxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value;
            _currentValue = _maxValue;
        }
    }

    public virtual int BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            CalculateMaxValue();
        }
    }

    public virtual int ModifierValue
    {
        get => _modifierValue;
        set
        {
            _modifierValue = value;
            CalculateMaxValue();
        }
    }

    public IntStat(int baseValue)
    {	
        BaseValue = baseValue;
    }
   
    public IntStat(int currentValue, int baseValue)
    {
        _currentValue = currentValue;
        _baseValue = baseValue;
        CalculateMaxValue();
    }

    public virtual void CalculateMaxValue()
    {
        _maxValue = _baseValue + _modifierValue;
        _currentValue = Math.Min(_currentValue, _maxValue);
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
        return new IntStat(a.CurrentValue / b.CurrentValue, a.BaseValue / b.BaseValue);
    }

    public virtual void AddModifier(int modifier)
    {
        ModifierValue += modifier;
        CalculateMaxValue();
    }

    public virtual void RemoveModifier(int modifier)
    {
        ModifierValue -= modifier;
        CalculateMaxValue();
    }
}
