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
    public GameObject PrefabBulletGota;
    public bool nightmareMode;
    public int numeroBullets;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        t_rb = gameObject.GetComponent<Rigidbody>();
        t_col = gameObject.GetComponent<Collider>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rolling && t_rb.velocity != Vector3.zero)
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
        anim.SetBool("isRoll", true);
        anim.SetTrigger("Roll");
        yield return new WaitForSeconds(rollWindup);

        t_rb.AddForce(transform.forward * rollSpeed, ForceMode.Impulse);
        t_col.material = bounceMat;
        rolling = true;

        yield return new WaitForSeconds(rollDuration);

        if(agent.enabled)
        {
            agent.isStopped = false;
        }
        t_rb.velocity = Vector3.zero;
        t_col.material = null;      
        rolling = false;
        anim.SetBool("isRoll", false);
    }

    private IEnumerator Explode()
    {
        int Arc = 360 / numeroBullets;
        for (int i = 0; i < 720; i += Arc)
        {
            Instantiate(PrefabBulletGota, transform.position, Quaternion.AngleAxis(i, Vector3.up));
        }

        yield return new WaitForSeconds(0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("wall") && rolling && nightmareMode)
        {
            StartCoroutine(Explode());
        }
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
