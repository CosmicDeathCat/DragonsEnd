using System.Collections.Generic;

namespace DragonsEnd.Stats.Stat_Resources.Interfaces
{
    public interface IRequiredStatResources
    {
        List<IRequiredStatResourceContainer> RequiredResources { get; set; }
    }
}