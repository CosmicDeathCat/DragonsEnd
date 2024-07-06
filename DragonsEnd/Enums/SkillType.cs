using System;

namespace DragonsEnd.Enums
{
    [Flags]
    public enum SkillType
    {
        None = 0,
        Melee = 1 << 1,
        Ranged = 1 << 2,
        Magic = 1 << 3,
        Alchemy = 1 << 4,
        Cooking = 1 << 5,
        Crafting = 1 << 6,
        Enchanting = 1 << 7,
        Fishing = 1 << 8,
        Fletching = 1 << 9,
        Foraging = 1 << 10,
        Mining = 1 << 11,
        Smithing = 1 << 12,
        Ranching = 1 << 13,
        Woodcutting = 1 << 14
    }
}