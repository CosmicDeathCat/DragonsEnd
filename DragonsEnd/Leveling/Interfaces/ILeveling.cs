using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Identity.Interfaces;

namespace DragonsEnd.Leveling.Interfaces
{
    public interface ILeveling : IIdentity
    {
        int CurrentLevel { get; set; }
        int MaxLevel { get; set; }
        long Experience { get; set; }
        IActor? Actor { get; set; }
        long ExperienceToNextLevel { get; }
        Dictionary<int, long> ExperienceLevels { get; set; }
        double LevelingThreshold { get; set; }
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
        int CalculateLevelFromExperience(long experience);
        string ToString();
    }
}