using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarinhoScript : MonoBehaviour
{
    
    public float lookRadius = 10f; // se o jogador entrar nesse raio o carinho vai começar a perseguir ele
    public Transform[] carinhoHearts;
    public Transform PlayerPosition;

    private bool launching;
    private int health = 3;
    private Skills Launch;
    private List<Skills> skills;  
    private HeartLaunch hlScript;
    private HeartOrbit hoScript;

    private Rigidbody _rb;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        skills = new List<Skills>();

        hlScript = GetComponent<HeartLaunch>();
        Launch = new Skills(hlScript.getProb(), hlScript.getCD(), false);
        
        skills.Add(Launch);

        hoScript = GetComponent<HeartOrbit>();
        skills.Add(hoScript.Orbit);
    }

    void Update() 
    {
        float distance = Vector3.Distance(PlayerPosition.position, transform.position); //Atual distancia entre o carinho e o player

        hoScript.OrbitAround(carinhoHearts);

        if(agent.enabled){
            if(distance <= lookRadius){
                agent.SetDestination(PlayerPosition.position);
            }

            if(distance <= agent.stoppingDistance){
                FaceTarget();

            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            hlScript.Launch(PlayerPosition.position);
        }


    }
    // Update is called once per frame
    /*void FixedUpdate()
    {
        if(PlayerPosition != null){
            //transform.LookAt(PlayerPosition);
            transform.rotation = Quaternion.LookRotation(PlayerPosition.position - transform.position);

        }
    }*/

    void FaceTarget()
    {
        Vector3 direction = (PlayerPosition.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Damage()
    {
        health -= 1;
        carinhoHearts[health].gameObject.SetActive(false);
        hoScript.orbitSpeed += 100.0f; 

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("rock"))
        {
            Damage();
        }
    }

}
