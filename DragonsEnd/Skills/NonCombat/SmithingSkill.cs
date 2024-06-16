using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.NonCombat
{
    public class SmithingSkill : BaseNonCombatSkill
    {
        public SmithingSkill(string name, IActor actor, int maxLevel = 20) : base(name, actor, maxLevel)
        {
        }
    }
}