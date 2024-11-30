using DragonsEnd.Enums;

namespace DragonsEnd.Stats.Stat_Resources.Interfaces
{
    public interface IRequiredStatResourceContainer
    {
        StatResourceType Type { get; set; }
        int RequiredAmount { get; set; }
    }
}