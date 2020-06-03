using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenoSpawner : MonoBehaviour
{
    public GameObject feno;
    public Transform fenoPai;
    public float bounds;
    public float height;

    void Start()
    {
        for(int i=0;i<3;i++)
        {
        	float posX = Random.Range(-bounds,bounds);
        	float posZ = Random.Range(-bounds,bounds);
        	float rot  = Random.Range(0.0f,180.0f);

        	Instantiate(feno,new Vector3(posX,height,posZ),Quaternion.Euler(0.0f,rot,90.0f),fenoPai);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
