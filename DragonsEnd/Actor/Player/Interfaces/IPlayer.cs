using System.Collections.Generic;
using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Actor.Player.Interfaces
{
    public interface IPlayer : IActor
    {
        IActor? ChooseTarget(List<IActor> targets);
    }
}