using System.Collections.Generic;
using DragonsEnd.Items.Interfaces;
using DragonsEnd.Items.Loot.Interfaces;
using DragonsEnd.Skills;

namespace DragonsEnd.Items.Loot
{
    public class LootConfig : ILootConfig
    {
        public virtual long MinItemAmountDrop { get; set; }
        public virtual long MaxItemAmountDrop { get; set; }
        public virtual long MinGold { get; set; }
        public virtual long MaxGold { get; set; }
        public virtual long MinCombatExperience { get; set; }
        public virtual long MaxCombatExperience { get; set; }
        public virtual List<SkillExperience>? SkillExperiences { get; set; }
        public virtual List<IItem>? LootableItems { get; set; }

        public LootConfig
        (
            long minItemAmountDrop = -1L,
            long maxItemAmountDrop = -1L,
            long minGold = -1L,
            long maxGold = -1L,
            long minCombatExperience = -1L,
            long maxCombatExperience = -1L,
            List<SkillExperience>? skillExperiences = null,
            List<IItem>? lootableItems = null
        )
        {
            MinItemAmountDrop = minItemAmountDrop;
            MaxItemAmountDrop = maxItemAmountDrop;
            MinGold = minGold;
            MaxGold = maxGold;
            MinCombatExperience = minCombatExperience;
            MaxCombatExperience = maxCombatExperience;
            SkillExperiences = skillExperiences;
            LootableItems = lootableItems;
        }
    }
}