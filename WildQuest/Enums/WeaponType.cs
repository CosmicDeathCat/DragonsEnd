namespace WildQuest.Enums;

/// <summary>
/// The type of weapon that a character can use.
/// This is used to determine what weapons a character can use based on their class and equipment slot.
/// </summary>
public enum WeaponType
{
    // Melee Weapons
    Dagger,          // Usable by all classes, equipped to Main Hand or Off Hand  (One Handed) (Melee)
    Sword,           // Usable by Warrior, equipped to Main Hand (One Handed) (Melee)
    GreatSword,      // Usable by Warrior, equipped to Main Hand and Off Hand  (Two Handed) (Melee)
    Claws,           // Usable by all classes, equipped to Main Hand and Off Hand (Two Handed) (Melee)

    // Ranged Weapons
    ThrowingKnife,   // Usable by all classes, equipped to Main Hand or Off Hand (One Handed) (Ranged)
    Bow,             // Usable by Archer, equipped to Main Hand and Off Hand (Two Handed) (Ranged)
    Crossbow,        // Usable by Archer, equipped to Main Hand and Off Hand (Two Handed) (Ranged)
    Firearm,         // Usable by all classes, equipped to Main Hand (One Handed) (Ranged)

    // Magic Weapons
    Staff,           // Usable by all classes, equipped to Main Hand and Off Hand (Two Handed) (Magic)
    Wand,            // Usable by all classes, equipped to Main Hand (One Handed) (Magic)
    Grimoire,        // Usable by Mage, equipped to Off Hand (One Handed) (Magic)
    Scepter,         // Usable by Mage, equipped to Main Hand (One Handed) (Magic)

    //TODO: Maybe add these later on when there a need for them
    // // Hybrid Weapons
    // Whip,            // Usable by Warrior and Archer, equipped to Main Hand or Off Hand (One Handed) (Melee/Ranged)
    // Spellbow,        // Usable by Archer and Mage, equipped to Main Hand and Off Hand (Two Handed) (Ranged/Magic)
    // Spellblade,      // Usable by Warrior and Mage, equipped to Main Hand (One Handed) (Melee/Magic)
    // Gunblade,        // Usable by Warrior and Archer, equipped to Main Hand (One Handed) (Melee/Ranged)
}