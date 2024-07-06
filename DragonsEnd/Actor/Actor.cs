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
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Inventory;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Loot;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Items.Messages;
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
            ID = Guid.NewGuid();
            Equipment = new IEquipmentItem[Enum.GetNames(enumType: typeof(EquipmentSlot)).Length];
            Inventory = new Inventory();
            LootConfig = new LootConfig();
            LootContainer = new LootContainer();
            ActorStats = new ActorStats(health: 100, mana: 100, stamina: 100, meleeAttack: 1, meleeDefense: 1, rangedAttack: 1, rangedDefense: 1, magicAttack: 1,
                magicDefense: 1);
            ActorSkills = new ActorSkills(actor: this);
            MessageSystem.MessageManager.RegisterForChannel<ItemMessage>(channel: MessageChannels.Items, handler: ItemMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }

        public Actor
        (
            string name,
            Gender gender,
            CharacterClassType characterClass,
            ActorStats actorStats,
            IActorSkills? actorSkills,
            CombatStyle combatStyle,
            double damageMultiplier = 1.00,
            double damageReductionMultiplier = 1.00,
            double criticalHitMultiplier = 2.00,
            IEquipmentItem[]? equipment = null,
            IInventory? inventory = null,
            ILootConfig? lootConfig = null,
            ILootContainer? lootContainer = null
        )
        {
            Name = name;
            ID = Guid.NewGuid();
            Gender = gender;
            CharacterClass = characterClass;
            ActorStats = actorStats;
            ActorSkills = actorSkills;
            ActorSkills?.UpdateActor(actor: this);
            _combatStyle = combatStyle;
            DamageMultiplier = new DoubleStat(baseValue: damageMultiplier);
            DamageReductionMultiplier = new DoubleStat(baseValue: damageReductionMultiplier);
            CriticalHitMultiplier = new DoubleStat(baseValue: criticalHitMultiplier);
            IsAlive = true;
            Inventory = inventory;
            LootConfig = lootConfig;
            LootContainer = lootContainer;

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

            if (LootContainer != null)
            {
                if (LootContainer?.Items.Count > 0)
                {
                }
                else
                {
                    // Default to using the provided inventory and equipment as drop items with default drop rates
                    foreach (var item in Inventory.Items)
                    {
                        if (item == null)
                        {
                            continue;
                        }

                        LootContainer?.Items.Add(item: new Item(item: item));
                    }

                    if (equipment != null)
                    {
                        foreach (var item in equipment)
                        {
                            lootContainer?.Items.Add(item: new Item(item: item));
                        }
                    }
                }
            }


            MessageSystem.MessageManager.RegisterForChannel<ItemMessage>(channel: MessageChannels.Items, handler: ItemMessageHandler);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }

        public virtual string Name { get; set; }
        public virtual Guid ID { get; set; }
        public virtual Gender Gender { get; set; }
        public Vector2 Position { get; set; }

        public int CombatLevel => (ActorSkills.MeleeSkill.Leveling.CurrentLevel +
                                   ActorSkills.RangedSkill.Leveling.CurrentLevel +
                                   ActorSkills.MagicSkill.Leveling.CurrentLevel) / 3;

        public virtual int Initiative { get; set; }
        public virtual DoubleStat DamageMultiplier { get; set; } = new(baseValue: 1.00);
        public virtual DoubleStat DamageReductionMultiplier { get; set; } = new(baseValue: 1.00);
        public virtual DoubleStat CriticalHitMultiplier { get; set; } = new(baseValue: 1.5);
        public virtual CharacterClassType CharacterClass { get; set; }

        public virtual IEquipmentItem?[] Equipment { get; set; } = new IEquipmentItem[Enum.GetNames(enumType: typeof(EquipmentSlot)).Length];
        public virtual IInventory? Inventory { get; set; }
        public virtual IActor? Target { get; set; }
        public virtual ActorStats? ActorStats { get; set; }
        public virtual IActorSkills? ActorSkills { get; set; }

        public virtual ILootContainer? LootContainer { get; set; }
        public virtual ILootConfig? LootConfig { get; set; }

        public bool HasAlreadyBeenLooted { get; set; }


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

        public int TurnCount { get; set; }

        public virtual bool IsAlive
        {
            get => ActorStats?.Health.CurrentValue > 0;
            set
            {
                if (value && ActorStats?.Health.CurrentValue <= 0)
                {
                    ActorStats.Health.CurrentValue = ActorStats.Health.MaxValue;
                }
                else if (!value && ActorStats?.Health.CurrentValue > 0)
                {
                    Die();
                }
            }
        }

        public void ResetTurns()
        {
            TurnCount = 0;
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
                            Inventory.Items.Remove(item: data.Item);
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

            var oldHealth = ActorStats?.Health.BaseValue;
            var oldMeleeAttack = ActorStats?.MeleeAttack.BaseValue;
            var oldRangedAttack = ActorStats?.RangedAttack.BaseValue;
            var oldMagicAttack = ActorStats?.MagicAttack.BaseValue;
            var oldMeleeDefense = ActorStats?.MeleeDefense.BaseValue;
            var oldRangedDefense = ActorStats?.RangedDefense.BaseValue;
            var oldMagicDefense = ActorStats?.MagicDefense.BaseValue;

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

        public virtual (bool hasHit, bool isBlocked, bool hasKilled, int damage, bool isCriticalHit) Attack
        (
            IActor source,
            IActor target
        )
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

        public virtual (bool hit, bool blocked, bool killed, int damage, bool isCriticalHit) HandleAttack
        (
            IActor source,
            IActor target,
            int attackValue,
            int defenseValue
        )
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

        public virtual int RollInitiative()
        {
            var rnd = new Random();
            Initiative = rnd.Next(minValue: 1, maxValue: 21);
            return Initiative;
        }

        public virtual bool TakeTurn(ICombatContext combatContext, List<IActor> targets, List<IActor> allies)
        {
            if (IsAlive && targets.Any(predicate: a => a.IsAlive))
            {
                Console.WriteLine(value: $"{Name} is taking their turn.");
                TurnCount++;
                var target = SelectRandomTarget(targets: targets);
                if (target == null)
                {
                    return false;
                }

                var attackResult = Attack(source: this, target: target);
                return true;
            }

            return false;
        }

        public virtual IActor? SelectRandomTarget(List<IActor> targets)
        {
            var rnd = new Random();
            var aliveTargets = targets.Where(predicate: a => a.IsAlive).ToList();
            if (aliveTargets.Count <= 0)
            {
                return null;
            }

            return aliveTargets[index: rnd.Next(maxValue: aliveTargets.Count)];
        }

        public ILootContainer? Loot(ILootConfig? lootConfig = null)
        {
            if(LootContainer != null)
            {
                return LootContainer;
            }
            
            var loot = LootSystem.GenerateLoot(lootedObject: this, lootConfig: lootConfig);
            LootContainer = new LootContainer(gold: loot.gold, combatExperience: loot.combatExperience, experiences: loot.skillExperiences, items: loot.items.ToArray());
            HasAlreadyBeenLooted = true;
            return LootContainer;
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
            return new Actor(name: Name, gender: Gender, characterClass: CharacterClass, actorStats: ActorStats, actorSkills: ActorSkills, combatStyle: CombatStyle,
                damageMultiplier: DamageMultiplier.BaseValue,
                damageReductionMultiplier: DamageReductionMultiplier.BaseValue,
                criticalHitMultiplier: CriticalHitMultiplier.BaseValue,
                equipment: Equipment?.ToArray()!,
                inventory: Inventory,
                lootConfig: LootConfig,
                lootContainer: LootContainer
            );
        }

        // public virtual void ActorDeathMessageHandler(IMessageEnvelope message)
        // {
        //     if (!message.Message<ActorDeathMessage>().HasValue)
        //     {
        //         return;
        //     }
        //
        //     var data = message.Message<ActorDeathMessage>().GetValueOrDefault();
        //     if (data.Source != this)
        //     {
        //         return;
        //     }
        //
        //     var loot = data.Target.Loot();
        //     switch (CombatStyle)
        //     {
        //         case CombatStyle.Melee:
        //             ActorSkills.MeleeSkill.Leveling.GainExperience(loot.CombatExperience);
        //             break;
        //         case CombatStyle.Ranged:
        //             ActorSkills.RangedSkill.Leveling.GainExperience(loot.CombatExperience);
        //             break;
        //         case CombatStyle.Magic:
        //             ActorSkills.MagicSkill.Leveling.GainExperience(loot.CombatExperience);
        //             break;
        //         case CombatStyle.Hybrid:
        //             var sharedExp = loot.CombatExperience / 3;
        //             ActorSkills.MeleeSkill.Leveling.GainExperience(sharedExp);
        //             ActorSkills.RangedSkill.Leveling.GainExperience(sharedExp);
        //             ActorSkills.MagicSkill.Leveling.GainExperience(sharedExp);
        //             break;
        //     }
        //     foreach (var skill in loot.SkillExperiences)
        //     {
        //         switch (skill.SkillType)
        //         {
        //             case SkillType.None:
        //                 break;
        //             case SkillType.Melee:
        //                 ActorSkills.MeleeSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Ranged:
        //                 ActorSkills.RangedSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Magic:
        //                 ActorSkills.MagicSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Alchemy:
        //                 ActorSkills.AlchemySkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Cooking:
        //                 ActorSkills.CookingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Crafting:
        //                 ActorSkills.CraftingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Enchanting:
        //                 ActorSkills.EnchantingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Fishing:
        //                 ActorSkills.FishingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Fletching:
        //                 ActorSkills.FletchingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Foraging:
        //                 ActorSkills.ForagingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Mining:
        //                 ActorSkills.MiningSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Smithing:
        //                 ActorSkills.SmithingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Ranching:
        //                 ActorSkills.RanchingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //             case SkillType.Woodcutting:
        //                 ActorSkills.WoodcuttingSkill.Leveling.GainExperience(skill.Experience);
        //                 break;
        //         }
        //     }
        //     
        //     Inventory?.Gold.Add(otherCurrency: loot.Gold);
        //     Inventory?.Items.AddRange(collection: loot.Items);
        //     var itemsString = loot.Items.Count is > 0 and > 1 ? "Items" : "Item";
        //     var lootItemsDisplay = loot.Items.Count > 0
        //         ? $"{loot.Items.Count} {itemsString} from {data.Target.Name}\n" +
        //           string.Join(separator: ", \n", values: loot.Items.Select(selector: x => "Looted " + x.Name))
        //         : "No Items";
        //     Console.WriteLine(value: $"{Name} has looted {loot.Gold} and {lootItemsDisplay}");
        // }

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
            }
        }
    }
}