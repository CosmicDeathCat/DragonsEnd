using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.NonCombat
{
    public class BaseNonCombatSkill : BaseSkill
    {
        public BaseNonCombatSkill(string name, IActor actor, int maxLevel = 100) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
    }
}