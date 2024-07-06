using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Skills.Combat.Ranged
{
    public class RangedSkill : BaseCombatSkill
    {
        public RangedSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }

        public override SkillType SkillType => SkillType.Ranged;
    }
}