using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TristezaScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject portaExit;
    public Vagalume[] vagalumes;
    private SadRain srScript;
    private SadPistol spScript;
    private SadRoll srlScript;
    private Skills Pistol;
    private Skills Roll;
    private List<Skills> skills;
    private BossSkillsCD SkillCD;
    public float Cooldown;
    [HideInInspector]
    public bool SkillIsReady = false;
    private NavMeshAgent Agent;
    private NavMeshPath Path;
    private Vector3 Direction;
    private float timer;
    private GeneralCounts Counts;
    public int damageCounter = 0;
    public int health = 3;

    public float RunAwayDistance;
    
    private Animator anim;
    public Renderer casco;
    public Texture quebrado1, quebrado2;
    public Animator animLuz;
    private DisplayFrase DF;

    void Start()
    {
        DF = GetComponent<DisplayFrase>();

        Counts = SaveSystem.GetInstance().generalCounts;

        Agent = GetComponent<NavMeshAgent>();

        Path = new NavMeshPath();

        skills = new List<Skills>();

        srScript = GetComponent<SadRain>();

        spScript = GetComponent<SadPistol>();
        Pistol = new Skills(spScript.getProb(),spScript.getCD(),false,spScript.Pistol);
        skills.Add(Pistol);

        srlScript = GetComponent<SadRoll>();
        Roll = new Skills(srlScript.getProb(), srlScript.getCD(), false, srlScript.Roll);
        skills.Add(Roll);

        SkillCD = GetComponent<BossSkillsCD>();
        anim = GetComponentInChildren<Animator>();

        StartCoroutine(ResetCooldown());
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Damage();
        }

        if(!Counts.TristezaIsMorto) {
            Counts.TristezaCompleteTimer += Time.deltaTime;
        }

        if(health > 0 && Agent.enabled)
        {
            FleeFromPlayer();

            if (SkillIsReady)
            {
                SkillCD.ChooseSkill(skills);
            }

            if (srScript.RainReady())
            {
                srScript.Rain();
            }

            if (Agent.isStopped)
            {
                FaceTarget(Player.transform.position);
            }

            if (damageCounter >= 3)
            {
                Damage();
                animLuz.SetBool("acende", false);
            }
        }
    }

    private void Damage()
    {
        health -= 1;
        damageCounter = 0;
        if(health<=0)
        {
            anim.SetTrigger("Death");
            Counts.TristezaIsMorto = true;
            portaExit.SetActive(true);
            srScript.StopAllCoroutines();
            Agent.enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            DF.Trigger.TriggerConversation(0,"TristezaMorre");
        }
        else
        {
            srScript.RainInterval -= 0.25f;
            if(health == 2)
            {
                Roll.SwitchReady();
                casco.material.mainTexture = quebrado1;
            }
            if(health == 1)
            {
                Pistol.SwitchReady();
                casco.material.mainTexture = quebrado2;
            }
            StartCoroutine(Stun());
        }
    }

    private IEnumerator Stun()
    {
        Agent.enabled = false;
        yield return new WaitForSeconds(2);
        Agent.enabled = true;
        for(int i = 0; i < vagalumes.Length; i++)
        {
            vagalumes[i].aceso = true;
            animLuz.SetBool("acende", true);
        }
    }
    public IEnumerator ResetCooldown() {
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }
    private void FleeFromPlayer() {

        float distance = Vector3.Distance(transform.position,Player.transform.position);

        if(distance < RunAwayDistance) {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            Agent.SetDestination(newPos);
        }
        else {
            
            if(!Agent.hasPath || timer >= 10) {
                timer = 0;
                Direction = ChooseDirection();
                NavMesh.CalculatePath(transform.position,Direction,NavMesh.AllAreas,Path);
                Agent.path = Path;
            }
        }
        
        timer += Time.deltaTime;
    }

    public void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private Vector3 ChooseDirection() {
        float xPosition = Random.Range(-19,19);
        float zPosition = Random.Range(-19,19);
        Vector3 newPos = new Vector3(xPosition,0,zPosition);
        return newPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(srlScript.rolling && collision.gameObject.CompareTag("Player"))
        {
            GeneralCounts.Kill = true;
        }
    }

}
