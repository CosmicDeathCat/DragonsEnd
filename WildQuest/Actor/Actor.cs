using System;
using System.Collections.Generic;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Messaging.Messages;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class Actor : IActor
{         
    public virtual string Name {get;set;} 
	public virtual Gender Gender {get;set;}
	public virtual ILeveling Leveling {get;set;}
	public virtual CharacterClassType CharacterClass { get; set; }
	public IEquipmentItem?[] Equipment { get; set; } = new IEquipmentItem[Enum.GetNames<EquipmentSlot>().Length];
	public List<IItem> Inventory { get; set; } = new();
	public virtual bool IsAlive {get;set;}
	public virtual ActorStats ActorStats {get;set;}

	public Actor(string name, Gender gender, CharacterClassType characterClass, ActorStats actorStats)
	{
        Name = name;
		Gender = gender;
		CharacterClass = characterClass;
		ActorStats = actorStats;
		IsAlive = true;
		Leveling = new Leveling(this);
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
}