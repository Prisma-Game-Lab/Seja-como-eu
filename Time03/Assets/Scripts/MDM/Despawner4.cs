using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner4 : MonoBehaviour
{
    public float TimetoDespawn;

    void Start()
    {
        StartCoroutine(Despawn4());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Despawn4() {
        yield return new WaitForSeconds(TimetoDespawn);
        Destroy(gameObject);
    }
}
