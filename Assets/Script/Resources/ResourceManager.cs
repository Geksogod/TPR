using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private int woodCount;

    public void AddResorces(Resource newResources)
    {
        switch (newResources.GetResourcesType())
        {
            case ResourcesData.TypeResources.wood:
                woodCount++;
                break;
        }
    }
    public void RemoveResorces(Resource newResources)
    {
        switch (newResources.GetResourcesType())
        {
            case ResourcesData.TypeResources.wood:
                woodCount--;
                break;
        }
    }

    public int GetResourcesCount(ResourcesData.TypeResources resourceType){
        switch (resourceType)
        {
            case ResourcesData.TypeResources.wood:
                return woodCount;
        }
        return int.MinValue;
    }
}
