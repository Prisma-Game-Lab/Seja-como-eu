using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject Player;
    private MovimentPlayer mp;
    void Start()
    {
        mp = Player.GetComponent<MovimentPlayer>();
    }

    
    void Update()
    {
        
    }

    public void TriggerConversation() {
        mp.enabled = false;
    }

    public void EndConversation() {
        mp.enabled = true;
    }
}
