using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat
{
    public abstract class BaseCombatSkill : BaseSkill
    {
        public BaseCombatSkill(string name, IActor? actor = null, int startingLevel = 1, int maxLevel = 100) : base(name: name, actor: actor, startingLevel: startingLevel, maxLevel: maxLevel)
        {
        }
    }
}