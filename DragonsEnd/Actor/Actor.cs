using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Actor.Messages;
using DragonsEnd.Enums;
using DragonsEnd.Items.Currency;
using DragonsEnd.Items.Currency.Extensions;
using DragonsEnd.Items.Drops;
using DragonsEnd.Items.Drops.Interfaces;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Loot;
using DragonsEnd.Items.Messages;
using DragonsEnd.Leveling.Interfaces;
using DragonsEnd.Leveling.Messages;
using DragonsEnd.Skills;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Actor
{
    public class Actor : IActor
    {
        protected CombatStyle _combatStyle = CombatStyle.Melee;

        public Actor()
        {
            Name = "Actor";
            Equipment = new IEquipmentItem[Enum.GetNames(enumType: typeof(EquipmentSlot)).Length];
            Inventory = new List<IItem?>();
            DropItems = new List<IDropItem>();
            Leveling = new Leveling.Leveling(actor: this, name: "Level");
            ActorStats = new ActorStats(health: 100, meleeAttack: 1, meleeDefense: 1, rangedAttack: 1, rangedDefense: 1, magicAttack: 1,
                magicDefense: 1);
            ActorSkills = new ActorSkills(actor: this);
            MessageSystem.MessageManager.RegisterForChannel<ItemMessage>(channel: MessageChannels.Items, handler: ItemMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
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
            ActorSkills = new ActorSkills(actor: this);
            _combatStyle = combatStyle;
            DamageMultiplier = new DoubleStat(baseValue: damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(baseValue: criticalHitMultiplier);
            IsAlive = true;
            Leveling = new Leveling.Leveling(actor: this, name: "Level", maxLevel: 100, level: level, experience: experience);
            Gold = new GoldCurrency(quantity: gold);
            if (inventory != null)
            {
                Inventory.AddRange(collection: inventory);
            }
            else
            {
                Inventory = new List<IItem?>();
            }

            if (equipment != null)
            {
                foreach (var item in equipment.Where(predicate: x => x != null))
                {
                    item.Equip(source: this, target: this);
                }
            }
            else
            {
                Equipment = new IEquipmentItem[Enum.GetNames(enumType: typeof(EquipmentSlot)).Length];
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

                    DropItems.Add(item: new DropItem(item: item, dropRate: item.DropRate));
                }

                if (equipment != null)
                {
                    foreach (var item in equipment)
                    {
                        DropItems.Add(item: new DropItem(item: item, dropRate: item.DropRate));
                    }
                }
            }

            MessageSystem.MessageManager.RegisterForChannel<ItemMessage>(channel: MessageChannels.Items, handler: ItemMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }

        public virtual string Name { get; set; }
        public virtual Gender Gender { get; set; }
        public Vector2 Position { get; set; }
        public virtual ILeveling Leveling { get; set; }
        public virtual DoubleStat DamageMultiplier { get; set; } = new(baseValue: 1.00);
        public virtual DoubleStat DamageReductionMultiplier { get; set; } = new(baseValue: 1.00);
        public virtual DoubleStat CriticalHitMultiplier { get; set; } = new(baseValue: 1.5);
        public virtual CharacterClassType CharacterClass { get; set; }

        public virtual IEquipmentItem?[] Equipment { get; set; } = new IEquipmentItem[Enum.GetNames(enumType: typeof(EquipmentSlot)).Length - 1];

        public virtual List<IItem?> Inventory { get; set; } = new();
        public GoldCurrency Gold { get; set; } = new(quantity: 0);
        public virtual IActor? Target { get; set; }
        public virtual ActorStats ActorStats { get; set; }
        public virtual IActorSkills ActorSkills { get; set; }

        public List<IDropItem> DropItems { get; set; } = new();

        public CombatStyle CombatStyle
        {
            get
            {
                var weapons = GetWeapons();
                weapons = weapons.Where(predicate: x => x != null).ToArray();
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
            return Equipment.Where(predicate: x => x is IWeaponItem).Cast<IWeaponItem>().Distinct().ToArray();
        }

        public virtual IArmorItem?[] GetArmor()
        {
            return Equipment.Where(predicate: x => x is IArmorItem).Cast<IArmorItem>().Distinct().ToArray();
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
                            Inventory.Remove(item: data.Item);
                        }

                        break;
                }
            }

            if (data.Target == this)
            {
                var equipmentItem = data.Item as IEquipmentItem;
                if (equipmentItem != null)
                {
                    equipmentItem.Equip(source: data.Source, target: this);
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

            Console.WriteLine(value: $"{Name} has increased stats for level {level}.");
            Console.WriteLine(value: $"{Name} new base stats:");
            Console.WriteLine(value: $"Health: {oldHealth} -> {ActorStats.Health.BaseValue}");
            Console.WriteLine(value: $"Melee Attack: {oldMeleeAttack} -> {ActorStats.MeleeAttack.BaseValue}");
            Console.WriteLine(value: $"Ranged Attack: {oldRangedAttack} -> {ActorStats.RangedAttack.BaseValue}");
            Console.WriteLine(value: $"Magic Attack: {oldMagicAttack} -> {ActorStats.MagicAttack.BaseValue}");
            Console.WriteLine(value: $"Melee Defense: {oldMeleeDefense} -> {ActorStats.MeleeDefense.BaseValue}");
            Console.WriteLine(value: $"Ranged Defense: {oldRangedDefense} -> {ActorStats.RangedDefense.BaseValue}");
            Console.WriteLine(value: $"Magic Defense: {oldMagicDefense} -> {ActorStats.MagicDefense.BaseValue}");
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

            Console.WriteLine(value: $"{Name} has decreased stats for level {level}.");
            Console.WriteLine(value: $"{Name} new base stats:");
            Console.WriteLine(value: $"Health: {oldHealth} -> {ActorStats.Health.BaseValue}");
            Console.WriteLine(value: $"Melee Attack: {oldMeleeAttack} -> {ActorStats.MeleeAttack.BaseValue}");
            Console.WriteLine(value: $"Ranged Attack: {oldRangedAttack} -> {ActorStats.RangedAttack.BaseValue}");
            Console.WriteLine(value: $"Magic Attack: {oldMagicAttack} -> {ActorStats.MagicAttack.BaseValue}");
            Console.WriteLine(value: $"Melee Defense: {oldMeleeDefense} -> {ActorStats.MeleeDefense.BaseValue}");
            Console.WriteLine(value: $"Ranged Defense: {oldRangedDefense} -> {ActorStats.RangedDefense.BaseValue}");
            Console.WriteLine(value: $"Magic Defense: {oldMagicDefense} -> {ActorStats.MagicDefense.BaseValue}");
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
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source: source, target: target,
                        attackValue: source.ActorStats.MeleeAttack.CurrentValue,
                        defenseValue: target.ActorStats.MeleeDefense.CurrentValue);
                    break;
                case CombatStyle.Ranged:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source: source, target: target,
                        attackValue: source.ActorStats.RangedAttack.CurrentValue,
                        defenseValue: target.ActorStats.RangedDefense.CurrentValue);
                    break;
                case CombatStyle.Magic:
                    (hit, blocked, killed, damage, isCriticalHit) = HandleAttack(source: source, target: target,
                        attackValue: source.ActorStats.MagicAttack.CurrentValue,
                        defenseValue: target.ActorStats.MagicDefense.CurrentValue);
                    break;
                case CombatStyle.Hybrid:
                    var avgAttack =
                        (int)Math.Round(
                            value: (source.ActorStats.MeleeAttack.CurrentValue + source.ActorStats.RangedAttack.CurrentValue +
                                    source.ActorStats.MagicAttack.CurrentValue) / 3.0, mode: MidpointRounding.AwayFromZero);
                    var avgDefense =
                        (int)Math.Round(
                            value: (target.ActorStats.MeleeDefense.CurrentValue + target.ActorStats.RangedAttack.CurrentValue +
                                    target.ActorStats.MagicDefense.CurrentValue) / 3.0, mode: MidpointRounding.AwayFromZero);
                    (hit, blocked, killed, damage, isCriticalHit) =
                        HandleAttack(source: source, target: target, attackValue: avgAttack, defenseValue: avgDefense);
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
            var attackRoll =
                rnd.Next(minValue: 1, maxValue: Math.Max(val1: 2, val2: attackValue - defenseValue + 1)); // Adjusted to ensure at least 1

            if (attackRoll > 0)
            {
                hit = true;
                var maxSourceDamage = Math.Round(value: attackValue * source.DamageMultiplier.CurrentValue,
                    mode: MidpointRounding.AwayFromZero);
                var maxTargetDefense = Math.Round(value: defenseValue * target.DamageReductionMultiplier.CurrentValue,
                    mode: MidpointRounding.AwayFromZero);
                blocked = maxSourceDamage - maxTargetDefense <= 0;
                var roundedMinDamage = Math.Round(value: Math.Clamp(value: maxSourceDamage - maxTargetDefense, min: 1, max: maxSourceDamage),
                    mode: MidpointRounding.AwayFromZero);
                var roundedMaxDamage = Math.Round(value: Math.Clamp(value: maxSourceDamage, min: roundedMinDamage, max: maxSourceDamage),
                    mode: MidpointRounding.AwayFromZero);
                var randomDamage = 0;
                if (blocked)
                {
                    randomDamage = rnd.Next(minValue: 0, maxValue: (int)roundedMinDamage + 1);
                }
                else
                {
                    randomDamage = rnd.Next(minValue: (int)roundedMinDamage, maxValue: (int)roundedMaxDamage + 1);
                }

                damage = Math.Clamp(value: randomDamage, min: 0, max: (int)maxSourceDamage);

                if (isCriticalHit)
                {
                    damage = (int)(damage * CriticalHitMultiplier.CurrentValue); // Apply critical hit multiplier
                    Console.WriteLine(
                        value: $"{source.Name} lands a critical hit on {target.Name} for {damage} {source.CombatStyle} damage!");
                }
                else
                {
                    if (blocked)
                    {
                        if (damage <= 0)
                        {
                            Console.WriteLine(
                                value: $"{source.Name} attacks {target.Name}, but the attack is completely blocked!");
                        }
                        else
                        {
                            Console.WriteLine(
                                value:
                                $"{source.Name} attacks {target.Name}, but {target.Name} blocks most of the damage. However, {source.Name} still manages to deal {damage} {source.CombatStyle} damage.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            value: $"{source.Name} attacks {target.Name} for {damage} {source.CombatStyle} damage.");
                    }
                }

                if (damage > 0)
                {
                    target.TakeDamage(sourceActor: source, damage: damage);

                    if (target.ActorStats.Health.CurrentValue <= 0)
                    {
                        killed = true;
                        return (hit, blocked, killed, damage, isCriticalHit);
                    }
                }
            }
            else
            {
                Console.WriteLine(value: $"{source.Name} attacks {target.Name}, but misses!");
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
                lootedObject: this,
                minItemAmountDrop: minItemAmountDrop,
                maxItemAmountDrop: maxItemAmountDrop,
                minGold: minGold,
                maxGold: maxGold,
                minExperience: minExperience,
                maxExperience: maxExperience,
                specificLootableItems: specificLootableItems);
            var lootContainer = new LootContainer(gold: loot.gold, experience: loot.experience, items: loot.items.ToArray());
            return lootContainer;
        }

        public virtual void TakeDamage(IActor sourceActor, int damage)
        {
            Target = sourceActor;
            Console.WriteLine(value: $"{Name} takes {damage} {sourceActor.CombatStyle.ToString()} damage.");
            ActorStats.Health.CurrentValue -= damage;
            if (ActorStats.Health.CurrentValue <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            IsAlive = false;
            Console.WriteLine(value: $"{Name} has died!");
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat, message: new ActorDeathMessage(source: Target, target: this));
        }

        public virtual IActor Copy()
        {
            return new Actor(name: Name, gender: Gender, characterClass: CharacterClass, actorStats: ActorStats, combatStyle: CombatStyle,
                damageMultiplier: DamageMultiplier.BaseValue,
                damageReductionMultiplier: DamageReductionMultiplier.BaseValue,
                criticalHitMultiplier: CriticalHitMultiplier.BaseValue, level: Leveling.CurrentLevel, experience: Leveling.Experience,
                gold: Gold.CurrentValue,
                equipment: Equipment?.ToArray()!,
                inventory: Inventory?.ToArray()!, dropItems: DropItems?.ToArray()!);
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
            Leveling.GainExperience(amount: loot.Experience);
            Gold.Add(otherCurrency: loot.Gold);
            Inventory.AddRange(collection: loot.Items);
            var lootItemsDisplay = loot.Items.Count > 0
                ? $"{loot.Items.Count} Items from {data.Target.Name}\n" +
                  string.Join(separator: ", \n", values: loot.Items.Select(selector: x => "Looted " + x.Name))
                : "No Items";
            Console.WriteLine(value: $"{Name} has looted {loot.Gold} and {lootItemsDisplay}");
        }

        ~Actor()
        {
            MessageSystem.MessageManager.UnregisterForChannel<ItemMessage>(channel: MessageChannels.Items, handler: ItemMessageHandler);
            MessageSystem.MessageManager.UnregisterForChannel<LevelingMessage>(channel: MessageChannels.Level,
                handler: LevelingMessageHandler);
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

            if (data.SenderIdentity.ID.Equals(g: ActorSkills.CookingSkill.Leveling.ID))
            {
                // if (data.Type == LevelingType.GainLevel)
                // {
                //     Console.WriteLine($"{Name} has gained a level in Cooking. {Name} is now level {data.Level}.");
                // }
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
                return;
            }


            switch (data.Type)
            {
                case LevelingType.GainExperience:
                    Console.WriteLine(
                        value:
                        $"{Name} gained {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                    break;
                case LevelingType.LoseExperience:
                    Console.WriteLine(
                        value:
                        $"{Name} lost {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                    break;
                case LevelingType.SetExperience:
                    Console.WriteLine(value: $"{Name} set experience to {data.Experience}.");
                    break;
                case LevelingType.GainLevel:
                    Console.WriteLine(value: $"{Name} gained a level. {Name} is now level {data.Level}.");
                    IncreaseStatsForLevel(level: data.Level);
                    break;
                case LevelingType.LoseLevel:
                    Console.WriteLine(value: $"{Name} lost a level. {Name} is now level {data.Level}.");
                    DecreaseStatsForLevel(level: data.Level);
                    break;
                case LevelingType.SetLevel:
                    Console.WriteLine(value: $"{Name} set level to {data.Level}.");
                    break;
            }
        }
    }
}