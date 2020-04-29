using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DeathScreen;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(GeneralCounts.Kill) {
            Death();
            GeneralCounts.Kill = false;
        }   
    }

    public void Death() {
        DeathScreen.SetActive(true);
        GeneralCounts.DeathCount++;
        Time.timeScale = 0;
    }
}
