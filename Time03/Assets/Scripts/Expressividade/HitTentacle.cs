using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class HitTentacle : MonoBehaviour
{
    public static event Action tentacleHit;
    private MovimentPlayer _mp;

    void Start()
    {
        _mp = GetComponent<MovimentPlayer>();

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("VulnTent"))
        {
            if(_mp.dashing)
            {
                tentacleHit?.Invoke();
            }
        }
    }
}
