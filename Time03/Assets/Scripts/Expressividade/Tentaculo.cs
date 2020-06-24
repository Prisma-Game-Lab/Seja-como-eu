using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
	public GameObject tentaculo;
	public GameObject wavyTentacle;
	public int gapSize = 3;
	public float gapPeriod = 4;
	public float speed = 5;
	public float despawnDuration = 5;
	private int gaps = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnTentacle",1.0f,1.0f);
        StartCoroutine(AddGap());
    }

    // Update is called once per 
    void Update()
    {
        
    }

    private void spawnTentacle()
    {
    	 GameObject t;
    	 if(gaps>0)
    	 {
    	 	gaps=gaps - 1;
    	 	if(gaps < gapSize-1)
    	 	{
    	 		return;
    	 	}
    	 	else
    	 	{
    	 		t = Instantiate(wavyTentacle, this.transform);
    	 	}

    	 }

    	 else
    	 {
    	 	t = Instantiate(tentaculo ,this.transform);
    	 }

    	 t.GetComponent<Rigidbody>().AddForce(transform.forward * speed * 10);
    	 StartCoroutine(Despawn(t));

    }

    private IEnumerator AddGap()
    {
    	yield return new WaitForSeconds(1.0f);
    	while (true)
    	{
	    	yield return new WaitForSeconds(Random.Range(0.0f,2.0f));
	    	gaps += gapSize;
	    	yield return new WaitForSeconds(gapPeriod);
    	}
    }

    private IEnumerator Despawn(GameObject obj)
    {
    	yield return new WaitForSeconds(despawnDuration);
        Destroy(obj);
    }
}
    

