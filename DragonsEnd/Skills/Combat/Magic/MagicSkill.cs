using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Skills.Combat.Magic
{
    public class MagicSkill : BaseCombatSkill
    {
        public MagicSkill(string name, IActor actor, int maxLevel = 100) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
        
        public override SkillType SkillType => SkillType.Magic;

    }
}