using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePrison : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) {
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
    }
}
