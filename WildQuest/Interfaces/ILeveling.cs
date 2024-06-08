using System.Collections.Generic;

namespace WildQuest.Interfaces;

public interface ILeveling
{
    int CurrentLevel { get; set; }
    int MaxLevel { get; set; }
    long Experience { get; set; }
    long ExperienceToNextLevel { get; }
    Dictionary<int, long> ExperienceLevels { get; set; }
    void LevelUp();
    void LevelDown();
    void GainExperience(long amount);
    void LoseExperience(long amount);
    void SetExperience(long amount);
    void SetLevel(int level);
    void SetMaxLevel(int level);
    void ValidateLevel();
    int ValidateLevel(int level);
    long ValidateExperience(long experience);
    string ToString();
    
}