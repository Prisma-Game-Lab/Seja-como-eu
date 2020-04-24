using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartClap : MonoBehaviour
{
    [Range(0, 100)]
    public float Probabilidade;

    public float CoolDown;

    public float windup;
    public float hugRadius;

    public GameObject player;

    private EnemyCollision eCol;
    private Transform _t;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        _t = GetComponent<Transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        eCol = player.GetComponent<EnemyCollision>();
    }


    void Update()
    {
        
    }

    private IEnumerator HHug(){

    	Collider[] hitColliders;
    	bool inBox = false;
    	
    	agent.enabled = false; //desativa agente do carinho

    	yield return new WaitForSeconds(windup); //tempo esticando os bracos

    	int i = 0;

    	hitColliders = Physics.OverlapBox(_t.forward * hugRadius/2f, new Vector3(hugRadius/2f,hugRadius/2f,hugRadius/2f), _t.rotation);
    	while( i < hitColliders.Length)
    	{
    		if(ballHitColliders[i].tag == "Player")
    		{
    			inBox = true;
    			break;
    		}
    		i+=1;
    	}

    	if(inBox)
    	{
	    	hitColliders = Physics.OverlapSphere(_t.position, hugRadius);
	        while (i < hitColliders.Length)
	        {
	        	if(ballHitColliders[i].tag == "Player")
	        	{
	        		
	            	eCol.Hit();
	            	break;
	            }
	            i+=1;
	        }
	    }

        agent.enabled = true; //reativa agente do carinho

    }

	void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hugRadius);
    }

    public void Clap() {
    	StartCoroutine(HHug());
        Debug.Log("Clap!");
    }

    public float getProb()
    {
    	return Probabilidade;
    }

    public float getCD()
    {
    	return CoolDown;
    }
}
