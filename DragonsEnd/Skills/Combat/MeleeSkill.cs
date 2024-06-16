using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat
{
    public class MeleeSkill : BaseCombatSkill
    {
        public MeleeSkill(string name, IActor actor, int maxLevel = 20) : base(name, actor, maxLevel)
        {
        }
    }
}