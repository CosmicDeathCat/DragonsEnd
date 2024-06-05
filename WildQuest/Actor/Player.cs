using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Actor;

public class Player : CombatActor, IPlayer
{
    public Player(string name, Gender gender, CharacterClassType characterClass, ActorStats actorStats) : base(name, gender, characterClass, actorStats)
    {
        
    }
}