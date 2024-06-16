using DragonsEnd.Actor.Interfaces;
using DragonsEnd.Stats.Leveling.Interfaces;

namespace DragonsEnd.Skills.Interfaces
{
    public interface ISkill
    {
        string Name { get; set; }
        ILeveling Leveling { get; set; }
        IActor Actor { get; set; }
    }
}