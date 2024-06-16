using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Drops;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Loot;
using DragonsEnd.Messaging.Messages;
using DragonsEnd.Skills;
using DragonsEnd.Skills.Combat;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Skills.NonCombat;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Leveling;
using DragonsEnd.Stats.Leveling.Interfaces;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Actor
{
    public class Actor : IActor
    {
        protected CombatStyle _combatStyle = CombatStyle.Melee;

        public Actor()
        {
            Name = "Actor";
            Equipment = new IEquipmentItem[Enum.GetNames(typeof(EquipmentSlot)).Length];
            Inventory = new List<IItem?>();
            DropItems = new List<IDropItem>();
            Leveling = new Leveling(this);
            ActorStats = new ActorStats(100, 1, 1, 1, 1, 1, 1);
            ActorSkills = new ActorSkills(this);
            MessageSystem.MessageManager.RegisterForChannel<ItemMessage>(MessageChannels.Items, ItemMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(MessageChannels.Level, LevelingMessageHandler);
        }

        public Actor(
            string name,
            Gender gender,
            CharacterClassType characterClass,
            ActorStats actorStats,
            CombatStyle combatStyle,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            int level = -1,
            long experience = -1L,
            long gold = 0L,
            IEquipmentItem[]? equipment = null,
            IItem[]? inventory = null,
            params IDropItem[] dropItems)
        {
            Name = name;
            Gender = gender;
            CharacterClass = characterClass;
            ActorStats = actorStats;
            ActorSkills = new ActorSkills(this);
            _combatStyle = combatStyle;
            DamageMultiplier = new DoubleStat(damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(criticalHitMultiplier);
            IsAlive = true;
            Leveling = new Leveling(this, 100, level, experience);
            Gold = new GoldCurrency(gold);
            if (inventory != null)
            {
                Inventory.AddRange(inventory);
            }
            else
            {
                Inventory = new List<IItem?>();
            }

            if (equipment != null)
            {
                foreach (var item in equipment.Where(x => x != null))
                {
                    item.Equip(this, this);
                }
            }
            else
            {
                Equipment = new IEquipmentItem[Enum.GetNames(typeof(EquipmentSlot)).Length];
            }

            if (dropItems.Length > 0)
            {
                DropItems = dropItems.ToList();
            }
            else
            {
                // Default to using the provided inventory and equipment as drop items with default drop rates
                foreach (var item in Inventory)
                {
                    if (item == null)
                    {
                        continue;
                    }
                    DropItems.Add(new DropItem(item, item.DropRate));
                }

                if (equipment != null)
                {
                    foreach (var item in equipment)
                    {
                        DropItems.Add(new DropItem(item, item.DropRate));
                    }
                }
            }

            MessageSystem.MessageManager.RegisterForChannel<ItemMessage>(MessageChannels.Items, ItemMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(MessageChannels.Level, LevelingMessageHandler);
        }

        public virtual string Name { get; set; }
        public virtual Gender Gender { get; set; }
        public Vector2 Position { get; set; }
        public virtual ILeveling Leveling { get; set; }
        public virtual DoubleStat DamageMultiplier { get; set; } = new DoubleStat(1.00);
        public virtual DoubleStat DamageReductionMultiplier { get; set; } = new DoubleStat(1.00);
        public virtual DoubleStat CriticalHitMultiplier { get; set; } = new DoubleStat(1.5);
        public virtual CharacterClassType CharacterClass { get; set; }

        public virtual IEquipmentItem?[] Equipment { get; set; } = new IEquipmentItem[Enum.GetNames(typeof(EquipmentSlot)).Length - 1];

        public virtual List<IItem?> Inventory { get; set; } = new List<IItem?>();
        public GoldCurrency Gold { get; set; } = new GoldCurrency(0);
        public virtual IActor? Target { get; set; }
        public virtual ActorStats ActorStats { get; set; }
        public virtual IActorSkills ActorSkills { get; set; }
        
        public List<IDropItem> DropItems { get; set; } = new List<IDropItem>();

        public CombatStyle CombatStyle
        {
            get
            {
                var weapons = GetWeapons();
                weapons = weapons.Where(x => x != null).ToArray();
                return weapons.Length > 0 ? weapons[0]!.CombatStyle : _combatStyle;
            }
            set => _combatStyle = value;
        }

        public virtual bool IsAlive
        {
            get => ActorStats.Health.CurrentValue > 0;
            set
            {
                if (value && ActorStats.Health.CurrentValue <= 0)
                {
                    ActorStats.Health.CurrentValue = ActorStats.Health.MaxValue;
                }
                else if (!value && ActorStats.Health.CurrentValue > 0)
                {
                    Die();
                }
            }
        }

        public virtual IWeaponItem?[] GetWeapons()
        {
            return Equipment.Where(x => x is IWeaponItem).Cast<IWeaponItem>().Distinct().ToArray();
        }

        public virtual IArmorItem?[] GetArmor()
        {
            return Equipment.Where(x => x is IArmorItem).Cast<IArmorItem>().Distinct().ToArray();
        }

        public virtual void ItemMessageHandler(IMessageEnvelope message)
        {
            if (!message.Message<ItemMessage>().HasValue)
            {
                return;
            }

            var data = message.Message<ItemMessage>().GetValueOrDefault();
            if (data.Source == this)
            {
                switch (data.Item.Type)
                {
                    case ItemType.NonConsumable:
                        break;
                    case ItemType.Consumable:
                        data.Item.Quantity--;
                        if (data.Item.Quantity <= 0)
                        {
                            Inventory.Remove(data.Item);
                        }

                        break;
                }
            }

            if (data.Target == this)
            {
                var equipmentItem = data.Item as IEquipmentItem;
                if (equipmentItem != null)
                {
                    equipmentItem.Equip(data.Source, this);
                }
            }
        }

        public virtual void IncreaseStatsForLevel(int level)
        {
            var statIncrease = level - 1; // Linear increase

            var oldHealth = ActorStats.Health.BaseValue;
            var oldMeleeAttack = ActorStats.MeleeAttack.BaseValue;
            var oldRangedAttack = ActorStats.RangedAttack.BaseValue;
            var oldMagicAttack = ActorStats.MagicAttack.BaseValue;
            var oldMeleeDefense = ActorStats.MeleeDefense.BaseValue;
            var oldRangedDefense = ActorStats.RangedDefense.BaseValue;
            var oldMagicDefense = ActorStats.MagicDefense.BaseValue;

            ActorStats.Health.BaseValue += statIncrease;
            ActorStats.MeleeAttack.BaseValue += statIncrease;
            ActorStats.RangedAttack.BaseValue += statIncrease;
            ActorStats.MagicAttack.BaseValue += statIncrease;
            ActorStats.MeleeDefense.BaseValue += statIncrease;
            ActorStats.RangedDefense.BaseValue += statIncrease;
            ActorStats.MagicDefense.BaseValue += statIncrease;

            Console.WriteLine($"{Name} has increased stats for level {level}.");
            Console.WriteLine($"{Name} new base stats:");
            Console.WriteLine($"Health: {oldHealth} -> {ActorStats.Health.BaseValue}");
            Console.WriteLine($"Melee Attack: {oldMeleeAttack} -> {ActorStats.MeleeAttack.BaseValue}");
            Console.WriteLine($"Ranged Attack: {oldRangedAttack} -> {ActorStats.RangedAttack.BaseValue}");
            Console.WriteLine($"Magic Attack: {oldMagicAttack} -> {ActorStats.MagicAttack.BaseValue}");
            Console.WriteLine($"Melee Defense: {oldMeleeDefense} -> {ActorStats.MeleeDefense.BaseValue}");
            Console.WriteLine($"Ranged Defense: {oldRangedDefense} -> {ActorStats.RangedDefense.BaseValue}");
            Console.WriteLine($"Magic Defense: {oldMagicDefense} -> {ActorStats.MagicDefense.BaseValue}");
        }

        public virtual void DecreaseStatsForLevel(int level)
        {
            var statDecrease = level - 1; // Linear decrease

            var oldHealth = ActorStats.Health.BaseValue;
            var oldMeleeAttack = ActorStats.MeleeAttack.BaseValue;
            var oldRangedAttack = ActorStats.RangedAttack.BaseValue;
            var oldMagicAttack = ActorStats.MagicAttack.BaseValue;
            var oldMeleeDefense = ActorStats.MeleeDefense.BaseValue;
            var oldRangedDefense = ActorStats.RangedDefense.BaseValue;
            var oldMagicDefense = ActorStats.MagicDefense.BaseValue;

            ActorStats.Health.BaseValue -= statDecrease;
            ActorStats.MeleeAttack.BaseValue -= statDecrease;
            ActorStats.RangedAttack.BaseValue -= statDecrease;
            ActorStats.MagicAttack.BaseValue -= statDecrease;
            ActorStats.MeleeDefense.BaseValue -= statDecrease;
            ActorStats.RangedDefense.BaseValue -= statDecrease;
            ActorStats.MagicDefense.BaseValue -= statDecrease;

            Console.WriteLine($"{Name} has decreased stats for level {level}.");
            Console.WriteLine($"{Name} new base stats:");
            Console.WriteLine($"Health: {oldHealth} -> {ActorStats.Health.BaseValue}");
            Console.WriteLine($"Melee Attack: {oldMeleeAttack} -> {ActorStats.MeleeAttack.BaseValue}");
            Console.WriteLine($"Ranged Attack: {oldRangedAttack} -> {ActorStats.RangedAttack.BaseValue}");
            Console.WriteLine($"Magic Attack: {oldMagicAttack} -> {ActorStats.MagicAttack.BaseValue}");
            Console.WriteLine($"Melee Defense: {oldMeleeDefense} -> {ActorStats.MeleeDefense.BaseValue}");
            Console.WriteLine($"Ranged Defense: {oldRangedDefense} -> {ActorStats.RangedDefense.BaseValue}");
            Console.WriteLine($"Magic Defense: {oldMagicDefense} -> {ActorStats.MagicDefense.BaseValue}");
        }

        public virtual (bool hasHit, bool isBlocked, bool hasKilled, int damage, bool isCriticalHit) Attack(
            IActor source,
            IActor target)
        {
            Target = target;

            var hit = false;
            var blocked = false;
            var killed = false;
            var damage = 0;
            var isCriticalHit = false;

            switch (CombatStyle)
            {
                case CombatStyle.Melee:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target,
                        source.ActorStats.MeleeAttack.CurrentValue,
                        target.ActorStats.MeleeDefense.CurrentValue);
                    break;
                case CombatStyle.Ranged:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target,
                        source.ActorStats.RangedAttack.CurrentValue,
                        target.ActorStats.RangedDefense.CurrentValue);
                    break;
                case CombatStyle.Magic:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target,
                        source.ActorStats.MagicAttack.CurrentValue,
                        target.ActorStats.MagicDefense.CurrentValue);
                    break;
                case CombatStyle.Hybrid:
                    var avgAttack =
                        (int)Math.Round(
                            (source.ActorStats.MeleeAttack.CurrentValue + source.ActorStats.RangedAttack.CurrentValue +
                             source.ActorStats.MagicAttack.CurrentValue) / 3.0, MidpointRounding.AwayFromZero);
                    var avgDefense =
                        (int)Math.Round(
                            (target.ActorStats.MeleeDefense.CurrentValue + target.ActorStats.RangedAttack.CurrentValue +
                             target.ActorStats.MagicDefense.CurrentValue) / 3.0, MidpointRounding.AwayFromZero);
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source, target, avgAttack, avgDefense);
                    break;
            }

            return (hit, blocked, killed, damage, isCriticalHit);
        }

        public virtual (bool hit, bool blocked, bool killed, int damage, bool isCriticalHit) HandleAttack(IActor source,
            IActor target,
            int attackValue, int defenseValue)
        {
            Target = target;

            var rnd = new Random();
            var hit = false;
            var blocked = false;
            var killed = false;
            var damage = 0;
            var critRoll = rnd.NextDouble();
            var isCriticalHit = critRoll < source.ActorStats.CriticalHitChance.CurrentValue;

            // Ensure the roll is between 1 and the effective attack value minus defense value plus 1
            var attackRoll = rnd.Next(1, Math.Max(2, attackValue - defenseValue + 1)); // Adjusted to ensure at least 1

            if (attackRoll > 0)
            {
                hit = true;
                var maxSourceDamage = Math.Round(attackValue * source.DamageMultiplier.CurrentValue,
                    MidpointRounding.AwayFromZero);
                var maxTargetDefense = Math.Round(defenseValue * target.DamageReductionMultiplier.CurrentValue,
                    MidpointRounding.AwayFromZero);
                blocked = maxSourceDamage - maxTargetDefense <= 0;
                var roundedMinDamage = Math.Round(Math.Clamp(maxSourceDamage - maxTargetDefense, 1, maxSourceDamage),
                    MidpointRounding.AwayFromZero);
                var roundedMaxDamage = Math.Round(Math.Clamp(maxSourceDamage, roundedMinDamage, maxSourceDamage),
                    MidpointRounding.AwayFromZero);
                var randomDamage = 0;
                if (blocked)
                {
                    randomDamage = rnd.Next(0, (int)roundedMinDamage + 1);
                }
                else
                {
                    randomDamage = rnd.Next((int)roundedMinDamage, (int)roundedMaxDamage + 1);
                }

                damage = Math.Clamp(randomDamage, 0, (int)maxSourceDamage);

                if (isCriticalHit)
                {
                    damage = (int)(damage * CriticalHitMultiplier.CurrentValue); // Apply critical hit multiplier
                    Console.WriteLine(
                        $"{source.Name} lands a critical hit on {target.Name} for {damage} {source.CombatStyle} damage!");
                }
                else
                {
                    if (blocked)
                    {
                        if (damage <= 0)
                        {
                            Console.WriteLine(
                                $"{source.Name} attacks {target.Name}, but the attack is completely blocked!");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"{source.Name} attacks {target.Name}, but {target.Name} blocks most of the damage. However, {source.Name} still manages to deal {damage} {source.CombatStyle} damage.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{source.Name} attacks {target.Name} for {damage} {source.CombatStyle} damage.");
                    }
                }

                if (damage > 0)
                {
                    target.TakeDamage(source, damage);

                    if (target.ActorStats.Health.CurrentValue <= 0)
                    {
                        killed = true;
                        return (hit, blocked, killed, damage, isCriticalHit);
                    }
                }
            }
            else
            {
                Console.WriteLine($"{source.Name} attacks {target.Name}, but misses!");
            }

            return (hit, blocked, killed, damage, isCriticalHit);
        }


        public virtual LootContainer Loot(
            long minItemAmountDrop = -1L,
            long maxItemAmountDrop = -1L,
            long minGold = -1L,
            long maxGold = -1L,
            long minExperience = -1L,
            long maxExperience = -1L,
            params IDropItem[] specificLootableItems)
        {
            var loot = LootSystem.GenerateLoot(
                this,
                minItemAmountDrop,
                maxItemAmountDrop,
                minGold,
                maxGold,
                minExperience,
                maxExperience,
                specificLootableItems);
            var lootContainer = new LootContainer(loot.gold, loot.experience, loot.items.ToArray());
            return lootContainer;
        }

        public virtual void TakeDamage(IActor sourceActor, int damage)
        {
            Target = sourceActor;
            Console.WriteLine($"{Name} takes {damage} {sourceActor.CombatStyle.ToString()} damage.");
            ActorStats.Health.CurrentValue -= damage;
            if (ActorStats.Health.CurrentValue <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            IsAlive = false;
            Console.WriteLine($"{Name} has died!");
            MessageSystem.MessageManager.SendImmediate(MessageChannels.Combat, new ActorDeathMessage(Target, this));
        }

        public virtual IActor Copy()
        {
            return new Actor(Name, Gender, CharacterClass, ActorStats, CombatStyle, DamageMultiplier.BaseValue,
                DamageReductionMultiplier.BaseValue,
                CriticalHitMultiplier.BaseValue, Leveling.CurrentLevel, Leveling.Experience, Gold.CurrentValue,
                Equipment?.ToArray()!,
                Inventory?.ToArray()!, DropItems?.ToArray()!);
        }

        ~Actor()
        {
            MessageSystem.MessageManager.UnregisterForChannel<ItemMessage>(MessageChannels.Items, ItemMessageHandler);
            MessageSystem.MessageManager.UnregisterForChannel<LevelingMessage>(MessageChannels.Level,
                LevelingMessageHandler);
        }

        public virtual void LevelingMessageHandler(IMessageEnvelope message)
        {
            if (!message.Message<LevelingMessage>().HasValue)
            {
                return;
            }

            var data = message.Message<LevelingMessage>().GetValueOrDefault();
            if (data.Actor != this)
            {
                return;
            }

            if (data.Sender != Leveling)
            {
                if (data.Sender == ActorSkills.CookingSkill)
                {
                    if(data.Type == LevelingType.GainLevel)
                    {
                        Console.WriteLine($"{Name} has gained a level in Cooking. {Name} is now level {data.Level}.");
                    }
                    // switch (data.Type)
                    // {
                    //     case LevelingType.GainExperience:
                    //         Console.WriteLine(
                    //             $"{Name} gained {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                    //         break;
                    //     case LevelingType.LoseExperience:
                    //         Console.WriteLine(
                    //             $"{Name} lost {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                    //         break;
                    //     case LevelingType.SetExperience:
                    //         Console.WriteLine($"{Name} set experience to {data.Experience}.");
                    //         break;
                    //     case LevelingType.GainLevel:
                    //         Console.WriteLine($"{Name} gained a level. {Name} is now level {data.Level}.");
                    //         IncreaseStatsForLevel(data.Level);
                    //         break;
                    //     case LevelingType.LoseLevel:
                    //         Console.WriteLine($"{Name} lost a level. {Name} is now level {data.Level}.");
                    //         DecreaseStatsForLevel(data.Level);
                    //         break;
                    //     case LevelingType.SetLevel:
                    //         Console.WriteLine($"{Name} set level to {data.Level}.");
                    //         break;
                    // }
                }
                return;
            }

            switch (data.Type)
            {
                case LevelingType.GainExperience:
                    Console.WriteLine(
                        $"{Name} gained {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                    break;
                case LevelingType.LoseExperience:
                    Console.WriteLine(
                        $"{Name} lost {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                    break;
                case LevelingType.SetExperience:
                    Console.WriteLine($"{Name} set experience to {data.Experience}.");
                    break;
                case LevelingType.GainLevel:
                    Console.WriteLine($"{Name} gained a level. {Name} is now level {data.Level}.");
                    IncreaseStatsForLevel(data.Level);
                    break;
                case LevelingType.LoseLevel:
                    Console.WriteLine($"{Name} lost a level. {Name} is now level {data.Level}.");
                    DecreaseStatsForLevel(data.Level);
                    break;
                case LevelingType.SetLevel:
                    Console.WriteLine($"{Name} set level to {data.Level}.");
                    break;
            }
        }
        
        public virtual void ActorDeathMessageHandler(IMessageEnvelope message)
        {
            if (!message.Message<ActorDeathMessage>().HasValue)
            {
                return;
            }

            var data = message.Message<ActorDeathMessage>().GetValueOrDefault();
            if (data.Source != this)
            {
                return;
            }

            if (data.Target == this)
            {
                return;
            }

            var loot = data.Target.Loot();
            Leveling.GainExperience(loot.Experience);
            Gold.Add(loot.Gold);
            Inventory.AddRange(loot.Items);
            var lootItemsDisplay = loot.Items.Count > 0
                ? $"{loot.Items.Count} Items from {data.Target.Name}\n" +
                  string.Join(", \n", loot.Items.Select(x => "Looted " + x.Name))
                : "No Items";
            Console.WriteLine($"{Name} has looted {loot.Gold} and {lootItemsDisplay}");
        }
    }
}