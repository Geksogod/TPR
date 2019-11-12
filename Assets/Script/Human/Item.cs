using System;
using UnityEngine;
using static MainResources;

[Serializable]
public class Item 
{
    public string name;
    public float durability;
    public enum ItemType
    {
        WorkTool,
        Resource
    }
    public ItemType itemType;
    public TypeResources typeResources;
    public float benefit;

    /// <summary>
    /// Resurces
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="ItemType"></param>
    public Item(string Name, TypeResources TypeResources)
    {
        name = Name;
        typeResources = TypeResources;
        itemType = ItemType.Resource;
        durability = 100;
        benefit = 0;
    }
    /// <summary>
    /// Item (you can't create Reasurces)
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="ItemType"></param>
    /// <param name="Durability"></param>
    /// <param name="Benefit"></param>
    public Item(string Name, ItemType ItemType, float Durability, float Benefit)
    {
        if (ItemType == ItemType.Resource)
            return;
        name = Name;
        itemType = ItemType;
        durability = Durability;
        benefit = Benefit;
    }
}

