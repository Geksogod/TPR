using System;
using UnityEngine;
using static MainResources;

[Serializable]
public abstract class Item 
{
    protected string name;
    protected float durability;
    public enum ItemType
    {
        WorkTool,
        Resource
    }
    protected ItemType itemType;
    protected TypeResources typeResources;
    protected float benefit;

    /// <summary>
    /// Resurces
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="ItemType"></param>
    protected Item(string Name, TypeResources TypeResources)
    {
        name = Name;
        typeResources = TypeResources;
    }
    /// <summary>
    /// Item (you can't create Reasurces)
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="ItemType"></param>
    /// <param name="Durability"></param>
    /// <param name="Benefit"></param>
    protected Item(string Name, ItemType ItemType, float Durability, float Benefit)
    {
        if (ItemType == ItemType.Resource)
            return;
        name = Name;
        itemType = ItemType;
        durability = Durability;
        benefit = Benefit;
    }
}

