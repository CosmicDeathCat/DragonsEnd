using System;

namespace DragonsEnd.Stats.Stat
{
    [Serializable]
    public class DoubleStat
    {
        private double _baseValue;
        private double _currentValue;
        private double _modifierValue;

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

        public double CurrentValue
        {
            get => _currentValue;
            set => _currentValue =
                Math.Clamp(value: value, min: double.MinValue, max: MaxValue); // Ensures CurrentValue never exceeds MaxValue.
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

        public static DoubleStat operator +(DoubleStat a, DoubleStat b)
        {
            return new DoubleStat(currentValue: a.CurrentValue + b.CurrentValue, baseValue: a.BaseValue + b.BaseValue);
        }

        public static DoubleStat operator -(DoubleStat a, DoubleStat b)
        {
            return new DoubleStat(currentValue: a.CurrentValue - b.CurrentValue, baseValue: a.BaseValue - b.BaseValue);
        }

        public static DoubleStat operator *(DoubleStat a, DoubleStat b)
        {
            return new DoubleStat(currentValue: a.CurrentValue * b.CurrentValue, baseValue: a.BaseValue * b.BaseValue);
        }

        public static DoubleStat operator /(DoubleStat a, DoubleStat b)
        {
            return b.CurrentValue != 0
                ? // Prevent division by zero.
                new DoubleStat(currentValue: a.CurrentValue / b.CurrentValue, baseValue: a.BaseValue / (b.BaseValue == 0 ? 1 : b.BaseValue))
                : new DoubleStat(currentValue: 0, baseValue: a.BaseValue); // Handle zero division safely.
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
}