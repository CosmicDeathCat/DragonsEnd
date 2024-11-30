using DragonsEnd.Enums;
using DragonsEnd.Stats.Stat_Resources.Interfaces;

namespace DragonsEnd.Stats.Stat_Resources
{
    public class RequiredStatResourceContainer : IRequiredStatResourceContainer
    {
        public StatResourceType Type { get; set; }
        public int RequiredAmount { get; set; }
        public double RequiredAmountPercentage { get; set; }
        
        public RequiredStatResourceContainer(StatResourceType type = StatResourceType.None, int requiredAmount = 0)
        {
            Type = type;
            RequiredAmount = requiredAmount;
        }
    }
}