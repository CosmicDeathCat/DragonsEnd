using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.NonCombat
{
    public class FletchingSkill : BaseNonCombatSkill
    {
        public FletchingSkill(string name, IActor actor, int maxLevel = 20) : base(name, actor, maxLevel)
        {
        }
    }
}