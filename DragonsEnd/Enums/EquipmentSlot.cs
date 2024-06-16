using System;

namespace DragonsEnd.Enums
{
    [Flags] // Allows combination of values
    public enum EquipmentSlot
    {
        None = 0,
        Head = 1 << 0,
        Body = 1 << 1,
        Hands = 1 << 2,
        Feet = 1 << 3,
        MainHand = 1 << 4,
        OffHand = 1 << 5,
        TwoHanded = MainHand | OffHand // Combines MainHand and OffHand
    }
}


// [System.Flags]
// public enum EquipmentSlot
// {
//     Head, 
//     Body,
//     Hands, 
//     Feet,
//     MainHand,
//     OffHand,
//     TwoHanded
// }
// public enum EquipmentSlot
// {
//     None = 0,
//     Head = 1 << 0,
//     Body = 1 << 1,
//     Hands = 1 << 2,
//     Feet = 1 << 3,
//     MainHand = 1 << 4,
//     OffHand = 1 << 5,
//     TwoHanded = OffHand | MainHand
// }