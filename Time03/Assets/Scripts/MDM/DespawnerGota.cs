using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerGota : MonoBehaviour
{

    public GameObject gotaParticle;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Finish")) {
            Debug.Log("BATEUUUUUUUUU");
            Instantiate(gotaParticle, this.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }
    }
}
