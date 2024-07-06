using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat.Messages
{
    public struct TurnStartMessage
    {
        public ICombatContext CombatContext { get; }
        public IActor Sender { get; }

        public TurnStartMessage(ICombatContext combatContext, IActor sender)
        {
            CombatContext = combatContext;
            Sender = sender;
        }
    }
}