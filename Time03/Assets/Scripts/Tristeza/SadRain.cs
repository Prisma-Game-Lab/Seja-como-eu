using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadRain : MonoBehaviour
{
    public int NumeroGotas;

    public float RainInterval;

    public float Cooldown;

    public GameObject PrefabGota;

    private bool isReady = false;

    void Start()
    {
        StartCoroutine(StartRain());
    }

    
    void Update()
    {
        
    }

    public void Rain() {
        isReady = false;
        StartCoroutine(SlowRain());
    }

    private IEnumerator SlowRain() {
        float xPosition;
        float zPosition;
        for (int i = 0; i < NumeroGotas; i++) {
            xPosition = Random.Range(-19,19);
            zPosition = Random.Range(-19,19);
            Instantiate(PrefabGota, new Vector3(xPosition, 15, zPosition), Quaternion.identity);
            yield return new WaitForSeconds(RainInterval);                                                                
        }
        yield return new WaitForSeconds(Cooldown);
        isReady = true;
    }

    private IEnumerator StartRain() {
        yield return new WaitForSeconds(Cooldown);
        isReady = true;
    }

    public bool RainReady() {
        return isReady;
    }
}
