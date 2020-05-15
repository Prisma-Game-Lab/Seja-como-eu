using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeartLaunch : MonoBehaviour
{
	private Vector3 target;
	private Transform _t;
    private Rigidbody _rb;
	private NavMeshAgent agent;

	public float risingDuration;
	public float height;

	public float launchingSpeed;

	public float windup;
	public float posLaunch;

	[Range(0, 100)]
	public float probabilidadeLaunch;

    public float cooldownLaunch;
    public Transform playerPosition;
    private Animator anim;

    private CarinhoScript cScript;

    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        cScript = GetComponent<CarinhoScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch()
    {
        agent.enabled = false; //desativa agente do carinho
    	StartCoroutine(HLaunch());
    	//Debug.Log("Launch!");
    }

	private IEnumerator HLaunch()
    {

    	Vector3 startPos = _t.position;
    	float time = 0f;

		_rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

		target = _t.position + height*_t.up;

        // Rising
        anim.SetTrigger("Up");
    	while(time < 1)//move para o alto
    	{
    		time += Time.deltaTime / risingDuration;
    		_t.position = Vector3.Lerp(startPos,target,time);
    		cScript.FaceTarget(playerPosition.position);
    		yield return new WaitForEndOfFrame ();
    	} 
    	// End Rising
        //Debug.Log("Chegou no alto");

    	target = playerPosition.position; //alvo definido
        
        yield return new WaitForSeconds(windup); //tempo pra desviar

        // Launching
        anim.SetTrigger("Fall");
        float distTol = 0.05f;
        while(Vector3.Distance(_t.position,target) > distTol)//vai pro jogador
        {
            
        	_t.position = Vector3.MoveTowards(_t.position, target, launchingSpeed * Time.deltaTime);
        	//cScript.FaceTarget(target);
        	yield return new WaitForEndOfFrame ();
        }
        // End Launching

        anim.SetTrigger("Ground");
        yield return new WaitForSeconds(posLaunch); //tempo pos launch
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
