using System;
using WildQuest.Actor;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Items;
using WildQuest.Stats;

namespace WildQuest.Enemy;

public class BasicEnemy : CombatActor, IEnemy
{
    public BasicEnemy() :base()
    {
        
    }
    
    public BasicEnemy(
        string name,
        Gender gender,
        CharacterClassType characterClass,
        ActorStats actorStats,
        CombatStyle combatStyle,
        int level = 1,
        long experience = -1L,
        double damageMultiplier = 1.00,
        double damageReductionMultiplier = 1.00,
        long gold = 0,
        IEquipmentItem[]? equipment = null,
        IItem[]? inventory = null,
        params IDropItem[] dropItems) 
        : base(name, gender, characterClass, actorStats, combatStyle, level, experience, damageMultiplier, damageReductionMultiplier, gold, equipment, inventory, dropItems)
    {
        
    }
    
}