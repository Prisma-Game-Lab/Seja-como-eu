using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsNextPiece : MonoBehaviour
{
 
    public Tentaculo tt;
    private int spawns;
    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.CompareTag("TentPiece") || other.transform.CompareTag("WavyPiece"))
        {
            tt.spawnTentacle();
            spawns++;
            if(spawns>9000)
            {
                Debug.Log("Too Many Pieces!");
            }
        }
    }
}
