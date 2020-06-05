using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float autoTime;
    private bool following = true;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Autopilot());
    }

    // Update is called once per frame
    void Update()
    {
        if(following)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0,0.5f,0), speed * Time.deltaTime);
        }
    }

    private IEnumerator Autopilot()
    {
        yield return new WaitForSeconds(autoTime);

        following = false;
        rb.velocity = target.position.normalized * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.CompareTag("orb"))
        {
            Destroy(gameObject);
        }
    }
}
