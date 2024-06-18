using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat
{
    public abstract class BaseCombatSkill : BaseSkill
    {
        public BaseCombatSkill(string name, IActor actor, int maxLevel = 20) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
    }
}