using System.Collections.Generic;

namespace WildQuest.Interfaces;

public interface ILeveling
{
    int CurrentLevel { get; set; }
    int MaxLevel { get; set; }
    int Experience { get; set; }
    int ExperienceToNextLevel { get; }
    Dictionary<int, int> ExperienceLevels { get; set; }
    void LevelUp();
    void LevelDown();
    void GainExperience(int amount);
    void LoseExperience(int amount);
    void SetExperience(int amount);
    void SetLevel(int level);
    void SetMaxLevel(int level);
    void ValidateLevel();
    int ValidateLevel(int level);
    int ValidateExperience(int experience);
    
    
    string ToString();
    
}