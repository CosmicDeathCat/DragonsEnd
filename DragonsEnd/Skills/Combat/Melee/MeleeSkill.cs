using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Skills.Combat.Melee
{
    public class MeleeSkill : BaseCombatSkill
    {
        public override SkillType SkillType { get => SkillType.Melee; }

        public MeleeSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}