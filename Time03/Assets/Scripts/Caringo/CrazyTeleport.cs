using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyTeleport : MonoBehaviour
{
    public float Cooldown;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Teleport() {
        List<float> Pos = new List<float>();

        Pos.Add(16);
        Pos.Add(0);
        Pos.Add(-16);

        transform.position = new Vector3(Pos[Random.Range(0,3)],transform.position.y,Pos[Random.Range(0,3)]); 
    }
}
