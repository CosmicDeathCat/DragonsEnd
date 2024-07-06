using System;
using System.Collections.Generic;
using System.Linq;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.Combat.Messages;

namespace DragonsEnd.Combat
{
    public class CombatSystem : ICombatSystem
    {
        public ICombatContext CombatContext { get; set; } = new CombatContext();

        public CombatSystem()
        {
        }

        public CombatSystem(List<IActor> players, List<IActor> enemies)
        {
            CombatContext.Setup(players: players, enemies: enemies);
        }

        public void Setup(List<IActor> players, List<IActor> enemies)
        {
            CombatContext.Setup(players: players, enemies: enemies);
        }

        public void StartCombat()
        {
            CombatContext.ResetTurns();
            CombatContext.RollInitiative();
            CombatContext.SortCombatantsByInitiative();

            while (CombatContext.Players.Any(predicate: p => p.IsAlive) && CombatContext.Enemies.Any(predicate: e => e.IsAlive))
            {
                CombatContext.CurrentRound++;
                Console.WriteLine(value: $"Round {CombatContext.CurrentRound} has started.");
                MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat, message: new RoundStartMessage(combatContext: CombatContext));
                foreach (var actor in CombatContext.Combatants)
                {
                    if (actor.IsAlive)
                    {
                        StartTurn(actor: actor);
                        CombatContext.CurrentTurn++;
                        CombatContext.CurrentActor = actor;
                        if (CombatContext.Players.Contains(item: actor))
                        {
                            actor.TakeTurn(combatContext: CombatContext, targets: CombatContext.Enemies, allies: CombatContext.Players);
                        }
                        else
                        {
                            actor.TakeTurn(combatContext: CombatContext, targets: CombatContext.Players, allies: CombatContext.Enemies);
                        }

                        EndTurn(actor: actor);
                    }
                }

                MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat, message: new RoundEndMessage(combatContext: CombatContext));

                if (!CombatContext.Players.Any(predicate: p => p.IsAlive))
                {
                    Defeat();
                    break;
                }

                if (!CombatContext.Enemies.Any(predicate: e => e.IsAlive))
                {
                    Victory();
                    break;
                }
            }
        }

        public void StartTurn(IActor actor)
        {
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat, message: new TurnStartMessage(combatContext: CombatContext, sender: actor));
        }

        public void EndTurn(IActor actor)
        {
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat, message: new TurnEndMessage(combatContext: CombatContext, sender: actor));
        }

        public void Victory()
        {
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat,
                message: new VictoryMessage(combatContext: CombatContext, players: CombatContext.Players, enemies: CombatContext.Enemies));
        }

        public void Defeat()
        {
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Combat,
                message: new DefeatMessage(combatContext: CombatContext, players: CombatContext.Players, enemies: CombatContext.Enemies));
        }
    }
}