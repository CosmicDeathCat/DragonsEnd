using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.NonCombat
{
    public class MiningSkill : BaseNonCombatSkill
    {
        public MiningSkill(string name, IActor actor, int maxLevel = 20) : base(name, actor, maxLevel)
        {
        }
    }
}