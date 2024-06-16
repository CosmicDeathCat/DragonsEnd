using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Actor.Enemy.Interfaces
{
    public interface IEnemy : IActor
    {
        new IEnemy Copy();
    }
}