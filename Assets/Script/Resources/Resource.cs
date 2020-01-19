using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item
{
    private ResourcesData resourcesData;
    [SerializeField]
    public Resource(ResourcesData resources)
    {
        name = resources.name;
        resourcesData = resources;
    }
    public ResourcesData.TypeResources GetResourcesType(){
        return resourcesData.typeResources;
    }
}
