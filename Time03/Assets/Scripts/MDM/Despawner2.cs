using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn2());
    }

    // Update is called once per frame
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
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
