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
    public float damageInvuln;

    private bool SkillIsReady = false;

    private Animator anim;

    private Rigidbody _rb;
    private NavMeshAgent agent;
    private int fullhealth;

    private bool invulneravel = false;

    void Start()
    {
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
        if(health > 0)
        {
            Dano();
            float distance = Vector3.Distance(PlayerPosition.position, transform.position); //Atual distancia entre o carinho e o player

            if (agent != null && agent.enabled)
            {
                if (distance <= lookRadius)
                {
                    agent.SetDestination(PlayerPosition.position);
                }
            }

            FaceTarget();

            hoScript.OrbitAround();

            if (SkillIsReady && agent.isStopped == false)
            {
                SkillCD.ChooseSkill(skills);
                StartCoroutine(ResetCooldown());
            }

            if (agent.hasPath && agent.isStopped == false)
            {
                anim.SetBool("Idle", false);
            }
            else
            {
                anim.SetBool("Idle", true);
            }
        }
    }

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

    void Damage(GameObject feno)
    {        
        if(agent.enabled && !invulneravel)
        {
            health -= 1;
            carinhoHearts[health].gameObject.SetActive(false);
            Destroy(feno);
            if (health <= 0)
            {
                portalExit.gameObject.SetActive(true);
                anim.SetTrigger("Death");
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
                StartCoroutine(Invunerability());
                StartCoroutine(Stun());
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("rock"))
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

    private IEnumerator Invunerability()
    {
        invulneravel = true;
        yield return new WaitForSeconds(damageInvuln);
        invulneravel = false;
    }

    private IEnumerator Stun()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(damageStun);
        agent.isStopped = false;
    }

    void Dano(){
        if(Input.GetKeyDown(KeyCode.K)){
            Damage(null);
        }

    }

}