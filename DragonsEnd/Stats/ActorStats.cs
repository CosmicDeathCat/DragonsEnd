using System;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Stats
{
    [Serializable]
    public class ActorStats
    {
        public ActorStats(int health, int meleeAttack, int meleeDefense, int rangedAttack, int rangedDefense,
            int magicAttack, int magicDefense, double criticalHitChance = 0.05)
        {
            Health = new IntStat(baseValue: health);
            MeleeAttack = new IntStat(baseValue: meleeAttack);
            RangedAttack = new IntStat(baseValue: rangedAttack);
            MagicAttack = new IntStat(baseValue: magicAttack);
            MeleeDefense = new IntStat(baseValue: meleeDefense);
            RangedDefense = new IntStat(baseValue: rangedDefense);
            MagicDefense = new IntStat(baseValue: magicDefense);
            CriticalHitChance = new DoubleStat(baseValue: criticalHitChance);
        }

        public ActorStats(IntStat health, IntStat meleeAttack, IntStat meleeDefense, IntStat rangedAttack,
            IntStat rangedDefense, IntStat magicAttack, IntStat magicDefense, DoubleStat criticalHitChance)
        {
            Health = health;
            MeleeAttack = meleeAttack;
            RangedAttack = rangedAttack;
            MagicAttack = magicAttack;
            MeleeDefense = meleeDefense;
            RangedDefense = rangedDefense;
            MagicDefense = magicDefense;
            CriticalHitChance = criticalHitChance;
        }

        public virtual IntStat Health { get; set; }
        public virtual IntStat MeleeAttack { get; set; }
        public virtual IntStat RangedAttack { get; set; }
        public virtual IntStat MagicAttack { get; set; }
        public virtual IntStat MeleeDefense { get; set; }
        public virtual IntStat RangedDefense { get; set; }
        public virtual IntStat MagicDefense { get; set; }
        public virtual DoubleStat CriticalHitChance { get; set; } // Configurable critical hit chance

        public static ActorStats operator +(ActorStats a, ActorStats b)
        {
            return new ActorStats(health: a.Health + b.Health, meleeAttack: a.MeleeAttack + b.MeleeAttack,
                meleeDefense: a.MeleeDefense + b.MeleeDefense, rangedAttack: a.RangedAttack + b.RangedAttack,
                rangedDefense: a.RangedDefense + b.RangedDefense, magicAttack: a.MagicAttack + b.MagicAttack,
                magicDefense: a.MagicDefense + b.MagicDefense, criticalHitChance: a.CriticalHitChance + b.CriticalHitChance);
        }

        public static ActorStats operator -(ActorStats a, ActorStats b)
        {
            return new ActorStats(health: a.Health - b.Health, meleeAttack: a.MeleeAttack - b.MeleeAttack,
                meleeDefense: a.MeleeDefense - b.MeleeDefense, rangedAttack: a.RangedAttack - b.RangedAttack,
                rangedDefense: a.RangedDefense - b.RangedDefense, magicAttack: a.MagicAttack - b.MagicAttack,
                magicDefense: a.MagicDefense - b.MagicDefense, criticalHitChance: a.CriticalHitChance - b.CriticalHitChance);
        }

        public static ActorStats operator *(ActorStats a, ActorStats b)
        {
            return new ActorStats(health: a.Health * b.Health, meleeAttack: a.MeleeAttack * b.MeleeAttack,
                meleeDefense: a.MeleeDefense * b.MeleeDefense, rangedAttack: a.RangedAttack * b.RangedAttack,
                rangedDefense: a.RangedDefense * b.RangedDefense, magicAttack: a.MagicAttack * b.MagicAttack,
                magicDefense: a.MagicDefense * b.MagicDefense, criticalHitChance: a.CriticalHitChance * b.CriticalHitChance);
        }

        public static ActorStats operator /(ActorStats a, ActorStats b)
        {
            return new ActorStats(health: a.Health / b.Health, meleeAttack: a.MeleeAttack / b.MeleeAttack,
                meleeDefense: a.MeleeDefense / b.MeleeDefense, rangedAttack: a.RangedAttack / b.RangedAttack,
                rangedDefense: a.RangedDefense / b.RangedDefense, magicAttack: a.MagicAttack / b.MagicAttack,
                magicDefense: a.MagicDefense / b.MagicDefense, criticalHitChance: a.CriticalHitChance / b.CriticalHitChance);
        }

        public void AddModifier(ActorStats modifier)
        {
            Health.AddModifier(modifier: modifier.Health.CurrentValue);
            MeleeAttack.AddModifier(modifier: modifier.MeleeAttack.CurrentValue);
            RangedAttack.AddModifier(modifier: modifier.RangedAttack.CurrentValue);
            MagicAttack.AddModifier(modifier: modifier.MagicAttack.CurrentValue);
            MeleeDefense.AddModifier(modifier: modifier.MeleeDefense.CurrentValue);
            RangedDefense.AddModifier(modifier: modifier.RangedDefense.CurrentValue);
            MagicDefense.AddModifier(modifier: modifier.MagicDefense.CurrentValue);
            CriticalHitChance.AddModifier(modifier: modifier.CriticalHitChance.CurrentValue);
        }

        public void RemoveModifier(ActorStats modifier)
        {
            Health.RemoveModifier(modifier: modifier.Health.CurrentValue);
            MeleeAttack.RemoveModifier(modifier: modifier.MeleeAttack.CurrentValue);
            RangedAttack.RemoveModifier(modifier: modifier.RangedAttack.CurrentValue);
            MagicAttack.RemoveModifier(modifier: modifier.MagicAttack.CurrentValue);
            MeleeDefense.RemoveModifier(modifier: modifier.MeleeDefense.CurrentValue);
            RangedDefense.RemoveModifier(modifier: modifier.RangedDefense.CurrentValue);
            MagicDefense.RemoveModifier(modifier: modifier.MagicDefense.CurrentValue);
            CriticalHitChance.RemoveModifier(modifier: modifier.CriticalHitChance.CurrentValue);
        }
    }
}