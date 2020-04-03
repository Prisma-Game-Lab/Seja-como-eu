using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarinhoScript : MonoBehaviour
{
    public Transform PlayerPosition;
    private Rigidbody _rb;
        void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerPosition != null){
            //transform.LookAt(PlayerPosition);
            transform.rotation = Quaternion.LookRotation(PlayerPosition.position - transform.position);
        }
    }
}
