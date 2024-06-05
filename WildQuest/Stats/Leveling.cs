using System.Collections.Generic;
using DLS.MessageSystem;
using DLS.MessageSystem.Messaging.MessageChannels.Enums;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Messaging.Messages;

namespace WildQuest.Stats;

public class Leveling : ILeveling
{
    public static int BaseExperience { get; set; } = 50;
    public static double ExperienceExponent { get; set; } = 1.25;
    public static int DefaultMaxLevel { get; set; } = 100;

    public static Dictionary<int, int> GenerateExperienceLevels(int maxLevel = 100, int baseExperience = 50, double exponent = 1.25)
    {
        var levels = new Dictionary<int, int>();

        for (int i = 1; i <= maxLevel; i++)
        {
            levels.Add(i, (int)(baseExperience * System.Math.Pow(i - 1, exponent)));
        }

        return levels;
    }

    protected IActor Actor { get; set; }
    protected int _currentLevel = 1;
    public virtual int CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = ValidateLevel(value);
    }

    protected int _maxLevel = DefaultMaxLevel;
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

    protected int _experience = 0;
    public virtual int Experience
    {
        get => _experience;
        set
        {
            _experience = ValidateExperience(value);
            ValidateLevel();
        }
    }

    public virtual int ExperienceToNextLevel => ExperienceLevels.ContainsKey(CurrentLevel + 1) ? ExperienceLevels[CurrentLevel + 1] : 0;
    public Dictionary<int, int> ExperienceLevels { get; set; } = GenerateExperienceLevels(DefaultMaxLevel, BaseExperience, ExperienceExponent);

    public Leveling(IActor actor)
    {
        Actor = actor;
    }

    public virtual void LevelUp()
    {
        if (CurrentLevel >= MaxLevel) return;
        CurrentLevel++;
        Experience = ExperienceLevels[CurrentLevel];
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, Experience, CurrentLevel, LevelingType.GainLevel));
    }

    public virtual void LevelDown()
    {
        if (CurrentLevel <= 1) return;
        CurrentLevel--;
        Experience = ExperienceLevels[CurrentLevel];
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, Experience, CurrentLevel, LevelingType.LoseLevel));
    }

    public virtual void GainExperience(int amount)
    {
        if (Experience >= ExperienceLevels[MaxLevel]) return;
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, amount, CurrentLevel, LevelingType.GainExperience));
        Experience += amount;
        ValidateLevel();
    }

    public virtual void LoseExperience(int amount)
    {
        if (Experience <= 0) return;
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, amount, CurrentLevel, LevelingType.LoseExperience));
        Experience -= amount;
        ValidateLevel();
    }

    public virtual void SetExperience(int amount)
    {
        Experience = amount;
        ValidateLevel();
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, amount, _currentLevel, LevelingType.SetExperience));
    }

    public virtual void SetLevel(int level)
    {
        CurrentLevel = level;
        MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, Experience, _currentLevel, LevelingType.SetLevel));
    }

    public virtual void SetMaxLevel(int level)
    {
        MaxLevel = level;
    }

    public virtual int ValidateLevel(int level)
    {
        if (level < 1) return 1;
        if (level > MaxLevel) return MaxLevel;
        return level;
    }

    public virtual void ValidateLevel()
    {
        while (Experience >= ExperienceToNextLevel && CurrentLevel < MaxLevel)
        {
            _currentLevel++;
            MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, Experience, _currentLevel, LevelingType.GainLevel));
        }

        while (Experience < ExperienceLevels[CurrentLevel] && CurrentLevel > 1)
        {
            _currentLevel--;
            MessageSystem.MessageManager.SendImmediate(MessageChannels.Level, new LevelingMessage(Actor, Experience, _currentLevel, LevelingType.LoseLevel));
        }
    }

    public virtual int ValidateExperience(int experience)
    {
        if (experience < 0) return 0;
        if (experience > ExperienceLevels[MaxLevel]) return ExperienceLevels[MaxLevel];
        return experience;
    }

    public override string ToString()
    {
        return $"Level: {CurrentLevel}, Experience: {Experience}/{ExperienceToNextLevel}";
    }
}