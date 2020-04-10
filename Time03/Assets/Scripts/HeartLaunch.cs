using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLaunch : MonoBehaviour
{
	private Vector3 target;
	private bool launching = false;
	private Transform _t;

	public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Transform>();
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
	        }
        }
    }

    public void Launch(Vector3 playerPosition)
    {
    	target = playerPosition;
    	StartCoroutine(HLaunch());
    	Debug.Log("Launch!");
    }

    private IEnumerator HLaunch()
    {

        /*ALVO DEFINIDO / TEMPO PARA DESVIAR*/
        yield return new WaitForSeconds(2);

        launching = true;
    }
}
