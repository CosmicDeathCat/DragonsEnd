using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Skills.Combat.Magic
{
    public class MagicSkill : BaseCombatSkill
    {
        public override SkillType SkillType { get => SkillType.Magic; }

        public MagicSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}