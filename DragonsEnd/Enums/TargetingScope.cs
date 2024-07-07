namespace DragonsEnd.Enums
{
    [System.Flags]
    public enum TargetingScope
    {
        None = 0,
        Single = 1 << 0,
        Multiple = 1 << 1,
        All = Single | Multiple
    }
}