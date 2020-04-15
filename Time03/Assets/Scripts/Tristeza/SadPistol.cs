using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadPistol : MonoBehaviour
{
    public int NumeroBullets;

    public float Probabilidade;

    public float Cooldown;

    public GameObject PrefabBulletGota;
    public GameObject Reference;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Pistol() {
        int Arc = 360/NumeroBullets;
        for(int i = 0; i < 360; i += Arc) {
            Instantiate(PrefabBulletGota,Reference.transform.position,Quaternion.AngleAxis(i,Vector3.up));
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
