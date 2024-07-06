using System;

namespace DragonsEnd.Enums
{
    [Flags]
    public enum CharacterClassType
    {
        None = 0,

        Freelancer = 1 << 0,

        Warrior = 1 << 1,

        Mage = 1 << 2,

        Archer = 1 << 3,

        All = Freelancer | Warrior | Mage | Archer
    }
}