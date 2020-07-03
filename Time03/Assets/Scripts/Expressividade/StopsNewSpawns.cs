using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopsNewSpawns : MonoBehaviour
{
    public GameObject spawner;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.CompareTag("TentPiece"))
        {
            Destroy(spawner);
            enabled=false;
        }
    }
}
