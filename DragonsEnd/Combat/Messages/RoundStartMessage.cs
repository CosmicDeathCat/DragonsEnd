using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat.Messages
{
    public struct RoundStartMessage
    {
        private ICombatContext CombatContext { get; }

        public RoundStartMessage(ICombatContext combatContext)
        {
            CombatContext = combatContext;
        }
    }
}