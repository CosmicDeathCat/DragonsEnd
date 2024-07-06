using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat.Messages
{
    public struct VictoryMessage
    {
        public ICombatContext CombatContext { get; }
        public List<IActor> Players { get; }
        public List<IActor> Enemies { get; }

        public VictoryMessage(ICombatContext combatContext, List<IActor> players, List<IActor> enemies)
        {
            CombatContext = combatContext;
            Players = players;
            Enemies = enemies;
        }
    }
}