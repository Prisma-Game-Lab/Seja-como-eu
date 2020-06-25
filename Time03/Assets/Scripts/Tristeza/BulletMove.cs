using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public float bulletDuration;

    public GameObject gotaParticle;

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
        Instantiate(gotaParticle, this.transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }
}
