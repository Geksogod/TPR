using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public enum Events
    {
        ChooseResources
    }
    public static Events CurrentEvent
    {
        get { return currentEvent; }
    }
    private static Events currentEvent;

    public static void ChangeCurrentEvent(Events eventType)
    {
        if (CurrentEvent != eventType)
            currentEvent = eventType;
        else
            Debug.Log("current event is already "+eventType);
    }

    
}
