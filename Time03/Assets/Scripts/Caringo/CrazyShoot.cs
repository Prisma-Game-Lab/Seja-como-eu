using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyShoot : MonoBehaviour
{
    public GameObject CrazyBullet;
    public Transform PlayerPosition;
    public float Cooldown;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Shoot() {
        Vector3 Pos = transform.position;
        Vector3 Direction = transform.forward;
        Quaternion Rotation = transform.rotation;

        Vector3 SpawnPos = Pos + Direction*1.2f;

        GameObject Bullet =  Instantiate(CrazyBullet,new Vector3(SpawnPos.x,0.5f,SpawnPos.z),new Quaternion(0,Rotation.y,0,Rotation.w));
        
    }
}
