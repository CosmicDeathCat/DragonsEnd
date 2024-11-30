using System;
using DragonsEnd.Abilities.Combat.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Stats.Stat_Resources.Interfaces;

namespace DragonsEnd.Abilities
{
    public class DurationAbility : BasicAbility, IDurationAbility
    {
        public virtual DurationType DurationType { get; set; }
        public virtual int MinDuration { get; set; }
        public virtual int MaxDuration { get; set; }
        public virtual int CurrentDuration { get; set; }
        public virtual bool IsDurationRandom { get; set; }
        public virtual bool IsDurationComplete { get => CurrentDuration <= 0; }
        
        public DurationAbility()
        {
            ID = Guid.NewGuid();
        }
        
        public DurationAbility(string name, string description, AbilityType type, IRequiredStatResources? requiredStatResourceCost, TargetingType targetingType, TargetingScope targetingScope, ActorScopeType actorScopeType, DurationType durationType, int minDuration, int maxDuration, bool isDurationRandom = false)
        {
            Name = name;
            ID = Guid.NewGuid();
            Description = description;
            Type = type;
            RequiredStatResourceCost = requiredStatResourceCost;
            TargetingType = targetingType;
            TargetingScope = targetingScope;
            ActorScopeType = actorScopeType;
            DurationType = durationType;
            MinDuration = minDuration;
            MaxDuration = maxDuration;
            IsDurationRandom = isDurationRandom;
            CurrentDuration = isDurationRandom ? new Random().Next(minDuration, maxDuration + 1) : maxDuration;
        }
        
        public virtual void StartDuration()
        {
            CurrentDuration = IsDurationRandom ? new Random().Next(MinDuration, MaxDuration + 1) : MaxDuration;
        }

        /// <summary>
        ///  Update the duration of the ability based on the duration type
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual void UpdateDuration()
        {
            switch (DurationType)
            {
                case DurationType.None:
                    // No duration to handle
                    break;

                case DurationType.Instant:
                    // Instant effects are applied immediately, duration is irrelevant
                    CurrentDuration = 0;
                    break;

                case DurationType.Permanent:
                    // Permanent effects never expire
                    CurrentDuration = int.MaxValue;
                    break;

                case DurationType.Temporary:
                case DurationType.Timed:
                case DurationType.Turn:
                case DurationType.Round:
                    if (CurrentDuration > 0)
                    {
                        CurrentDuration--;
                    }
                    else
                    {
                        CurrentDuration = 0; // Ensure it doesn't go below zero
                    }
                    break;

                case DurationType.Encounter:
                    // Encounter duration would be handled by the game's encounter system
                    // Typically, it would be set to a specific value at the start and decremented by the encounter system
                    break;

                case DurationType.Custom:
                    // Custom duration handling logic can be implemented here
                    HandleCustomDuration();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Edge case handling: Ensure current duration does not go negative
            if (CurrentDuration < 0)
            {
                CurrentDuration = 0;
            }
            
        }

        /// <summary>
        ///  Custom end duration logic can be implemented here
        /// </summary>
        public virtual void EndDuration()
        {
            
        }

        /// <summary>
        ///   Custom duration handling logic can be implemented here
        /// </summary>
        public virtual void HandleCustomDuration()
        {
            
        }
    }
}