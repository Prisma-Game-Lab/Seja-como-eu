using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public float bulletDuration;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        StartCoroutine(Despawn());
    }

    
    void Update()
    {
        
    }

    private IEnumerator Despawn() {
        yield return new WaitForSeconds(bulletDuration);
        Destroy(gameObject);
    }
}
