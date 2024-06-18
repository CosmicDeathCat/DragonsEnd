﻿using DragonsEnd.Actor.Interfaces;

namespace DragonsEnd.Skills.Combat.Melee
{
    public class MeleeSkill : BaseCombatSkill
    {
        public MeleeSkill(string name, IActor actor, int maxLevel = 20) : base(name: name, actor: actor, maxLevel: maxLevel)
        {
        }
    }
}