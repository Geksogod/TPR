using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BuildingType
    {
        Storage
    }
    [SerializeField]
    private BuildingType buildingType;

    public BuildingType GetBuildingType()
    {
        return buildingType;
    }
}
