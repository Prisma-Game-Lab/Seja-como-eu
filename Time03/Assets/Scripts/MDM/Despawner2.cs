using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner2 : MonoBehaviour
{
    public float TimetoDespawn;

    void Start()
    {    
        StartCoroutine(Despawn2());
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish")) {
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Despawn2() {
        yield return new WaitForSeconds(TimetoDespawn);
        Destroy(gameObject);
    }
}
