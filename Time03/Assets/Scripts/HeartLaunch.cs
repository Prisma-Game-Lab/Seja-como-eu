using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeartLaunch : MonoBehaviour
{
	private Vector3 target;
	private bool launching = false;
	private Transform _t;
	private NavMeshAgent agent;

	public float speed;

	public float windup;

	[Range(0, 100)]
	public float probabilidadeLaunch;

    public float cooldownLaunch;
    public Transform PlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(launching)
        {
	        float step = speed * Time.deltaTime;
	        _t.position = Vector3.MoveTowards(_t.position, target, step);
	        if(_t.position == target)
	        {
	        	launching = false;
	        	agent.enabled = true;
	        }
        }
    }

    public void Launch()
    {
    	target = PlayerPosition.position;
    	StartCoroutine(HLaunch());
    	Debug.Log("Launch!");
    }

	private IEnumerator HLaunch()
    {
    	agent.enabled = false;
        /*ALVO DEFINIDO / TEMPO PARA DESVIAR*/
        yield return new WaitForSeconds(windup);

        launching = true;
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
