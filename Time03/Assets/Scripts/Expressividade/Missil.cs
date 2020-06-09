using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float autoTime;
    private bool following = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Autopilot());
    }

    // Update is called once per frame
    void Update()
    {
        if(following)
        {
            FaceTarget(target.position + new Vector3(0, 0.5f, 0));
            transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0,0.5f,0), speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private IEnumerator Autopilot()
    {
        yield return new WaitForSeconds(autoTime);

        following = false;
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("orb"))
        {
            Destroy(gameObject);
        }
    }
}
