using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject player;

    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    
    void Update()
    {
        if(GeneralCounts.Kill) {
            Death();
        }   
    }

    public void Death() {
        player.GetComponent<RagdollController>().DoRagdoll(true);
        player.GetComponent<MovimentPlayer>().enabled = false;
        DeathScreen.SetActive(true);
        Counts.DeathCount++;
    }
}
