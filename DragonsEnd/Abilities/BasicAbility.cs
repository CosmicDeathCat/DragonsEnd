using System;
using System.Collections.Generic;
using DragonsEnd.Abilities.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat_Resources;
using DragonsEnd.Stats.Stat_Resources.Interfaces;

namespace DragonsEnd.Abilities
{
    public class BasicAbility : IAbility
    {
        public virtual string Name { get; set; }
        public virtual Guid ID { get; set; }
        public virtual string Description { get; set; }
        public virtual AbilityType Type { get; set; }
        public virtual IRequiredStatResources? RequiredStatResourceCost { get; set; }
        public virtual TargetingType TargetingType { get; set; }
        public virtual TargetingScope TargetingScope { get; set; }
        public virtual ActorScopeType ActorScopeType { get; set; }
        public virtual List<IActor?> Targets { get; set; }
        public virtual IActor? SourceActor { get; set; }

        public BasicAbility()
        {
            ID = Guid.NewGuid();
        }

        public BasicAbility(string name, string description, AbilityType type, IRequiredStatResources? requiredStatResourceCost, TargetingType targetingType, TargetingScope targetingScope, ActorScopeType actorScopeType)
        {
            Name = name;
            ID = Guid.NewGuid();
            Description = description;
            Type = type;
            RequiredStatResourceCost = requiredStatResourceCost;
            TargetingType = targetingType;
            TargetingScope = targetingScope;
            ActorScopeType = actorScopeType;
            
        }
        
        public virtual void SetSource(IActor? source)
        {
            SourceActor = source;
        }
        
        public virtual void SetTargets(List<IActor?> targets)
        {
            Targets = targets;
        }

        public virtual bool Useable()
        {
            return Useable(source: SourceActor);
        }

        public virtual bool Useable(IActor? source)
        {
            if (source == null)
            {
                return false;
            }
            if (RequiredStatResourceCost != null)
            {
                var sourceStats = source.ActorStats;
                foreach (var requiredResource in RequiredStatResourceCost.RequiredResources)
                {
                    switch (requiredResource.Type)
                    {
                        case StatResourceType.Health:
                            if (sourceStats?.Health.CurrentValue < requiredResource.RequiredAmount)
                            {
                                return false;
                            }
                            break;
                        case StatResourceType.Mana:
                            if (sourceStats?.Mana.CurrentValue < requiredResource.RequiredAmount)
                            {
                                return false;
                            }
                            break;
                        case StatResourceType.Stamina:
                            if (sourceStats?.Stamina.CurrentValue < requiredResource.RequiredAmount)
                            {
                                return false;
                            }
                            break;

                    }
                }
            
            }


            return true;
        }

        public virtual bool Use(IActor? source, List<IActor?> targets)
        {
            if(source == null) return false;
            if(!Useable(source)) return false;
            SetSource(source);
            SetTargets(targets);
            if(targets.Count == 0) return false;
            
            if (RequiredStatResourceCost != null)
            {
                var sourceStats = source.ActorStats!;
                foreach (var requiredResource in RequiredStatResourceCost.RequiredResources)
                {
                    switch (requiredResource.Type)
                    {
                        case StatResourceType.Health:
                            sourceStats.Health.CurrentValue -= requiredResource.RequiredAmount;
                            break;
                        case StatResourceType.Mana:
                            sourceStats.Mana.CurrentValue -= requiredResource.RequiredAmount;
                            break;
                        case StatResourceType.Stamina:
                            sourceStats.Stamina.CurrentValue -= requiredResource.RequiredAmount;
                            break;

                    }
                }
            }
            
            return true;
        }

        public virtual IAbility Copy()
        {
            return new BasicAbility()
            {
                Name = Name,
                ID = ID,
                Type = Type,
                RequiredStatResourceCost = RequiredStatResourceCost,
                TargetingType = TargetingType,
                TargetingScope = TargetingScope,
                ActorScopeType = ActorScopeType,
                Targets = Targets
            };
        }
    }
}