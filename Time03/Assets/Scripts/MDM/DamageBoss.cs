using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    public GameObject MDM;
    public float TimeInside;
    private MDM mdm;
    private float timer;
    private bool Canlose = true;
    void Start()
    {
        mdm = MDM.GetComponent<MDM>();
    }

    
    void Update()
    {
        if(Canlose) {
            timer += Time.deltaTime;
            if(timer >= TimeInside) {
                mdm.PerdeVida();  
                timer = 0;
            }
        }
        else {
            timer = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) {
            Canlose = true;
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) {
            Canlose = false;
        }
    }
}
