using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SadRoll : MonoBehaviour
{
    public float rollWindup;
    public float rollDuration;

    public float rollSpeed;

    [Range(0,100)]
    public float Probabilidade;

    public float Cooldown;

    private NavMeshAgent agent;

    private Rigidbody t_rb;
    [HideInInspector]
    public bool rolling;
    [Tooltip("O material é o que faz a tristeza rebater nas paredes.")]
    public PhysicMaterial bounceMat;
    private Collider t_col;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        t_rb = gameObject.GetComponent<Rigidbody>();
        t_col = gameObject.GetComponent<Collider>();
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
        StartCoroutine(RollDestination());
    }

    private IEnumerator RollDestination()
    {
        agent.isStopped = true;       

        yield return new WaitForSeconds(rollWindup);

        t_rb.AddForce(transform.forward * rollSpeed, ForceMode.Impulse);
        t_col.material = bounceMat;
        rolling = true;

        yield return new WaitForSeconds(rollDuration);

        t_rb.velocity = Vector3.zero;
        t_col.material = null;
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
