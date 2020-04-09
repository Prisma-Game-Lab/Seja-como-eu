using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarinhoScript : MonoBehaviour
{
    public float ProbabilidadeLaunch;
    public float CoolDownLaunch;
    public float lookRadius = 10f; // se o jogador entrar nesse raio o carinho vai começar a perseguir ele
    public Transform[] carinhoHearts;
    public Transform PlayerPosition;


    private float orbitSpeed = 100.0f;
    private int health = 3;
    private Skills Launch;
    private List<Skills> skills;
    private Rigidbody _rb;
    private NavMeshAgent agent;
    public GameObject arena;

    void Start()
    {
        Launch = new Skills(ProbabilidadeLaunch,CoolDownLaunch,false);

        agent = GetComponent<NavMeshAgent>();

        skills = new List<Skills>();
        skills.Add(Launch);       
    }


    void Update()
    {

        //float distance = Vector3.Distance(PlayerPosition.position, transform.position); //Atual distancia entre o carinho e o player

        HeartOrbit();

        /*if(distance <= lookRadius){
            agent.SetDestination(PlayerPosition.position);
        }

        if(distance <= agent.stoppingDistance){
            FaceTarget();
        }*/
    }
    // Update is called once per frame
    /*void FixedUpdate()
    {
        if(PlayerPosition != null){
            //transform.LookAt(PlayerPosition);
            transform.rotation = Quaternion.LookRotation(PlayerPosition.position - transform.position);
        }
    }*/

    void FaceTarget(){
        Vector3 direction = (PlayerPosition.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void HeartOrbit()
    {
        for(int i = 0; i <= carinhoHearts.Length - 1; i++)
        {
            carinhoHearts[i].RotateAround(arena.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }

    void Damage()
    {
        health -= 1;
        carinhoHearts[health].gameObject.SetActive(false);
        orbitSpeed += 100;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if(rb != null && collision.collider.CompareTag("rock"))
        {
            Damage();
        }
    }

}
