using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Сlothes : Item
{
    public enum ClothesType{
        Header,
        Body
    }
    private ClothesType clothesType;
    private float protection;
    private float prestige; 

    public Сlothes(string Name,float Durability, float Protection,float Prestige,ClothesType ClothesType,Mesh ItemMesh)
    {
        name = Name;
        protection = Protection; 
        prestige = Prestige;      
        durability = Durability;
        clothesType = ClothesType;
        itemMesh = ItemMesh;
    }

    public float GetPrestige(Сlothes clothes){
        return clothes.prestige;
    }
    public float GetProtection(Сlothes clothes){
        return clothes.protection;
    }
    public void UpdateClothes(Mesh newClothes,Сlothes clothes){
        if(CanUpdate(clothes))
            clothes.itemMesh = newClothes;
        else{
            Debug.LogError("Can't update this clothes.Use clothes.CanUpdate()");
        }
    }
    public bool CanUpdate(Сlothes clothes){
        return Mathf.Approximately(clothes.durability,100);
    }
    public ClothesType GetGlothesType(Сlothes clothes){
        return clothes.clothesType;
    }

}
