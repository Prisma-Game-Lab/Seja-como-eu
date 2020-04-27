using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PunchFollow : MonoBehaviour
{
    public GameObject PrefabPunchesFollow;
    public Transform PlayerPosition;
    public float Cooldown;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Follow() {
        GameObject one =  Instantiate(PrefabPunchesFollow,new Vector3(19,0.5f,19),Quaternion.identity);
        GameObject two =  Instantiate(PrefabPunchesFollow,new Vector3(-19,0.5f,19),Quaternion.identity);
        GameObject three =  Instantiate(PrefabPunchesFollow,new Vector3(19,0.5f,-19),Quaternion.identity);
        GameObject four =  Instantiate(PrefabPunchesFollow,new Vector3(-19,0.5f,-19),Quaternion.identity);

        one.transform.LookAt(PlayerPosition);
        two.transform.LookAt(PlayerPosition);
        three.transform.LookAt(PlayerPosition);
        four.transform.LookAt(PlayerPosition);
    }

    public float GetCD() {
        return Cooldown;
    }
}
