using System;
using System.Collections.Generic;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Leveling.Interfaces;
using DragonsEnd.Leveling.Messages;
using DragonsEnd.Utility.Extensions.Int;

namespace DragonsEnd.Leveling
{
    //TODO: Handle types of levels like skills and regular combat levels
    public class Leveling : ILeveling
    {
        public static long BaseExperience { get; set; } = 50;
        public static double ExperienceExponent { get; set; } = 1.25;
        public static int DefaultMaxLevel { get; set; } = 100;

        public static double DefaultLevelingThreshold { get; set; } = 10.00;
        public virtual string Name { get; set; }
        public virtual Guid ID { get; set; } = Guid.NewGuid();

        public double LevelingThreshold { get; set; } = DefaultLevelingThreshold;

        public IActor? Actor { get; set; }

        public virtual int CurrentLevel { get => _currentLevel; set => _currentLevel = ValidateLevel(level: value); }

        public virtual int MaxLevel
        {
            get => _maxLevel;
            set
            {
                _maxLevel = value < 1 ? 1 : value;
                if (_currentLevel > _maxLevel)
                {
                    _currentLevel = _maxLevel;
                }
            }
        }

        public virtual long Experience
        {
            get => _experience;
            set
            {
                var oldExperience = _experience;
                _experience = ValidateExperience(experience: value);
                if (_experience > oldExperience)
                {
                    MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                        message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: _experience - oldExperience, level: CurrentLevel,
                            type: LevelingType.GainExperience));
                }
                else if (_experience < oldExperience)
                {
                    MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                        message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: oldExperience - _experience, level: CurrentLevel,
                            type: LevelingType.LoseExperience));
                }

                ValidateLevel();
            }
        }

        public virtual long ExperienceToNextLevel
        {
            get
            {
                if (CurrentLevel >= MaxLevel)
                {
                    return 0;
                }

                return ExperienceLevels[key: CurrentLevel + 1] - Experience;
            }
        }

        public Dictionary<int, long> ExperienceLevels { get; set; } =
            GenerateExperienceLevels(maxLevel: DefaultMaxLevel, baseExperience: BaseExperience, exponent: ExperienceExponent);

        public Leveling(string name, int level = 1, int maxLevel = 100)
        {
            Name = name;
            _maxLevel = maxLevel;
            _currentLevel = ValidateLevel(level: level);
            _experience = ExperienceLevels[key: _currentLevel.Clamp(min: 1, max: MaxLevel)];
        }

        public Leveling(IActor? actor, string name, int level = 1, int maxLevel = 100)
        {
            Name = name;
            Actor = actor;
            _maxLevel = maxLevel;
            _currentLevel = ValidateLevel(level: level);
            _experience = ExperienceLevels[key: _currentLevel.Clamp(min: 1, max: MaxLevel)];
        }

        public Leveling(IActor? actor, string name, int level = -1, int maxLevel = 100, long experience = -1)
        {
            Actor = actor;
            Name = name;
            _maxLevel = maxLevel;
            if (level == -1 && experience == -1)
            {
                _currentLevel = 1; // Default to level 1
                _experience = ExperienceLevels[key: 2]; // Default experience for level 1 to 2
            }
            else if (level == -1)
            {
                _experience = ValidateExperience(experience: experience);
                _currentLevel = CalculateLevelFromExperience(experience: _experience); // Calculate level based on experience
            }
            else
            {
                _currentLevel = ValidateLevel(level: level);
                _experience = experience == -1
                    ? ExperienceLevels[key: _currentLevel.Clamp(min: 1, max: MaxLevel)]
                    : ValidateExperience(experience: experience);
            }
        }


        public virtual void LevelUp()
        {
            if (CurrentLevel >= MaxLevel)
            {
                return;
            }

            CurrentLevel++;
            Experience = ExperienceLevels[key: CurrentLevel];
        }

        public virtual void LevelDown()
        {
            if (CurrentLevel <= 1)
            {
                return;
            }

            CurrentLevel--;
            Experience = ExperienceLevels[key: CurrentLevel];
        }

        public virtual void GainExperience(long amount)
        {
            if (Experience >= ExperienceLevels[key: MaxLevel] || amount <= 0)
            {
                return;
            }

            Experience += amount;
        }

        public virtual void LoseExperience(long amount)
        {
            if (Experience <= 0)
            {
                return;
            }

            Experience -= amount;
        }

        public virtual void SetExperience(long amount)
        {
            Experience = amount;
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: amount, level: _currentLevel,
                    type: LevelingType.SetExperience));
        }

        public virtual void SetLevel(int level)
        {
            CurrentLevel = level;
            _experience = ExperienceLevels[key: _currentLevel.Clamp(min: 1, max: MaxLevel)];
            MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: Experience, level: _currentLevel,
                    type: LevelingType.SetLevel));
        }

        public virtual void SetMaxLevel(int level)
        {
            MaxLevel = level;
        }

        public virtual int ValidateLevel(int level)
        {
            if (level < 1)
            {
                level = 1;
            }

            if (level > MaxLevel)
            {
                level = MaxLevel;
            }

            while (_currentLevel < level)
            {
                _currentLevel++;
                MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                    message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: Experience, level: _currentLevel,
                        type: LevelingType.GainLevel));
            }

            while (_currentLevel > level)
            {
                _currentLevel--;
                MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                    message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: Experience, level: _currentLevel,
                        type: LevelingType.LoseLevel));
            }

            return _currentLevel;
        }


        public virtual void ValidateLevel()
        {
            while (Experience >= ExperienceLevels[key: CurrentLevel + 1] && CurrentLevel < MaxLevel)
            {
                _currentLevel++;
                MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                    message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: Experience, level: _currentLevel,
                        type: LevelingType.GainLevel));
            }

            while (Experience < ExperienceLevels[key: CurrentLevel] && CurrentLevel > 1)
            {
                _currentLevel--;
                MessageSystem.MessageManager.SendImmediate(channel: MessageChannels.Level,
                    message: new LevelingMessage(senderIdentity: this, actor: Actor, experience: Experience, level: _currentLevel,
                        type: LevelingType.LoseLevel));
            }
        }

        public virtual long ValidateExperience(long experience)
        {
            if (experience < 0)
            {
                return 0;
            }

            if (experience > ExperienceLevels[key: MaxLevel])
            {
                return ExperienceLevels[key: MaxLevel];
            }

            return experience;
        }

        public virtual int CalculateLevelFromExperience(long experience)
        {
            for (var level = 1; level <= MaxLevel; level++)
            {
                if (experience < ExperienceLevels[key: level])
                {
                    return level - 1;
                }
            }

            return MaxLevel; // If experience is higher than max level's requirement
        }

        public override string ToString()
        {
            var nextLevelExperience = CurrentLevel >= MaxLevel ? 0 : ExperienceLevels[key: CurrentLevel + 1];
            return $"Level: {CurrentLevel}, Experience: {Experience}/{nextLevelExperience}";
        }

        public static Dictionary<int, long> GenerateExperienceLevels
        (
            int maxLevel = 100,
            long baseExperience = 50,
            double exponent = 1.25
        )
        {
            var levels = new Dictionary<int, long>();

            for (var i = 1; i <= maxLevel; i++)
            {
                var experience =
                    (long)Math.Round(value: baseExperience * Math.Pow(x: i - 1, y: exponent), mode: MidpointRounding.AwayFromZero);
                levels.Add(key: i, value: experience);
            }

            return levels;
        }

        protected int _currentLevel = 1;

        protected long _experience;

        protected int _maxLevel = DefaultMaxLevel;
    }
}