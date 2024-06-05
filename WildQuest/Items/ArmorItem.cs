using System.Collections.Generic;
using WildQuest.Enums;
using WildQuest.Interfaces;
using WildQuest.Stats;

namespace WildQuest.Items;

public class ArmorItem : EquipmentItem, IArmorItem
{
    public virtual CombatStyle CombatStyle { get; set; }
    public virtual ArmorType ArmorType { get; set; }
    
    public ArmorItem(string name, string description, int price, ItemType type, List<EquipmentSlot> slots, ActorStats stats, CombatStyle combatStyle, ArmorType armorType, bool stackable = true, int quantity = 1) : base(name, description, price, type, slots, stats, stackable, quantity)
    {
        CombatStyle = combatStyle;
        ArmorType = armorType;
    }
}