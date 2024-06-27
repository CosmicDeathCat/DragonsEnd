using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Skills.Combat.Ranged
{
    public class RangedSkill : BaseCombatSkill
    {
        public RangedSkill(string name, IActor actor, int maxLevel = 100) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
        
        public override SkillType SkillType => SkillType.Ranged;

    }
}