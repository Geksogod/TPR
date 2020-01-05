using System;
using Unity.Collections;
using UnityEngine;
using static MainResources;

[System.Serializable]
public abstract class Item
{
    [SerializeField, ReadOnly]
    protected string name;
    protected float durability;
    protected GameObject itemGameObject;
    protected Mesh itemMesh;
    protected bool isBroken;


    public void Repear(float repearValue,Item item){
        item.durability+=repearValue;
        if(item.durability>100)
            item.durability=100;
    }

    public void BringDamage(float damage, Item item)
    {
        item.durability -= damage;
        if (item.durability <= 0)
            item.isBroken = true;
            
    }
    public string GetName(Item item)
    {
        return item.name;
    }
    public bool HasGameObject(Item item)
    {
        return item.itemGameObject != null;
    }
    public GameObject GetGameObject(Item item)
    {
        if (HasGameObject(item))
        {
            Debug.LogError("This item have'nt GameObject. Use item.HasGameObject()");
            return null;
        }
        return item.itemGameObject;
    }
    public float GetDurability(Item item)
    {
        return item.durability;
    }
    public bool IsBroken(Item item)
    {
        return item.isBroken;
    }
    public bool HasMesh(Item item)
    {
        return item.itemMesh != null;
    }
    public Mesh GetMesh()
    {
        return itemMesh;
    }
}

