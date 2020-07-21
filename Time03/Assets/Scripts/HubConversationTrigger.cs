using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubConversationTrigger : MonoBehaviour
{
    private DisplayFrase DF;
    private GeneralCounts Counts;
    private int rnd;
    private int[] IndexList = {14,16,18,20,22,24,26,28,30};
    private bool firstTime;
    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player")) 
        {
            DF = other.gameObject.GetComponent<DisplayFrase>();
            firstTime=true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(firstTime && other.gameObject.CompareTag("Player") && Input.GetAxis("Dash") == 1)  
        {
            if(!Counts.CarinhoIsMorto || !Counts.TristezaIsMorto || !Counts.ExpressividadeIsMorto) 
                {
                if(Counts.Events["PrimeiraConversa"]) 
                {
                    DF.Trigger.TriggerConversation(0,"PrimeiraConversa");
                }
                else 
                {
                    rnd = Random.Range(0,IndexList.Length);
                    DF.Trigger.TriggerConversation(IndexList[rnd],"FraseRandom");
                }
            }
            else 
            {
                if(Counts.Events["DialogoFinal"]) 
                {
                    DF.Trigger.TriggerConversation(32,"DialogoFinal");
                }
                else 
                {
                    DF.Trigger.TriggerConversation(78,"DialogoFinal");
                }
                
            }
            firstTime=false;
        }
    }
}
