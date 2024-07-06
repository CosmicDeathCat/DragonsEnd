using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Combat.Interfaces
{
    public interface ICombatSystem
    {
        ICombatContext CombatContext { get; set; }
        void Setup(List<IActor> players, List<IActor> enemies);
        void StartCombat();
        void StartTurn(IActor actor);
        void EndTurn(IActor actor);
        void Victory();
        void Defeat();
    }
}