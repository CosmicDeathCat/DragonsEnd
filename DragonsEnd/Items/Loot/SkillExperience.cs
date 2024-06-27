using System;
using DragonsEnd.Enums;
using DragonsEnd.Utility.Extensions.Random;

namespace DragonsEnd.Items.Loot
{
    public struct SkillExperience
    {
        public SkillType SkillType { get; }
        public long MinExperience { get; }
        public long MaxExperience { get; }
        public long Experience { get; }

        public SkillExperience(SkillType skillType, long minExperience, long maxExperience)
        {
            var rnd = new Random();
            SkillType = skillType;
            MinExperience = minExperience;
            MaxExperience = maxExperience;
            Experience = rnd.NextInt64(minExperience, maxExperience + 1);
        }

        public SkillExperience(SkillType skillType, long experience)
        {
            SkillType = skillType;
            MinExperience = experience;
            MaxExperience = experience;
            Experience = experience;
        }
    }
}