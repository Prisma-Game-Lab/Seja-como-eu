using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) {
            rend.material.color = Color.red;
        }
        if(other.gameObject.CompareTag("Caringo")) {
            rend.material.color = Color.magenta;
        }
    }
}
