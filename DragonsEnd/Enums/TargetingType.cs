namespace DragonsEnd.Enums
{
    [System.Flags]
    public enum TargetingType
    {
        None = 0,
        Self = 1 << 0,
        Ally = 1 << 1,
        Enemy = 1 << 2,
        All = Self | Ally | Enemy
    }
}