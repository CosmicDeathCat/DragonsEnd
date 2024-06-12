using System;
using System.Collections.Generic;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Items;
using WildQuest.Items.Currency;
using WildQuest.Items.Loot;
using WildQuest.Messaging.Messages;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class Actor : IActor
{
    public virtual string Name { get; set; }
    public virtual Gender Gender { get; set; }
    public virtual ILeveling Leveling { get; set; }
    public virtual DoubleStat DamageMultiplier { get; set; } = new(1.00);
    public virtual DoubleStat DamageReductionMultiplier { get; set; } = new(1.00);
    public virtual DoubleStat CriticalHitMultiplier { get; set; } = new(1.5);
    public virtual CharacterClassType CharacterClass { get; set; }
    public virtual IEquipmentItem?[] Equipment { get; set; } = new IEquipmentItem[Enum.GetNames(typeof(EquipmentSlot)).Length -1];
    public virtual List<IItem?> Inventory { get; set; } = new();
    public GoldCurrency Gold { get; set; } = new(0);
    public virtual IActor? Target { get; set; }


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

    public virtual ActorStats ActorStats { get; set; }

    public List<IDropItem> DropItems { get; set; } = new();

    protected CombatStyle _combatStyle = CombatStyle.Melee;

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

    public Actor()
    {
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
        CombatStyle = combatStyle;
        DamageMultiplier = new DoubleStat(damageMultiplier);
        DamageReductionMultiplier = new DoubleStat(damageReductionMultiplier);
        CriticalHitMultiplier = new DoubleStat(criticalHitMultiplier);
        IsAlive = true;
        Leveling = new Leveling(this, level, experience);
        Gold = new GoldCurrency(gold);
        if (inventory != null)
        {
            Inventory.AddRange(inventory);
        }
        else
        {
            Inventory = [];
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

    ~Actor()
    {
        MessageSystem.MessageManager.UnregisterForChannel<ItemMessage>(MessageChannels.Items, ItemMessageHandler);
        MessageSystem.MessageManager.UnregisterForChannel<LevelingMessage>(MessageChannels.Level, LevelingMessageHandler);
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
        if (!message.Message<ItemMessage>().HasValue) return;
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

    public virtual void LevelingMessageHandler(IMessageEnvelope message)
    {
        if (!message.Message<LevelingMessage>().HasValue) return;
        var data = message.Message<LevelingMessage>().GetValueOrDefault();
        if (data.Actor != this) return;
        switch (data.Type)
        {
            case LevelingType.GainExperience:
                Console.WriteLine($"{Name} gained {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
                break;
            case LevelingType.LoseExperience:
                Console.WriteLine($"{Name} lost {data.Experience} experience. {Name} now has {Leveling.Experience} experience. {Leveling.ExperienceToNextLevel} remaining till next level.");
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

    public virtual void IncreaseStatsForLevel(int level)
    {
        int statIncrease = level - 1; // Linear increase

        int oldHealth = ActorStats.Health.BaseValue;
        int oldMeleeAttack = ActorStats.MeleeAttack.BaseValue;
        int oldRangedAttack = ActorStats.RangedAttack.BaseValue;
        int oldMagicAttack = ActorStats.MagicAttack.BaseValue;
        int oldMeleeDefense = ActorStats.MeleeDefense.BaseValue;
        int oldRangedDefense = ActorStats.RangedDefense.BaseValue;
        int oldMagicDefense = ActorStats.MagicDefense.BaseValue;

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
        int statDecrease = level - 1; // Linear decrease

        int oldHealth = ActorStats.Health.BaseValue;
        int oldMeleeAttack = ActorStats.MeleeAttack.BaseValue;
        int oldRangedAttack = ActorStats.RangedAttack.BaseValue;
        int oldMagicAttack = ActorStats.MagicAttack.BaseValue;
        int oldMeleeDefense = ActorStats.MeleeDefense.BaseValue;
        int oldRangedDefense = ActorStats.RangedDefense.BaseValue;
        int oldMagicDefense = ActorStats.MagicDefense.BaseValue;

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
        return new Actor(Name, Gender, CharacterClass, ActorStats, CombatStyle, DamageMultiplier.BaseValue, DamageReductionMultiplier.BaseValue,
            CriticalHitMultiplier.BaseValue, Leveling.CurrentLevel, Leveling.Experience, Gold.CurrentValue, Equipment?.ToArray()!,
            Inventory?.ToArray()!, DropItems?.ToArray()!);
    }
}