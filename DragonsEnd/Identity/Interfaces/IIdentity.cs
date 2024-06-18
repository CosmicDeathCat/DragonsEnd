using System;

namespace DragonsEnd.Identity.Interfaces
{
    public interface IIdentity
    {
        string Name { get; set; }
        Guid ID { get; set; }
    }
}