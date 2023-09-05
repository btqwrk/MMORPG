using System;
using UnityEngine;

[Serializable]
public class Weapon : Item
{
    public int damage;

    public Weapon(int id, string name, string description, Sprite icon, int weaponDamage)
        : base(id, name, description, icon)
    {
        damage = weaponDamage;
    }
}
