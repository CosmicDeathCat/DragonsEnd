using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Identity.Interfaces;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat_Resources.Interfaces;

namespace DragonsEnd.Abilities.Interfaces
{
    public interface IAbility : IIdentity
    {
        string Description { get; set; }
        AbilityType Type { get; set; }
        IRequiredStatResources? RequiredStatResourceCost { get; set; }
        TargetingType TargetingType { get; set; }
        TargetingScope TargetingScope { get; set; }
        ActorScopeType ActorScopeType { get; set; }
        List<IActor?> Targets { get; set; }
        IActor? SourceActor { get; set; }
        bool Useable(IActor? source);
        bool Useable();
        bool Use(IActor? source, List<IActor?> targets);
        void SetSource(IActor? source);
        void SetTargets(List<IActor?> targets);
        IAbility Copy();
    }
}