using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventTrigger
{   
    public EventTrigger(EventFunction EventName) {
        ActiveEvent = EventName;
    }

    public Dictionary<string,bool> Event;

    public delegate void EventFunction();   

    private EventFunction ActiveEvent;

    public void Activate() {
        ActiveEvent();
    }  
}
