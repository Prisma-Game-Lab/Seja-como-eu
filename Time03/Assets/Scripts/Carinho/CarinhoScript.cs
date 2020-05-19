using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarinhoScript : MonoBehaviour
{
    
    public float lookRadius = 10f; // se o jogador entrar nesse raio o carinho vai começar a perseguir ele
    public Transform[] carinhoHearts;
    public Transform PlayerPosition;
    public GameObject portalExit;
    private int health = 3;
    private Skills Launch;
    private Skills Orbit;
    private Skills Clap;
    private List<Skills> skills;  
    private HeartLaunch hlScript;
    private HeartOrbit hoScript;
    private HeartClap hcScript;
    private BossSkillsCD SkillCD; 
    public float Cooldown;

    public float damageStun;

    private bool SkillIsReady = false;

    private Animator anim;

    private Rigidbody _rb;
    private NavMeshAgent agent;
    private int fullhealth;

    private bool invulneravel = false;

    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;

        fullhealth = health;

        agent = GetComponent<NavMeshAgent>();

        skills = new List<Skills>();

        hoScript = GetComponent<HeartOrbit>();
        Orbit = new Skills(hoScript.getProb(), hoScript.getCD(), true, hoScript.Expansion);
        skills.Add(Orbit);

        hlScript = GetComponent<HeartLaunch>();
        Launch = new Skills(hlScript.getProb(), hlScript.getCD(), false, hlScript.Launch);
        skills.Add(Launch);

        hcScript = GetComponent<HeartClap>();
        Clap = new Skills(hcScript.getProb(), hcScript.getCD(), false, hcScript.Clap);
        skills.Add(Clap);

        SkillCD = GetComponent<BossSkillsCD>();

        anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();

        StartCoroutine(ResetCooldown());
    }

    void Update() 
    {
        if(!Counts.CarinhoIsMorto) {
            Counts.CarinhoCompleteTimer += Time.deltaTime;
        }
        if(health > 0)
        {
            float distance = Vector3.Distance(PlayerPosition.position, transform.position); //Atual distancia entre o carinho e o player

            if (agent != null && agent.enabled)
            {
                if (distance <= lookRadius)
                {
                    //Debug.Log(agent.enabled);
                    agent.SetDestination(PlayerPosition.position);
                    FaceTarget(PlayerPosition.position);
                }
                anim.SetBool("Idle", !(agent.hasPath));
                if (SkillIsReady)
                {
                    SkillCD.ChooseSkill(skills);
                    StartCoroutine(ResetCooldown());
                }
            }


            hoScript.OrbitAround();

            if(distance<=hcScript.hugRadius && health == 1)
            {
                Clap.setReady(true);
            }
            else
            {
                Clap.setReady(false);
            }


        }
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if(direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Damage(GameObject feno)
    {        
        FenoScript fs = feno.GetComponent<FenoScript>();
        if(agent.enabled && !invulneravel && fs.lancado)
        {
            health -= 1;
            carinhoHearts[health].gameObject.SetActive(false);
            Destroy(feno);
            if (health <= 0)
            {
                Counts.CarinhoIsMorto = true;
                anim.SetTrigger("Death");
                portalExit.SetActive(true);
            }
            else
            {
                anim.SetTrigger("Damage");              
                if(health == fullhealth - 1)
                {
                    Launch.SwitchReady();
                }
                if(health == fullhealth - 2)
                {
                    Clap.SwitchReady();
                }
                hoScript.orbitSpeed += 100.0f;
                hoScript.maxRadius += 5.0f;
                
                agent.enabled = false;
                invulneravel = true;
                StartCoroutine(Stun());
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Feno"))
        {
            Damage(collision.gameObject);
        }
    }

    private IEnumerator ResetCooldown()
    {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }


    private IEnumerator Stun()
    {
        //Debug.Log("Start Stun!");

        yield return new WaitForSeconds(damageStun);      
        //Debug.Log("End Stun!");
        agent.enabled = true;
        invulneravel = false;
    }
}