﻿using System.Collections.Generic;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using WildQuest.Enums;
using WildQuest.Stats;

namespace WildQuest.Interfaces;

public interface IActor : ILootable
{         
    string Name {get;set;} 
	Gender Gender {get;set;}
	CharacterClassType CharacterClass {get;set;}
	IEquipmentItem?[] Equipment {get;set;}
	List<IItem?> Inventory {get;set;}
	ActorStats ActorStats {get;set;}
	IActor? Target {get;set;}
	bool IsAlive {get;set;}    
	IWeaponItem?[] GetWeapons();
	IArmorItem?[] GetArmor();
	void ItemMessageHandler(IMessageEnvelope message);
	void Die();

}