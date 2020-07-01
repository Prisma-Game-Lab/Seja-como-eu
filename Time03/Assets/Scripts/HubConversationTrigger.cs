using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubConversationTrigger : MonoBehaviour
{
    private DisplayFrase DF;
    private GeneralCounts Counts;
    private int rnd;
    private int[] IndexList = {14,16,18,20,22,24,26,28,30};
    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        DF = other.gameObject.GetComponent<DisplayFrase>();
        if(other.gameObject.CompareTag("Player")) {
            if(!Counts.CarinhoIsMorto || !Counts.TristezaIsMorto || !Counts.ExpressividadeIsMorto) {
                if(Counts.Events["PrimeiraConversa"]) {
                    Debug.Log("entrei");
                    DF.Trigger.TriggerConversation(0,"PrimeiraConversa");
                }
                else {
                    rnd = Random.Range(0,IndexList.Length);
                    DF.Trigger.TriggerConversation(IndexList[rnd],"FraseRandom");
                }
            }
            else {
                DF.Trigger.TriggerConversation(32,"DialogoFinal");
            }
        }
    }
}
