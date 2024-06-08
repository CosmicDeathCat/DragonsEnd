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
    public virtual string Name {get;set;} 
	public virtual Gender Gender {get;set;}
	public virtual ILeveling Leveling {get;set;}
	public virtual CharacterClassType CharacterClass { get; set; }
	public virtual IEquipmentItem?[] Equipment { get; set; } = new IEquipmentItem[Enum.GetNames<EquipmentSlot>().Length];
	public virtual List<IItem?> Inventory { get; set; } = new();
	public ICurrency Gold { get; set; } = new GoldCurrency(0);
	public virtual bool IsAlive {get;set;}
	public virtual ActorStats ActorStats {get;set;}
	
	public List<IDropItem> DropItems { get; set; } = new();

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
		int level = 1,
		long gold = 0,
		IEquipmentItem[]? equipment = null,
		IItem[]? inventory = null, 
		params IDropItem[] dropItems)
	{
        Name = name;
		Gender = gender;
		CharacterClass = characterClass;
		ActorStats = actorStats;
		IsAlive = true;
		Leveling = new Leveling(this, level);
		Gold = new GoldCurrency(gold);
		if (inventory != null)
		{
			Inventory.AddRange(inventory);
		}
		else
		{
			Inventory = new List<IItem>();
		}
		if (equipment != null)
		{
			foreach (var item in equipment)
			{
				item.Equip(this, this);
			}
		}
		else
		{
			Equipment = new IEquipmentItem[Enum.GetNames<EquipmentSlot>().Length];
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
		if(!message.Message<ItemMessage>().HasValue) return;
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
		if(data.Target == this)
		{
			var equipmentItem = data.Item as IEquipmentItem;
			if(equipmentItem != null)
			{
				equipmentItem.Equip(data.Source, this);
			}
		}
		
	}

	public virtual void LevelingMessageHandler(IMessageEnvelope message)
	{
		if(!message.Message<LevelingMessage>().HasValue) return;
		var data = message.Message<LevelingMessage>().GetValueOrDefault();
		if(data.Actor != this) return;
		switch (data.Type)
		{
			case LevelingType.GainExperience:
				Console.WriteLine($"{Name} gained {data.Experience} experience.");
				break;
			case LevelingType.LoseExperience:
				Console.WriteLine($"{Name} lost {data.Experience} experience.");
				break;
			case LevelingType.SetExperience:
				Console.WriteLine($"{Name} set experience to {data.Experience}.");
				break;
			case LevelingType.GainLevel:
				Console.WriteLine($"{Name} gained a level.");
				break;
			case LevelingType.LoseLevel:
				Console.WriteLine($"{Name} lost a level.");
				break;
			case LevelingType.SetLevel:
				Console.WriteLine($"{Name} set level to {data.Level}.");
				break;
		}
	}


	public LootContainer Loot(
		IActor? source,            
		long minItemAmountDrop = -1L, 
		long maxItemAmountDrop = -1L, 
		long minGold = -1L, 
		long maxGold = -1L, 
		long minExperience = -1L, 
		long maxExperience = -1L,
		params IDropItem[] specificLootableItems)
	{
		var loot = LootSystem.GenerateLoot(
			actor: this,
			minItemAmountDrop: minItemAmountDrop,
			maxItemAmountDrop: maxItemAmountDrop,
			minGold: minGold,
			maxGold: maxGold,
			minExperience: minExperience,
			maxExperience: maxExperience,
			specificLootableItems: specificLootableItems);
		var lootContainer = new LootContainer(loot.gold, loot.experience, loot.items.ToArray());
		return lootContainer;
		// long gold = loot.gold;
		// int experience = loot.experience;
		// List<IItem> items = loot.items;
		//
		// // Add gold to Source inventory
		// source.Gold.Add(gold);
		//
		// // Add items to Source inventory
		// source.Inventory.AddRange(items);
		//
		// // Add experience to Source leveling system
		// source.Leveling.GainExperience(experience);
	}

	public void HandleDeath(IActor? source)
	{
		ActorStats.Health.CurrentValue = 0;
		IsAlive = false;
		if (source == null) return;
		// Loot(source);
		
		// // Generate loot for the source
		// var loot = LootSystem.GenerateLoot();;
		// long gold = loot.gold;
		// int experience = loot.experience;
		// List<IItem> items = loot.items;
		//
		// // Create a loot container and add the loot
		// var lootContainer = new LootContainer();
		// lootContainer.AddLoot(new GoldCurrency(gold), experience, items);
		//
		// // Give the loot to the source
		// source.Gold.Add(lootContainer.Gold);
		// source.Leveling.GainExperience(lootContainer.Experience);
		// source.Inventory.AddRange(lootContainer.Items);
		MessageSystem.MessageManager.SendImmediate(MessageChannels.Combat, new ActorDeathMessage(source, this));
	}

}