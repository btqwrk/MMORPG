using System;
using UnityEngine;

[Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public Item(int id, string name, string description, Sprite icon)
    {
        itemID = id;
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
    }
}
