using DragonsEnd.Enums;

namespace DragonsEnd.Skills
{
    public struct SkillLevels
    {
        public SkillType SkillType { get; }
        public int Level { get; }

        public SkillLevels(SkillType skillType, int level)
        {
            SkillType = skillType;
            Level = level;
        }
    }
}