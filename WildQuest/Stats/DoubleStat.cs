using System;

namespace WildQuest.Stats;

[Serializable]
public class DoubleStat
{
    protected double _currentValue;
    protected double _maxValue;
    protected double _baseValue;
    protected double _modifierValue;
    
    public virtual double CurrentValue
    {
        get => _currentValue;
        set => _currentValue = Math.Clamp(value, double.MinValue, _maxValue);
    }
   
    public virtual double MaxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value;
            _currentValue = _maxValue;
        }
    }

    public virtual double BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            CalculateMaxValue();
        }
    }

    public virtual double ModifierValue
    {
        get => _modifierValue;
        set
        {
            _modifierValue = value;
            CalculateMaxValue();
        }
    }

    public DoubleStat(double baseValue)
    {	
        BaseValue = baseValue;
        CurrentValue = baseValue;
        CalculateMaxValue();
    }
   
    public DoubleStat(double currentValue, double baseValue)
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
        return new DoubleStat(a.CurrentValue / b.CurrentValue, a.BaseValue / b.BaseValue);
    }

    public virtual void AddModifier(double modifier)
    {
        ModifierValue += modifier;
        CalculateMaxValue();
    }

    public virtual void RemoveModifier(double modifier)
    {
        ModifierValue -= modifier;
        CalculateMaxValue();
    }
}