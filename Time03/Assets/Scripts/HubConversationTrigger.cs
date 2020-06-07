using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubConversationTrigger : MonoBehaviour
{
    private DisplayFrase DF;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        DF = other.gameObject.GetComponent<DisplayFrase>();
        if(other.gameObject.CompareTag("Player")) {
            DF.Trigger.TriggerConversation(0,"PrimeiraConversa");
        }
    }
}
