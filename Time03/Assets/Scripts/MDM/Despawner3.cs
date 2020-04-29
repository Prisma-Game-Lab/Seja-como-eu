using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish")) {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
