using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DLS.MessageSystem.Messaging.MessageWrappers.Extensions;
using DLS.MessageSystem.Messaging.MessageWrappers.Interfaces;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Leveling.Interfaces;
using DragonsEnd.Leveling.Messages;
using DragonsEnd.Lockable.Interfaces;
using DragonsEnd.Skills.Interfaces;

namespace DragonsEnd.Skills
{
    public abstract class BaseSkill : ISkill
    {
        public BaseSkill(string name, IActor actor, int maxLevel = 100)
        {
            Name = name;
            Actor = actor;
            Leveling = new Leveling.Leveling(actor: actor, name: name, maxLevel: maxLevel);
            MessageSystem.MessageManager.RegisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }

        ~BaseSkill()
        {
            MessageSystem.MessageManager.UnregisterForChannel<LevelingMessage>(channel: MessageChannels.Level, handler: LevelingMessageHandler);
        }
        
        public virtual string Name { get; set; }
        public virtual Guid ID { get; set; } = Guid.NewGuid();
        
        public virtual SkillType SkillType => SkillType.None;
        public virtual ILeveling Leveling { get; set; }
        public virtual IActor Actor { get; set; }
        public virtual ConcurrentDictionary<int, List<ILockable>> Unlocks { get; set; } = new();

        public virtual void HandleUnlocks(int level)
        {
            // Handle unlocking for the current level
            if (Unlocks.ContainsKey(key: level))
            {
                foreach (var unlock in Unlocks[key: level])
                {
                    if (unlock.Unlock())
                    {
                        Console.WriteLine(value: $"{Actor.Name} has unlocked {unlock.Name}. {unlock.Description}");
                    }
                }
            }

            // Handle locking for the levels above the current level
            foreach (var kvp in Unlocks)
            {
                if (kvp.Key > level)
                {
                    foreach (var unlock in kvp.Value)
                    {
                        if (unlock.Lock())
                        {
                            Console.WriteLine(value: $"{Actor.Name} has locked {unlock.Name}.");
                        }
                    }
                }
            }
        }


        public void LevelingMessageHandler(IMessageEnvelope message)
        {
            if (!message.Message<LevelingMessage>().HasValue)
            {
                return;
            }

            var data = message.Message<LevelingMessage>().GetValueOrDefault();
            if (data.SenderIdentity.ID.Equals(g: Leveling.ID))
            {
                switch (data.Type)
                {
                    case LevelingType.GainLevel:
                        Console.WriteLine(value: $"{Actor.Name} has gained a level in {Name}! now level {Leveling.CurrentLevel}!");
                        HandleUnlocks(level: Leveling.CurrentLevel);
                        break;
                    case LevelingType.GainExperience:
                        Console.WriteLine(value: $"{Actor.Name} has gained {data.Experience} {data.SenderIdentity.Name} experience!");
                        break;
                    case LevelingType.LoseExperience:
                        Console.WriteLine(value: $"{Actor.Name} has lost {data.Experience} {data.SenderIdentity.Name} experience!");
                        break;
                    case LevelingType.LoseLevel:
                        Console.WriteLine(value: $"{Actor.Name} has lost a level in {Name}! now level {Leveling.CurrentLevel}!");
                        HandleUnlocks(level: Leveling.CurrentLevel);
                        break;
                }
            }
        }
    }
}