using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Skills.Combat.Melee
{
    public class MeleeSkill : BaseCombatSkill
    {
        public MeleeSkill(string name, IActor actor, int maxLevel = 100) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
        
        public override SkillType SkillType => SkillType.Melee;

    }
}