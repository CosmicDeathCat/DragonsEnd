using System;
using DragonsEnd.Enums;
using DragonsEnd.Utility.Extensions.Random;

namespace DragonsEnd.Skills
{
    public struct SkillExperience
    {
        public SkillType SkillType { get; set; }
        public long MinExperience { get; set; }
        public long MaxExperience { get; set; }
        public long Experience { get; set; }

        public SkillExperience(SkillType skillType, long minExperience, long maxExperience)
        {
            var rnd = new Random();
            SkillType = skillType;
            MinExperience = minExperience;
            MaxExperience = maxExperience;
            Experience = rnd.NextInt64(minValue: minExperience, maxValue: maxExperience + 1);
        }

        public SkillExperience(SkillType skillType, long experience)
        {
            SkillType = skillType;
            MinExperience = experience;
            MaxExperience = experience;
            Experience = experience;
        }
        
        public void GenerateRandomExperience()
        {
            var rnd = new Random();
            Experience = rnd.NextInt64(minValue: MinExperience, maxValue: MaxExperience + 1);
        }
    }
}