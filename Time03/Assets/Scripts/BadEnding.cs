using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnding : MonoBehaviour
{
    
    public GameObject EndingCanvas;
    public GameObject Player;

    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }
    
    void Update()
    {
        if(Counts.Index == 110) {
            EndingCanvas.SetActive(true);
            Player.GetComponent<MovimentPlayer>().enabled = false;
            GetComponent<GameManager>().canPause = false;
            Counts.Events["DialogoFinal"] = true;
        }
    }
}
