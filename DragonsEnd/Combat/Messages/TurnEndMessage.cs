using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;

namespace DragonsEnd.Combat.Messages
{
    public struct TurnEndMessage
    {
        public ICombatContext CombatContext { get; }
        public IActor Sender { get; }

        public TurnEndMessage(ICombatContext combatContext, IActor sender)
        {
            CombatContext = combatContext;
            Sender = sender;
        }
    }
}