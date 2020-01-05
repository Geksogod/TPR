using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainResources;

public class Resource : Item
{
    public enum TypeResources{
        wood
    }
    public TypeResources typeResources;
    public Resource( string Name,TypeResources TypeResources)
    {
        name = Name;
        typeResources = TypeResources;
    }
}
