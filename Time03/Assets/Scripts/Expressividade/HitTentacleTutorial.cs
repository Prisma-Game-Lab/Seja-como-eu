using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTentacleTutorial : MonoBehaviour
{
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
                Destroy(collision.gameObject);
            }
        }
    }
}
