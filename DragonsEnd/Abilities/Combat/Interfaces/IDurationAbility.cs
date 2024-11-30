using DragonsEnd.Abilities.Interfaces;
using DragonsEnd.Enums;

namespace DragonsEnd.Abilities.Combat.Interfaces
{
    public interface IDurationAbility : IAbility
    {
        DurationType DurationType { get; set; }
        int MinDuration { get; set; }
        int MaxDuration { get; set; }
        int CurrentDuration { get; set; }
        bool IsDurationRandom { get; set; }
        bool IsDurationComplete { get; }
        void StartDuration();
        void UpdateDuration();
        void EndDuration();
        void HandleCustomDuration();

    }
}