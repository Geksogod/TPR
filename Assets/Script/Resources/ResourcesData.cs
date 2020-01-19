using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Resources Data", order = 1)][System.Serializable]
public class ResourcesData : ScriptableObject
{
    [SerializeField]
    private string resourcesName;
    [SerializeField]
    private Sprite icon;
    public enum TypeResources{
        wood
    }
    public TypeResources typeResources;
}
