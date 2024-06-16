using System;
using System.Collections.Generic;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;
using DragonsEnd.Messaging.Messages;
using DragonsEnd.Stats.Leveling.Interfaces;
using DragonsEnd.Utility.Extensions.Int;

namespace DragonsEnd.Stats.Leveling
{
    //TODO: Handle types of levels like skills and regular combat levels
    public class Leveling : ILeveling
    {
        protected int _currentLevel = 1;

        protected long _experience;

        protected int _maxLevel = DefaultMaxLevel;

        public Leveling(IActor actor, int maxLevel = 100)
        {
            Actor = actor;
            _maxLevel = maxLevel;
        }

        public Leveling(IActor actor, int maxLevel, int level = 1)
        {
            Actor = actor;
            _maxLevel = maxLevel;
            _currentLevel = ValidateLevel(level);
            _experience = ExperienceLevels[(_currentLevel + 1).Clamp(1, MaxLevel)];
        }

        public Leveling(IActor actor, int maxLevel, int level = -1, long experience = -1)
        {
            Actor = actor;
            _maxLevel = maxLevel;
            if (level == -1 && experience == -1)
            {
                _currentLevel = 1; // Default to level 1
                _experience = ExperienceLevels[2]; // Default experience for level 1 to 2
            }
            else if (level == -1)
            {
                _experience = ValidateExperience(experience);
                _currentLevel = CalculateLevelFromExperience(_experience); // Calculate level based on experience
            }
            else
            {
                _currentLevel = ValidateLevel(level);
                _experience = experience == -1
                    ? ExperienceLevels[(_currentLevel + 1).Clamp(1, MaxLevel)]
                    : ValidateExperience(experience);
            }
        }

        public static long BaseExperience { get; set; } = 50;
        public static double ExperienceExponent { get; set; } = 1.25;
        public static int DefaultMaxLevel { get; set; } = 100;

        public static double DefaultLevelingThreshold { get; set; } = 10.00;

        public double LevelingThreshold { get; set; } = DefaultLevelingThreshold;

        public IActor Actor { get; set; }

        public virtual int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = ValidateLevel(value);
        }

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
                _experience = ValidateExperience(value);
                if (_experience > oldExperience)
                {
                    MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                        new LevelingMessage(this,Actor, _experience - oldExperience, CurrentLevel,
                            LevelingType.GainExperience));
                }
                else if (_experience < oldExperience)
                {
                    MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                        new LevelingMessage(this,Actor, oldExperience - _experience, CurrentLevel,
                            LevelingType.LoseExperience));
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

                return ExperienceLevels[CurrentLevel + 1] - Experience;
            }
        }

        public Dictionary<int, long> ExperienceLevels { get; set; } =
            GenerateExperienceLevels(DefaultMaxLevel, BaseExperience, ExperienceExponent);


        public virtual void LevelUp()
        {
            if (CurrentLevel >= MaxLevel)
            {
                return;
            }

            CurrentLevel++;
            Experience = ExperienceLevels[CurrentLevel];
        }

        public virtual void LevelDown()
        {
            if (CurrentLevel <= 1)
            {
                return;
            }

            CurrentLevel--;
            Experience = ExperienceLevels[CurrentLevel];
        }

        public virtual void GainExperience(long amount)
        {
            if (Experience >= ExperienceLevels[MaxLevel])
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
            MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                new LevelingMessage(this,Actor, amount, _currentLevel, LevelingType.SetExperience));
        }

        public virtual void SetLevel(int level)
        {
            CurrentLevel = level;
            _experience = ExperienceLevels[(_currentLevel + 1).Clamp(1, MaxLevel)];
            MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                new LevelingMessage(this,Actor, Experience, _currentLevel, LevelingType.SetLevel));
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
                MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                    new LevelingMessage(this,Actor, Experience, _currentLevel, LevelingType.GainLevel));
            }

            while (_currentLevel > level)
            {
                _currentLevel--;
                MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                    new LevelingMessage(this,Actor, Experience, _currentLevel, LevelingType.LoseLevel));
            }

            return _currentLevel;
        }


        public virtual void ValidateLevel()
        {
            while (Experience >= ExperienceLevels[CurrentLevel + 1] && CurrentLevel < MaxLevel)
            {
                _currentLevel++;
                MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                    new LevelingMessage(this, Actor, Experience, _currentLevel, LevelingType.GainLevel));
            }

            while (Experience < ExperienceLevels[CurrentLevel] && CurrentLevel > 1)
            {
                _currentLevel--;
                MessageSystem.MessageManager.SendImmediate(MessageChannels.Level,
                    new LevelingMessage(this, Actor, Experience, _currentLevel, LevelingType.LoseLevel));
            }
        }

        public virtual long ValidateExperience(long experience)
        {
            if (experience < 0)
            {
                return 0;
            }

            if (experience > ExperienceLevels[MaxLevel])
            {
                return ExperienceLevels[MaxLevel];
            }

            return experience;
        }

        public virtual int CalculateLevelFromExperience(long experience)
        {
            for (var level = 1; level <= MaxLevel; level++)
            {
                if (experience < ExperienceLevels[level])
                {
                    return level - 1;
                }
            }

            return MaxLevel; // If experience is higher than max level's requirement
        }

        public override string ToString()
        {
            return $"Level: {CurrentLevel}, Experience: {Experience}/{ExperienceToNextLevel}";
        }

        public static Dictionary<int, long> GenerateExperienceLevels(int maxLevel = 100, long baseExperience = 50,
            double exponent = 1.25)
        {
            var levels = new Dictionary<int, long>();

            for (var i = 1; i <= maxLevel; i++)
            {
                var experience =
                    (long)Math.Round(baseExperience * Math.Pow(i - 1, exponent), MidpointRounding.AwayFromZero);
                levels.Add(i, experience);
            }

            return levels;
        }
    }
}