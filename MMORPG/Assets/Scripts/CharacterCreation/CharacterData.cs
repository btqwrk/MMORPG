using UnityEngine;
using MMOClassLib;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Custom/Character Data")]
public class CharacterData : ScriptableObject
{
    public int playerID;
    public string name;
    public int level;
    public string characterClass;
    public string race;
    public string gender;
    public int hp;
    public int resource;
    public int stamina;
    public int intelligence;
    public int agility;
    public int wisdom;
    public int strength;
    public int will;
    public float accuracy;
    public int vitality;
    public int headArmorID;
    public int chestArmorID;
    public int shoulderGuardsID;
    public int wristGuardsID;
    public int glovesID;
    public int beltID;
    public int legsArmorID;
    public int feetArmorID;
    public int mainWeaponID;
    public int offHandID;
}
