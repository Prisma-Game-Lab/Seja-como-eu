using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartClap : MonoBehaviour
{
    [Range(0, 100)]
    public float Probabilidade;

    public float CoolDown;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Clap() {
        Debug.Log("Clap!");
    }

    public float getProb()
    {
    	return Probabilidade;
    }

    public float getCD()
    {
    	return CoolDown;
    }
}
