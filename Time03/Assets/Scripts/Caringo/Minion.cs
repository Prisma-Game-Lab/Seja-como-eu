using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject Bullet;

    public float RepeatRate;

    void Start()
    {
        InvokeRepeating("ShootBullet",0,RepeatRate);
    }

    
    void Update()
    {
        
    }

    private void ShootBullet() {
        if(!transform.parent.gameObject.activeSelf) return;
        Vector3 Pos = transform.position;
        Vector3 Direction = transform.forward;
        Quaternion Rotation = transform.rotation;

        Vector3 SpawnPos = Pos + Direction*1.2f;

        Instantiate(Bullet,new Vector3(SpawnPos.x,0.5f,SpawnPos.z),new Quaternion(0,Rotation.y,0,Rotation.w));
    }
}
