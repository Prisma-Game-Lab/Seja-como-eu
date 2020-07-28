using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : MonoBehaviour
{
    public Transform target;
    public Transform targetInit;
    public float speed;
    public float autoTime;
    public float initTime;
    private bool following = false;
    private bool init = true;
    public GameObject Expressividade;
    public GameObject tintaParticle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Autopilot());
    }

    // Update is called once per frame
    void Update()
    {
        int HP = Expressividade.GetComponent<ExpressividadeScript>().health;

        if(HP == 0)
        {
            Instantiate(tintaParticle, this.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }
        if(init)
        {
            FaceTarget(targetInit.position + new Vector3(0, 0.5f, 0));
            transform.position = Vector3.MoveTowards(transform.position, targetInit.position + new Vector3(0,0.5f,0), speed * Time.deltaTime);
        }
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
        yield return new WaitForSeconds(initTime);
        init = false;
        following = true;

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
        if(other.CompareTag("Player") || other.CompareTag("wall"))
        {
            Instantiate(tintaParticle, this.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }
    }
}
