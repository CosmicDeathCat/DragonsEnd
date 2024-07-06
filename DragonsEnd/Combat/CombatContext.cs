using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat
{
    public class CombatContext : ICombatContext
    {
        public CombatContext()
        {
            Players = new List<IActor>();
            Enemies = new List<IActor>();
            Combatants = new List<IActor>();
        }

        public CombatContext(List<IActor> players, List<IActor> enemies)
        {
            Setup(players: players, enemies: enemies);
        }

        public virtual int CurrentRound { get; set; }
        public virtual IActor CurrentActor { get; set; }
        public virtual List<IActor> Players { get; set; } = new();
        public virtual List<IActor> Enemies { get; set; } = new();
        public virtual List<IActor> Combatants { get; set; } = new();

        public virtual void Setup(List<IActor> players, List<IActor> enemies)
        {
            Players = players;
            Enemies = enemies;
            Combatants.Clear();
            Combatants.AddRange(collection: Players);
            Combatants.AddRange(collection: Enemies);
            CurrentRound = 0;
        }

        public virtual void RollInitiative()
        {
            foreach (var actor in Combatants)
            {
                actor.Initiative = actor.RollInitiative();
            }
        }

        public void ResetTurns()
        {
            foreach (var actor in Combatants)
            {
                actor.ResetTurns();
            }
        }

        public virtual void SortCombatantsByInitiative()
        {
            Combatants.Sort(comparison: (a, b) => b.Initiative.CompareTo(value: a.Initiative));
        }
    }
}