using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.NonCombat
{
    public class BaseNonCombatSkill : BaseSkill
    {
        public BaseNonCombatSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}