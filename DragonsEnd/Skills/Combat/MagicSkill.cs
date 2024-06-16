using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat
{
    public class MagicSkill : BaseCombatSkill
    {
        public MagicSkill(string name, IActor actor, int maxLevel = 20) : base(name, actor, maxLevel)
        {
        }
    }
}