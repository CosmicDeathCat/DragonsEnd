using System;

namespace DragonsEnd.Stats.Stat
{
    [Serializable]
    public class LongStat
    {
        private long _baseValue;
        private long _currentValue;
        private long _modifierValue;

        public LongStat(long baseValue)
        {
            BaseValue = baseValue;
            CurrentValue = baseValue; // Initialize CurrentValue to BaseValue.
        }

        public LongStat(long currentValue, long baseValue)
        {
            _baseValue = baseValue;
            CurrentValue = currentValue; // Set CurrentValue, which will clamp to the calculated MaxValue.
        }

        public long CurrentValue
        {
            get => _currentValue;
            set => _currentValue =
                Math.Clamp(value: value, min: long.MinValue, max: MaxValue); // Ensures CurrentValue never exceeds MaxValue.
        }

        public long MaxValue => _baseValue + _modifierValue; // Calculates MaxValue on the fly.

        public long BaseValue
        {
            get => _baseValue;
            set
            {
                _baseValue = value;
                _currentValue = MaxValue; // Directly set CurrentValue to MaxValue.
            }
        }

        public long ModifierValue
        {
            get => _modifierValue;
            set
            {
                _modifierValue = value;
                _currentValue = MaxValue; // Directly set CurrentValue to MaxValue.
            }
        }

        public static LongStat operator +(LongStat a, LongStat b)
        {
            return new LongStat(currentValue: a.CurrentValue + b.CurrentValue, baseValue: a.BaseValue + b.BaseValue);
        }

        public static LongStat operator -(LongStat a, LongStat b)
        {
            return new LongStat(currentValue: a.CurrentValue - b.CurrentValue, baseValue: a.BaseValue - b.BaseValue);
        }

        public static LongStat operator *(LongStat a, LongStat b)
        {
            return new LongStat(currentValue: a.CurrentValue * b.CurrentValue, baseValue: a.BaseValue * b.BaseValue);
        }

        public static LongStat operator /(LongStat a, LongStat b)
        {
            return b.CurrentValue != 0
                ? // Prevent division by zero.
                new LongStat(currentValue: a.CurrentValue / b.CurrentValue, baseValue: a.BaseValue / b.BaseValue)
                : new LongStat(currentValue: 0, baseValue: a.BaseValue / (b.BaseValue == 0 ? 1 : b.BaseValue)); // Handle zero division safely.
        }

        public void AddModifier(long modifier)
        {
            ModifierValue += modifier;
        }

        public void RemoveModifier(long modifier)
        {
            ModifierValue -= modifier;
        }
    }
}