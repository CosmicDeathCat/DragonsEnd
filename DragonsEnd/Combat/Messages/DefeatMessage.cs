using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat.Messages
{
    public struct DefeatMessage
    {
        public ICombatContext CombatContext { get; }
        public List<IActor> Players { get; }
        public List<IActor> Enemies { get; }

        public DefeatMessage(ICombatContext combatContext, List<IActor> players, List<IActor> enemies)
        {
            CombatContext = combatContext;
            Players = players;
            Enemies = enemies;
        }
    }
}