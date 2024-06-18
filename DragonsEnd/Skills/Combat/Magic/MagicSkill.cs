using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat.Magic
{
    public class MagicSkill : BaseCombatSkill
    {
        public MagicSkill(string name, IActor actor, int maxLevel = 100) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
    }
}