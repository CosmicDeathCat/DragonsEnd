namespace DragonsEnd.Enums
{
    [System.Flags]
    public enum ActorScopeType
    {
        None = 0,
        Alive = 1 << 0,
        Dead = 1 << 1,
        All = Alive | Dead
    }
}