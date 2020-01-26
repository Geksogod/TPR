using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HumanHelper
{
    public static Building FindNearBuilding(Building.BuildingType typeBuilding, Vector3 currentPosition)
    {
        if (GameObject.FindObjectsOfType<Building>() == null)
            return null;
        float distanse = float.MaxValue;
        Building findBuilding = null;

        Building[] buildings = GameObject.FindObjectsOfType<Building>();
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].GetBuildingType() == typeBuilding)
            {
                Vector3 buildingsPosition = buildings[i].gameObject.transform.position;
                if (Vector3.Distance(buildingsPosition, currentPosition) < distanse)
                {
                    findBuilding = buildings[i];
                    distanse = Vector3.Distance(buildingsPosition, currentPosition);
                }
            }
        }
        return findBuilding;

    }
}
