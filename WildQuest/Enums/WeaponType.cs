namespace WildQuest.Enums;

/// <summary>
///  The type of weapon that a character can use.
///  This is used to determine what weapons a character can use based on their class.
/// </summary>
public enum WeaponType
{
    //Melee Weapons
    Dagger, // Usable by all classes
    Sword, // Only usable by Warrior
    GreatSword, // Only usable by Warrior
    // Ranged Weapons
    ThrowingKnife, // Usable by all classes
    Bow, // Only Usable by Archer
    Crossbow, // Only Usable by Archer
    // Magic Weapons
    Staff, // Usable by all classes
    Wand, // Only Usable by Mage 
    Grimoire // Only Usable by Mage
}