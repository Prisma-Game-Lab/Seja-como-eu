using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float Speed;
    private bool canGo = false;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitForLaunch());
    }

    
    void Update()
    {
        if(canGo)
            rb.velocity = transform.forward*Speed;
    }

    private IEnumerator WaitForLaunch() {
        yield return new WaitForSeconds(0.5f);
        canGo = true;
    }
}
