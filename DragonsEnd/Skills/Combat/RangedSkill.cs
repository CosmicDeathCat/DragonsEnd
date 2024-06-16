using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat
{
    public class RangedSkill : BaseCombatSkill
    {
        public RangedSkill(string name, IActor actor, int maxLevel = 20) : base(name, actor, maxLevel)
        {
        }
    }
}