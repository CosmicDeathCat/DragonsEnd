using System;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Messages;
using DragonsEnd.Items.Status.Interfaces;

namespace DragonsEnd.Items.Status
{
    [Serializable]
    public class StatIncreaseItem : Item, IStatIncreaseItem
    {
        public StatIncreaseItem
        (
            string name,
            string description,
            long price,
            ItemType type,
            double healthIncreasePercentage = 0.0,
            double manaIncreasePercentage = 0.0,
            double staminaIncreasePercentage = 0.0,
            double meleeAttackIncreasePercentage = 0.0,
            double meleeDefenseIncreasePercentage = 0.0,
            double rangedAttackIncreasePercentage = 0.0,
            double rangedDefenseIncreasePercentage = 0.0,
            double magicAttackIncreasePercentage = 0.0,
            double magicDefenseIncreasePercentage = 0.0,
            double criticalHitChanceIncreasePercentage = 0.0,
            bool stackable = true,
            long quantity = 1,
            double dropRate = 1
        ) : base(name: name, description: description, price: price, type: type,
            stackable: stackable,
            quantity: quantity, dropRate: dropRate)
        {
            HealthIncreasePercentage = healthIncreasePercentage;
            ManaIncreasePercentage = manaIncreasePercentage;
            StaminaIncreasePercentage = staminaIncreasePercentage;
            MeleeAttackIncreasePercentage = meleeAttackIncreasePercentage;
            MeleeDefenseIncreasePercentage = meleeDefenseIncreasePercentage;
            RangedAttackIncreasePercentage = rangedAttackIncreasePercentage;
            RangedDefenseIncreasePercentage = rangedDefenseIncreasePercentage;
            MagicAttackIncreasePercentage = magicAttackIncreasePercentage;
            MagicDefenseIncreasePercentage = magicDefenseIncreasePercentage;
            CriticalHitChanceIncreasePercentage = criticalHitChanceIncreasePercentage;
        }

        public virtual double HealthIncreasePercentage { get; set; }
        public virtual double ManaIncreasePercentage { get; set; }
        public virtual double StaminaIncreasePercentage { get; set; }
        public virtual double MeleeAttackIncreasePercentage { get; set; }
        public virtual double MeleeDefenseIncreasePercentage { get; set; }
        public virtual double RangedAttackIncreasePercentage { get; set; }
        public virtual double RangedDefenseIncreasePercentage { get; set; }
        public virtual double MagicAttackIncreasePercentage { get; set; }
        public virtual double MagicDefenseIncreasePercentage { get; set; }
        public virtual double CriticalHitChanceIncreasePercentage { get; set; }

        public override void Use(IActor? source, IActor? target)
        {
            if (target != null)
            {
                target.ActorStats.Health.BaseValue += (int)Math.Round(value: target.ActorStats.Health.MaxValue * HealthIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.Mana.BaseValue += (int)Math.Round(value: target.ActorStats.Mana.MaxValue * ManaIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.Stamina.BaseValue += (int)Math.Round(value: target.ActorStats.Stamina.MaxValue * StaminaIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.MeleeAttack.BaseValue += (int)Math.Round(value: target.ActorStats.MeleeAttack.MaxValue * MeleeAttackIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.MeleeDefense.BaseValue += (int)Math.Round(value: target.ActorStats.MeleeDefense.MaxValue * MeleeDefenseIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.RangedAttack.BaseValue += (int)Math.Round(value: target.ActorStats.RangedAttack.MaxValue * RangedAttackIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.RangedDefense.BaseValue +=
                    (int)Math.Round(value: target.ActorStats.RangedDefense.MaxValue * RangedDefenseIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.MagicAttack.BaseValue += (int)Math.Round(value: target.ActorStats.MagicAttack.MaxValue * MagicAttackIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.MagicDefense.BaseValue += (int)Math.Round(value: target.ActorStats.MagicDefense.MaxValue * MagicDefenseIncreasePercentage / 100, mode: MidpointRounding.AwayFromZero);
                target.ActorStats.CriticalHitChance.BaseValue += CriticalHitChanceIncreasePercentage;
            }

            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Items,
                message: new ItemMessage(item: this, source: source, target: target));
        }

        public override IItem Copy()
        {
            return new StatIncreaseItem(name: Name, description: Description, price: Price, type: Type,
                healthIncreasePercentage: HealthIncreasePercentage, manaIncreasePercentage: ManaIncreasePercentage,
                staminaIncreasePercentage: StaminaIncreasePercentage, meleeAttackIncreasePercentage: MeleeAttackIncreasePercentage,
                meleeDefenseIncreasePercentage: MeleeDefenseIncreasePercentage, rangedAttackIncreasePercentage: RangedAttackIncreasePercentage,
                rangedDefenseIncreasePercentage: RangedDefenseIncreasePercentage, magicAttackIncreasePercentage: MagicAttackIncreasePercentage,
                magicDefenseIncreasePercentage: MagicDefenseIncreasePercentage, criticalHitChanceIncreasePercentage: CriticalHitChanceIncreasePercentage,
                stackable: Stackable, quantity: Quantity);
        }
    }
}