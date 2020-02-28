using cakeslice;
using System.Collections.Generic;
using UnityEngine;

public class OutlineListener : MonoBehaviour
{
    private MouseMonitor mouseMonitor;
    private Outline outline;
    private List<Outline> outlines = new List<Outline>();
    private GameObject choosenGameObject;
    private GameObject CurrentGameObject
    {
        get { return curretGameObject; }
        set
        {
            if (CanOutline(value))
            {
                if (curretGameObject != value)
                {
                    curretGameObject = value;
                    outline = curretGameObject.GetComponent<Outline>();
                }
                if (!outline.enabled)
                {
                    outline.color = 1;
                    outline.enabled = true;
                }
            }
            else if (curretGameObject != null && value == null)
            {
                if (value == null && !InAList(curretGameObject))
                    outline.enabled = false;
            }
        }
    }
    private GameObject curretGameObject;

    private void Start()
    {
        mouseMonitor = this.gameObject.GetComponent<MouseMonitor>();
    }
    private void LateUpdate()
    {
        if (mouseMonitor != null)
            CurrentGameObject = mouseMonitor.currentGameObject;
    }
    public bool CanOutline(GameObject gameObject)
    {
        if (gameObject == null || gameObject.GetComponent<Outline>() == null)
            return false;
        if (InAList(gameObject))
            return false;
        switch (EventManager.CurrentEvent)
        {
            case EventManager.Events.ChooseResources:
                if (gameObject.GetComponent<ResourceContainer>()&&gameObject.GetComponent<ResourceContainer>().HasResources())
                    return true;
                break;
        }
        return false;
    }
    private bool InAList(GameObject gameObject)
    {
        Outline outline = gameObject.GetComponent<Outline>();
        for (int i = 0; i < outlines.Count; i++)
        {
            if (outlines[i] == outline)
                return true;
        }
        return false;
    }

    public void AddToOutlines(GameObject gameObject)
    {
        if (!CanOutline(gameObject))
            return;
        Outline outline = gameObject.GetComponent<Outline>();
        outline.enabled = true;
        outline.color = 0;
        outlines.Add(outline);
    }
    public void RemoveFromOutline(GameObject removeFrom)
    {
        if (!CanRemoveOutline(removeFrom)){
            Debug.LogError("Error");
            return;
        }
        Outline outline = removeFrom.GetComponent<Outline>();
        outline.enabled = false;
        outline.color = 0;
        outlines.Remove(outline);
    }

    public bool CanRemoveOutline(GameObject removeFrom)
    {
        if (removeFrom.GetComponent<Outline>() == null)
            return false;
        Outline outline = removeFrom.GetComponent<Outline>();
        return outlines.Contains(outline);
    }


}
