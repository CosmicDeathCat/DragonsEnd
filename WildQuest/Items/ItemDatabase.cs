using System.Collections.Generic;
using WildQuest.Interfaces;

namespace WildQuest.Items;

public static class ItemDatabase
{
    public static Dictionary<string, IItem> Items { get; set; } =
    [
    ];
}