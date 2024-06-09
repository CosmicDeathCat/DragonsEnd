using WildQuest.Actor;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Enemy;

public class BasicEnemy(
    string name,
    Gender gender,
    CharacterClassType characterClass,
    ActorStats actorStats,
    int level = 1,
    double damageMultiplier = 1.00,
    double damageReductionMultiplier = 1.00,
    long gold = 0,
    IEquipmentItem[]? equipment = null,
    IItem[]? inventory = null,
    params IDropItem[] dropItems) 
    : CombatActor(name, gender, characterClass, actorStats, level, damageMultiplier, damageReductionMultiplier, gold, equipment, inventory, dropItems), IEnemy
{
    
}