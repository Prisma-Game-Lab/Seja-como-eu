using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarinhoScript : MonoBehaviour
{
    public float ProbabilidadeLaunch;
    public float CoolDownLaunch;
    public float lookRadius = 10f; // se o jogador entrar nesse raio o carinho vai começar a perseguir ele

    private Skills Launch;

    private List<Skills> skills;
    

    public Transform PlayerPosition;
    private Rigidbody _rb;

    NavMeshAgent agent;
        void Start()
    {
        Launch = new Skills(ProbabilidadeLaunch,CoolDownLaunch,false);

        agent = GetComponent<NavMeshAgent>();

        skills.Add(Launch);

        
    }


    void Update() {
        float distance = Vector3.Distance(PlayerPosition.position, transform.position); //Atual distancia entre o carinho e o player

        if(distance <= lookRadius){
            agent.SetDestination(PlayerPosition.position);
        }

        if(distance <= agent.stoppingDistance){
            FaceTarget();
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

    void FaceTarget(){
        Vector3 direction = (PlayerPosition.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
