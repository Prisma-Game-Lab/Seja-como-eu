using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDespawn : MonoBehaviour
{
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Finish")) {
            Destroy(gameObject);
        }
    }
}
