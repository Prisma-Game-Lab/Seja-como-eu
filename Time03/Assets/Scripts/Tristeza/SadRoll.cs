using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SadRoll : MonoBehaviour
{
    public float rollWindup;
    public float rollDuration;

    public float rollSpeed;

    public float Probabilidade;

    public float Cooldown;

    private NavMeshAgent agent;

    private Rigidbody t_rb;

    private bool rolling;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        t_rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rolling)
        {
            transform.forward = t_rb.velocity;
        }
    }

    public void Roll()
    {
        float posx = Random.Range(-19, 19);
        float posz = Random.Range(-19, 19);
        Vector3 destination = new Vector3(posx, transform.position.y, posz);
        Debug.Log(destination);

        StartCoroutine(RollDestination(destination));
    }

    private IEnumerator RollDestination(Vector3 target)
    {
        agent.isStopped = true;       

        yield return new WaitForSeconds(0.5f);

        transform.LookAt(target);

        yield return new WaitForSeconds(rollWindup);

        t_rb.AddForce(target.normalized * rollSpeed, ForceMode.Impulse);

        rolling = true;

        yield return new WaitForSeconds(rollDuration);

        t_rb.velocity = Vector3.zero;
        agent.isStopped = false;
        rolling = false;
    }

    public float getProb()
    {
        return Probabilidade;
    }

    public float getCD()
    {
        return Cooldown;
    }
}
