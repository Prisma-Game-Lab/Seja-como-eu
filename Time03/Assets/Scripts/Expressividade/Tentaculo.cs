using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
	public GameObject tentaculo;
	public GameObject wavyTentacle;
	public GameObject spawnZone;
	public int gapPeriod;
	public int gapVar;
	private int gapCounter = 0;
	public float startingSpeed = 10;
	public static float speed = 50;
	private bool gap;
	public static event Action<float> speedChange;
	private static event Action nextSpawn;
	public static Tentaculo leader = null;
	public static int instances=0;
	private bool amLeader = false;

    // Start is called before the first frame update
    void Start()
    {
		gapCounter = (int)(UnityEngine.Random.Range(0.0f,gapVar));
		instances+=1;
		if(leader == null)
		{
			leader = this;
			amLeader=true;
			StartCoroutine(Startup());
		}
		else
		{
			nextSpawn+=spawnTentacle;
			Destroy(spawnZone);
		}
    }
	private void OnDestroy() 
	{
		if(amLeader)
		{
			speed = 50;
			leader = null;
		}
		else
		{
			nextSpawn-=spawnTentacle;
		}
		instances-=1;
	}

    public void spawnTentacle()
    {
    	GameObject t;
    	if(gap)
    	{
    	 	t = TentPooling.instance.NewTent(this.transform, true);
			gap=false;
    	}

    	else
    	{
    	 	t = TentPooling.instance.NewTent(this.transform ,false);
			 gapCounter +=1;
			 if (gapCounter >= gapPeriod)
			 {
				 gap = true;
				 gapCounter = (int)(UnityEngine.Random.Range(0.0f,gapVar));
			 }
    	}
		t.GetComponent<TentPieceScript>().Boost(speed * 10);
    	if(amLeader)
		{
			nextSpawn?.Invoke();
		}
    }

	public void SpeedUp(float newSpeed)
	{
		speedChange?.Invoke((newSpeed-speed) * 10);
		speed = newSpeed;
	}


	private IEnumerator Startup()
	{
		yield return new WaitUntil(()=>instances == 8);
		spawnTentacle();
		yield return new WaitForSeconds(2);
		SpeedUp(startingSpeed);
	}
}
    

