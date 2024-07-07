using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Actor.Player.Interfaces
{
    public interface IPlayer : IActor
    {
        void ShowCombatActions();
        bool PerformCombatAction(ICombatContext combatContext, List<IActor> targets, List<IActor> allies);
        void DisplayInventory();
        void DisplayStats();
        void DisplayEquipment();
        void DisplaySkills();

        List<IActor?> ChooseTargets
        (
            List<IActor> enemies,
            List<IActor> allies,
            TargetingScope targetingScope = TargetingScope.Single,
            TargetingType targetingType = TargetingType.Enemy,
            ActorScopeType actorScopeType = ActorScopeType.Alive
        );
    }
}