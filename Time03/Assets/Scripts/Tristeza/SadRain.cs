using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadRain : MonoBehaviour
{
    public int NumeroGotas;

    public float RainInterval;

    public float Cooldown;

    public GameObject PrefabGota;
    public GameObject PrefabBulletGota;
    public GameObject PrefabHelper;
    public int numeroBullets;

    private bool isReady = false;

    void Start()
    {
        StartCoroutine(StartRain());
        PrefabHelper.transform.localScale.Set(PrefabGota.transform.localScale.x,0.05f,PrefabGota.transform.localScale.z);
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
            GameObject rain = Instantiate(PrefabGota, new Vector3(xPosition, 15, zPosition), Quaternion.identity);
            Instantiate(PrefabHelper, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
            StartCoroutine(RainSplit(rain));
            yield return new WaitForSeconds(RainInterval);                                                                
        }
        yield return new WaitForSeconds(Cooldown);
        isReady = true;
    }

    private IEnumerator StartRain() {
        yield return new WaitForSeconds(Cooldown);
        isReady = true;
    }

    private IEnumerator RainSplit(GameObject gota)
    {
        yield return new WaitForSeconds(1.7f);

        int Arc = 360 / numeroBullets;
        for(int i = 0; i < 360; i += Arc)
        {
            Instantiate(PrefabBulletGota, gota.transform.position, Quaternion.AngleAxis(i, Vector3.up));
        }
    }

    public bool RainReady() {
        return isReady;
    }
}
