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

    public int NumeroBullets;
    public GameObject PrefabBulletGota;

    void Start()
    {
        StartCoroutine(StartRain());
    }


    void Update()
    {

    }

    public void Rain()
    {
        isReady = false;
        StartCoroutine(SlowRain());
    }

    private IEnumerator SlowRain()
    {
        float xPosition;
        float zPosition;
        for (int i = 0; i < NumeroGotas; i++)
        {
            xPosition = Random.Range(-19, 19);
            zPosition = Random.Range(-19, 19);
            GameObject rain = Instantiate(PrefabGota, new Vector3(xPosition, 15, zPosition), Quaternion.identity);
            StartCoroutine(SplitRain(rain));
            yield return new WaitForSeconds(RainInterval);
        }
        yield return new WaitForSeconds(Cooldown);
        isReady = true;
    }

    private IEnumerator SplitRain(GameObject gota)
    {
        yield return new WaitForSeconds(1.7f);

        int Arc = 360 / NumeroBullets;
        for (int i = 0; i < 360; i += Arc)
        {
            Instantiate(PrefabBulletGota, gota.transform.position, Quaternion.AngleAxis(i, Vector3.up));
        }
    }
    private IEnumerator StartRain()
    {
        yield return new WaitForSeconds(Cooldown);
        isReady = true;
    }

    public bool RainReady()
    {
        return isReady;
    }
}
