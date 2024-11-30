using System.Collections.Generic;
using System.Numerics;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Abilities.Combat.Interfaces;
using DragonsEnd.Abilities.Interfaces;
using DragonsEnd.Combat.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Items.Equipment.Interfaces;
using DragonsEnd.Items.Inventory.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Stats;
using DragonsEnd.Stats.Stat;

namespace DragonsEnd.Actor.Interfaces
{
    public interface IActor : ILootable
    {
        Gender Gender { get; set; }
        Vector2 Position { get; set; }
        int CombatLevel { get; }
        CharacterClassType CharacterClass { get; set; }
        IEquipmentItem?[] Equipment { get; set; }
        IInventory? Inventory { get; set; }

        // List<IItem?> Inventory { get; set; }
        ActorStats? ActorStats { get; set; }
        IActorSkills? ActorSkills { get; set; }
        public DoubleStat DamageMultiplier { get; set; }
        public DoubleStat DamageReductionMultiplier { get; set; }
        DoubleStat CriticalHitMultiplier { get; set; }
        IActor? Target { get; set; }
        bool IsAlive { get; set; }
        int Initiative { get; set; }
        CombatStyle CombatStyle { get; set; }
        IAttackAbility? AttackAbility { get; set; }
        IDefendAbility? DefendAbility { get; set; }
        int TurnCount { get; set; }
        List<IAbility> ActiveAbilities { get; set; }
        void ResetTurns();
        IWeaponItem?[] GetWeapons();
        IArmorItem?[] GetArmor();
        void ItemMessageHandler(IMessageEnvelope message);
        void TakeDamage(IActor sourceActor, int damage);

        (bool hasHit, bool isBlocked, bool hasKilled, int damage, bool isCriticalHit) Attack
        (
            IActor source,
            IActor target
        );

        (bool hit, bool blocked, bool killed, int damage, bool isCriticalHit) HandleAttack
        (
            IActor source,
            IActor target,
            int attackValue,
            int defenseValue
        );

        int RollInitiative();
        bool TakeTurn(ICombatContext combatContext, List<IActor> targets, List<IActor> allies);
        IActor? SelectRandomTarget(List<IActor> targets);
        void Die();
        void IncreaseStatsForLevel(int level);
        void DecreaseStatsForLevel(int level);

        IActor Copy();
        // void ActorDeathMessageHandler(IMessageEnvelope message);
    }
}