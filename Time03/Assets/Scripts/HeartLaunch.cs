using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeartLaunch : MonoBehaviour
{
	private Vector3 target;

	private bool launching = false;
	private bool rising = false;

	private Transform _t;
	private NavMeshAgent agent;

	public float risingDuration;
	public float height;

	public float launchingSpeed;

	public float windup;

	[Range(0, 100)]
	public float probabilidadeLaunch;

    public float cooldownLaunch;
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch()
    {
    	StartCoroutine(HLaunch());
    	Debug.Log("Launch!");
    }

	private IEnumerator HLaunch()
    {

    	Vector3 startPos = _t.position;
    	float time = 0f;

		agent.enabled = false; //desativa agente do carinho

		target = _t.position + height*_t.up;

    	rising = true;
    	while(time < 1)//move para o alto
    	{
    		time += Time.deltaTime / risingDuration;
    		_t.position = Vector3.Lerp(startPos,target,time);

    		yield return new WaitForEndOfFrame ();
    	} 
    	rising=false;


    	target = playerPosition.position; //alvo definido
        
        yield return new WaitForSeconds(windup); //tempo pra desviar

        launching = true;
        while(_t.position != target)//vai pro jogador
        {
        	_t.position = Vector3.MoveTowards(_t.position, target, launchingSpeed * Time.deltaTime);
        	yield return new WaitForEndOfFrame ();
        }
        launching = false;

        agent.enabled = true; //reativa agente do carinho
    }

    public float getProb()
    {
    	return probabilidadeLaunch;
    }

    public float getCD()
    {
    	return cooldownLaunch;
    }

    
}
