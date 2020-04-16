using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TristezaScript : MonoBehaviour
{
    public GameObject Player;
    private SadRain srScript;
    private SadPistol spScript;
    private Skills Rain;
    private Skills Pistol;
    private List<Skills> skills;
    private BossSkillsCD SkillCD;
    public float Cooldown;
    private bool SkillIsReady = false;
    private NavMeshAgent Agent;
    private bool CanGetNewDirection = true;

    private Vector3 Direction;

    public float RunAwayDistance;

    void Start()
    {

        Agent = GetComponent<NavMeshAgent>();

        skills = new List<Skills>();

        srScript = GetComponent<SadRain>();
        Rain = new Skills(srScript.getProb(),srScript.getCD(),false,srScript.Rain);
        skills.Add(Rain);

        spScript = GetComponent<SadPistol>();
        Pistol = new Skills(spScript.getProb(),spScript.getCD(),false,spScript.Pistol);
        skills.Add(Pistol);

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
            
            if(CanGetNewDirection) {
                Direction = ChooseDirection();
                StartCoroutine(ChangeDirection());
            }

            Agent.SetDestination(Direction);
        }
    }

    private IEnumerator ChangeDirection() {
        CanGetNewDirection = false;
        yield return new WaitForSeconds(8.0f);
        CanGetNewDirection = true;
    }

    private Vector3 ChooseDirection() {
        float xPosition = Random.Range(-19,19);
        float zPosition = Random.Range(-19,19);
        Vector3 newPos = new Vector3(xPosition,0,zPosition);
        Debug.Log(newPos);
        return newPos;
    }

}
