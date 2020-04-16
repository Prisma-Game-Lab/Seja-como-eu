using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    
    void Update()
    {
        
    }
}
