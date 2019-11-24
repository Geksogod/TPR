using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainResources;

public class Resource : Item
{
    public new string name;
    public new TypeResources typeResources;
    public Resource(string Name, TypeResources TypeResources)
        :base(Name, TypeResources)
    {
        name = Name;
        typeResources = TypeResources;
    }
}
