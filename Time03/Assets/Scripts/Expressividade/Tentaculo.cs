using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
	public GameObject tentaculo;
	public GameObject wavyTentacle;
	public int gapPeriod = 20;
	public int gapVar = 8;
	private int gapCounter = 0;
	public float startingSpeed = 10;
	private float speed = 100;
	private bool gap;
	public static event Action<float> speedChange;


    // Start is called before the first frame update
    void Start()
    {
		spawnTentacle();
		StartCoroutine(Startup());
    }

    public void spawnTentacle()
    {
    	GameObject t;
    	if(gap)
    	{
    	 	t = Instantiate(wavyTentacle, this.transform);
			gap=false;
    	}

    	else
    	{
    	 	t = Instantiate(tentaculo ,this.transform);
			 gapCounter +=1;
			 if (gapCounter == gapPeriod)
			 {
				 gap = true;
				 gapCounter = (int)UnityEngine.Random.value*gapVar;
			 }
    	}
		t.GetComponent<TentPieceScript>().Boost(speed * 10);
    	
    }

	public void SpeedUp(float newSpeed)
	{
		speedChange?.Invoke(newSpeed * 10);
		speed = newSpeed;
	}


	private IEnumerator Startup()
	{
		yield return new WaitForSeconds(2);
		SpeedUp(startingSpeed);
	}
}
    

