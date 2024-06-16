using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Skills.Interfaces;
using DragonsEnd.Stats.Leveling;
using DragonsEnd.Stats.Leveling.Interfaces;

namespace DragonsEnd.Skills
{
    public abstract class BaseSkill : ISkill
    {
        public BaseSkill(string name, IActor actor, int maxLevel = 20)
        {
            Name = name;
            Actor = actor;
            Leveling = new Leveling(actor, maxLevel);
        }

        public virtual string Name { get; set; }
        public virtual ILeveling Leveling { get; set; }
        public virtual IActor Actor { get; set; }
    }
}