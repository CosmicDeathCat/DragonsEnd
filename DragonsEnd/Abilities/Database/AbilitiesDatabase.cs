using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DragonsEnd.Abilities.Combat;
using DragonsEnd.Abilities.Constants;
using DragonsEnd.Abilities.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Abilities.Database
{
    public static class AbilitiesDatabase
    {
        public static ConcurrentDictionary<string, IAbility> Abilities { get; set; } =
            new(comparer: StringComparer.OrdinalIgnoreCase)
            {
                [key: AbilityNames.BasicAttack] = new BasicAttackAbility(
                name: AbilityNames.BasicAttack,
                description: "A basic attack that deals damage based on the source's attack value and the target's defense value.",
                type: AbilityType.Attack,
                requiredStatResourceCost: null,
                targetingType: TargetingType.Enemy,
                targetingScope: TargetingScope.Single,
                actorScopeType: ActorScopeType.Alive,
                style: CombatStyle.Melee,
                baseDamage: 1,
                ignoreDefense: false,
                alwaysHits: false
                ),
            };
        
        public static IAbility GetAbility(string abilityName)
        {
            if (Abilities.TryGetValue(key: abilityName, value: out var ability))
            {
                return ability.Copy() ?? throw new InvalidOperationException();
            }

            throw new ArgumentException(message: $"Ability with name '{abilityName}' not found.");
        }
    }
}