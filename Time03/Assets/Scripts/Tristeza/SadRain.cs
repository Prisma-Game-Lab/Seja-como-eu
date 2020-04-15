using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadRain : MonoBehaviour
{
    public int NumeroGotas;

    public float Probabilidade;

    public float Cooldown;

    public GameObject PrefabGota;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Rain() {
        Debug.Log("Executei");
        float xPosition;
        float zPosition;
        for (int i = 0; i < NumeroGotas; i++) {
            xPosition = Random.Range(-19,19);
            zPosition = Random.Range(-19,19);
            Instantiate(PrefabGota, new Vector3(xPosition, 15, zPosition), Quaternion.identity);                                                                
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
