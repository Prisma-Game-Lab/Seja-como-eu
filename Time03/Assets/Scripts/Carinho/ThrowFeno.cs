﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFeno : MonoBehaviour
{
    public float knockbackStrenght = 20.0f;
    public float knockbackHeight = 1.0f;

    private GeneralCounts Counts;

    private Rigidbody rb;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("rock"))
        {
            if(gameObject.GetComponent<MovimentPlayer>().dashing)
            {
                Vector3 dir = collision.transform.position - transform.position;
                rb.AddForce(dir.normalized * knockbackStrenght, ForceMode.Impulse);
            }
        }
    }

    public void Hit()
    {
        if(gameObject.GetComponent<MovimentPlayer>().dashing){
            return;
        }
        else{
            Debug.Log("Hit!");
        }
    }
}
