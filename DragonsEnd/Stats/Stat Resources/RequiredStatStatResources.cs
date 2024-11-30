using System.Collections.Generic;
using DragonsEnd.Stats.Stat_Resources.Interfaces;

namespace DragonsEnd.Stats.Stat_Resources
{
    public class RequiredStatStatResources : IRequiredStatResources
    {
        public List<IRequiredStatResourceContainer> RequiredResources { get; set; }
    }
}