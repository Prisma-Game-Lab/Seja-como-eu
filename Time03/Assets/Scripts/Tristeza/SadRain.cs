using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadRain : MonoBehaviour
{
    public int NumeroGotas;

    public float Probabilidade;

    public float Cooldown;

    public GameObject PrefabGota;
    public GameObject PrefabBulletGota;
    public int NumeroBullets;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Rain() {
        Debug.Log("choveu");
        float xPosition;
        float zPosition;
        for (int i = 0; i < NumeroGotas; i++) {
            xPosition = Random.Range(-19,19);
            zPosition = Random.Range(-19,19);
            GameObject rain = Instantiate(PrefabGota, new Vector3(xPosition, 15, zPosition), Quaternion.identity);
            StartCoroutine(RainSplit(rain));
        }
    }

    private IEnumerator RainSplit(GameObject gota)
    {
        yield return new WaitForSeconds(1.7f);

        int Arc = 360 / NumeroBullets;
        for (int i = 0; i < 360; i += Arc)
        {
            Instantiate(PrefabBulletGota, gota.transform.position, Quaternion.AngleAxis(i, Vector3.up));
        }
    }
    public float getProb()
    {
    	return Probabilidade;
    }

    public float getCD()
    {
    	return Cooldown;
    }
}
