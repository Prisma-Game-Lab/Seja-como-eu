using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrenght = 20.0f;
    [SerializeField]
    private float knockbackHeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if(rb != null && collision.collider.CompareTag("enemy") && this.gameObject.GetComponent<CharControler>().dashing)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir.y = knockbackHeight;
            rb.AddForce(dir.normalized * knockbackStrenght, ForceMode.Impulse);           
        }
    }
}
