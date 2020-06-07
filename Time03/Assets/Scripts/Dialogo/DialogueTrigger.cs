using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject Player;
    private MovimentPlayer mp;
    private bool EventTriggered = false;
    private GeneralCounts Counts;

    void Start()
    {
        mp = Player.GetComponent<MovimentPlayer>();
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    
    void Update()
    {
        
    }

    public void TriggerConversation(int index, string Key) {
        if(!Counts.Events[Key]) return;
        mp.enabled = false;
        EventTriggered = true;
        Counts.Index = index;
        Counts.Events[Key] = false;
    }

    public void EndConversation() {
        mp.enabled = true;
        EventTriggered = false;
    }

    public bool CanChat() {
        return EventTriggered;
    }
}
