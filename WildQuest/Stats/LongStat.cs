using System;

namespace WildQuest.Stats;

[Serializable]
public class LongStat
{
    protected long _currentValue;
    protected long _maxValue;
    protected long _baseValue;
    protected long _modifierValue;

    public virtual long CurrentValue
    {
        get => _currentValue;
        set => _currentValue = Math.Clamp(value, 0, _maxValue);
    }

    public virtual long MaxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value;
            _currentValue = _maxValue;
        }
    }

    public virtual long BaseValue
    {
        get => _baseValue;
        set
        {
            _baseValue = value;
            CalculateMaxValue();
        }
    }

    public virtual long ModifierValue
    {
        get => _modifierValue;
        set
        {
            _modifierValue = value;
            CalculateMaxValue();
        }
    }

    public LongStat(long baseValue)
    {
        BaseValue = baseValue;
    }

    public LongStat(long currentValue, long baseValue)
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

    public static LongStat operator +(LongStat a, LongStat b)
    {
        return new LongStat(a.CurrentValue + b.CurrentValue, a.BaseValue + b.BaseValue);
    }

    public static LongStat operator -(LongStat a, LongStat b)
    {
        return new LongStat(a.CurrentValue - b.CurrentValue, a.BaseValue - b.BaseValue);
    }

    public static LongStat operator *(LongStat a, LongStat b)
    {
        return new LongStat(a.CurrentValue * b.CurrentValue, a.BaseValue * b.BaseValue);
    }

    public static LongStat operator /(LongStat a, LongStat b)
    {
        return new LongStat(a.CurrentValue / b.CurrentValue, a.BaseValue / b.BaseValue);
    }

    public virtual void AddModifier(long modifier)
    {
        ModifierValue += modifier;
        CalculateMaxValue();
    }

    public virtual void RemoveModifier(long modifier)
    {
        ModifierValue -= modifier;
        CalculateMaxValue();
    }
}
