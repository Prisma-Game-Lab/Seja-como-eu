using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TristezaScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject deathScreen;
    private SadRain srScript;
    private SadPistol spScript;
    private SadRoll srlScript;
    private Skills Pistol;
    private Skills Roll;
    private List<Skills> skills;
    private BossSkillsCD SkillCD;
    public float Cooldown;
    private bool SkillIsReady = false;
    private NavMeshAgent Agent;
    private NavMeshPath Path;
    private Vector3 Direction;
    private float timer;

    public float RunAwayDistance;

    void Start()
    {

        Agent = GetComponent<NavMeshAgent>();

        Path = new NavMeshPath();

        skills = new List<Skills>();

        srScript = GetComponent<SadRain>();

        spScript = GetComponent<SadPistol>();
        Pistol = new Skills(spScript.getProb(),spScript.getCD(),true,spScript.Pistol);
        skills.Add(Pistol);

        srlScript = GetComponent<SadRoll>();
        Roll = new Skills(srlScript.getProb(), srlScript.getCD(), true, srlScript.Roll);
        skills.Add(Roll);

        SkillCD = GetComponent<BossSkillsCD>();

        StartCoroutine(ResetCooldown());
    }

    
    void Update()
    {
        FleeFromPlayer();

        if(SkillIsReady) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }

        if(srScript.RainReady()) {
            srScript.Rain();
        }
    }

    private IEnumerator ResetCooldown() {
        SkillIsReady = false;
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
            deathScreen.SetActive(true);
        }
    }

}
