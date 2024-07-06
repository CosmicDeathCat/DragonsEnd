using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat.Messages
{
    public struct RoundEndMessage
    {
        private ICombatContext CombatContext { get; }

        public RoundEndMessage(ICombatContext combatContext)
        {
            CombatContext = combatContext;
        }
    }
}