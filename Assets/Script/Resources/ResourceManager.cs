using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private int woodCount;
    [SerializeField]
    private TextMeshProUGUI woodText;

    public void AddResorces(Resource newResources)
    {
        switch (newResources.GetResourcesType())
        {
            case ResourcesData.TypeResources.wood:
                woodCount++;
                break;
        }
        WriteText();
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
    private void WriteText()
    {
        if (woodText != null)
            woodText.text = woodCount.ToString();
    }
}
