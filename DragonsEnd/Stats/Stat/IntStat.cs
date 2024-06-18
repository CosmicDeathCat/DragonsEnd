using System;

namespace DragonsEnd.Stats.Stat
{
    [Serializable]
    public class IntStat
    {
        private int _baseValue;
        private int _currentValue;
        private int _modifierValue;

        public IntStat(int baseValue)
        {
            BaseValue = baseValue; // Initialize base and current values to the baseValue provided.
            CurrentValue = baseValue; // Set the CurrentValue to the base value initially.
        }

        public IntStat(int currentValue, int baseValue)
        {
            _baseValue = baseValue;
            CurrentValue = currentValue; // Initialize and clamp currentValue within the calculated MaxValue.
        }

        public int CurrentValue
        {
            get => _currentValue;
            set => _currentValue =
                Math.Clamp(value: value, min: int.MinValue, max: MaxValue); // Ensures CurrentValue never exceeds MaxValue.
        }

        public int MaxValue => _baseValue + _modifierValue; // Calculates MaxValue on the fly.

        public int BaseValue
        {
            get => _baseValue;
            set
            {
                _baseValue = value;
                _currentValue = MaxValue; // Directly set CurrentValue to MaxValue when modifiers are changed.
            }
        }

        public int ModifierValue
        {
            get => _modifierValue;
            set
            {
                _modifierValue = value;
                _currentValue = MaxValue; // Directly set CurrentValue to MaxValue when modifiers are changed.
            }
        }

        public static IntStat operator +(IntStat a, IntStat b)
        {
            return new IntStat(currentValue: a.CurrentValue + b.CurrentValue, baseValue: a.BaseValue + b.BaseValue);
        }

        public static IntStat operator -(IntStat a, IntStat b)
        {
            return new IntStat(currentValue: a.CurrentValue - b.CurrentValue, baseValue: a.BaseValue - b.BaseValue);
        }

        public static IntStat operator *(IntStat a, IntStat b)
        {
            return new IntStat(currentValue: a.CurrentValue * b.CurrentValue, baseValue: a.BaseValue * b.BaseValue);
        }

        public static IntStat operator /(IntStat a, IntStat b)
        {
            return b.CurrentValue != 0
                ? new IntStat(currentValue: a.CurrentValue / b.CurrentValue, baseValue: a.BaseValue / b.BaseValue)
                : new IntStat(currentValue: 0,
                    baseValue: a.BaseValue / (b.BaseValue == 0 ? 1 : b.BaseValue)); // Handle division by zero gracefully.
        }

        public void AddModifier(int modifier)
        {
            ModifierValue +=
                modifier; // This will automatically adjust CurrentValue if necessary due to the setter logic.
        }

        public void RemoveModifier(int modifier)
        {
            ModifierValue -=
                modifier; // This will automatically adjust CurrentValue if necessary due to the setter logic.
        }
    }
}