using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Combat.Interfaces
{
    public interface ICombatContext
    {
        int CurrentRound { get; set; }
        int CurrentTurn { get; set; }
        IActor CurrentActor { get; set; }
        List<IActor> Players { get; set; }
        List<IActor> Enemies { get; set; }
        List<IActor> Combatants { get; set; }

        void Setup(List<IActor> players, List<IActor> enemies);
        void RollInitiative();
        void ResetTurns();
        void SortCombatantsByInitiative();
    }
}