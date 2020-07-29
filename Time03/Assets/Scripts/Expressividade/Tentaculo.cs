using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
	public GameObject tentaculo;
	public GameObject wavyTentacle;
	public GameObject spawnZone;
	public int gapVar;
	public int defaultPeriod=3;
	private int gapCounter = 0;
	private float startingSpeed = 150;
	public float speed = 10;
	public static float currentSpeed = 150;
	private bool gap;
	public static event Action<float> speedChange;
	private static event Action nextSpawn;
	public static Tentaculo leader = null;
	public static int instances=0;
	public static int gapPeriod;
	private bool amLeader = false;
	private Animator tentAnim;


    void Awake()
    {
		gapCounter = (UnityEngine.Random.Range(0,gapVar));
		instances+=1;
		if(leader == null)
		{
			leader = this;
			amLeader=true;
			gapPeriod=defaultPeriod;
		}
		else
		{
			nextSpawn+=spawnTentacle;
			Destroy(spawnZone);
		}
    }
	void Start() 
	{
		if(amLeader)
		{
			StartCoroutine(Startup());
		}
	}
	private void OnDestroy() 
	{
		if(amLeader)
		{
			leader = null;
			gapPeriod = 3;
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
			tentAnim.SetTrigger("newWave");
			t.GetComponent<GuideWave>().anim=tentAnim;
    	}

    	else
    	{
    	 	t = TentPooling.instance.NewTent(this.transform ,false);
			gapCounter +=1;
			if (gapCounter%gapPeriod == 0)
			{
				gap = true;
				gapCounter = 0;
			}
    	}
		t.GetComponent<TentPieceScript>().Boost(currentSpeed * 10);
    	if(amLeader)
		{
			nextSpawn?.Invoke();
		}
    }

	public void SpeedUp(float newSpeed)
	{
		speedChange?.Invoke((newSpeed-currentSpeed) * 10);
		currentSpeed = newSpeed;
	}


	private IEnumerator Startup()
	{
		yield return new WaitUntil(()=>instances == 8);
		int period=gapPeriod;
		gapPeriod=99999999;

		currentSpeed=startingSpeed;
		spawnTentacle();
		yield return new WaitForSeconds(0.5f);

		SpeedUp(speed);
		gapPeriod=period;
	}

	public void setAnim(Animator anim)
	{
		tentAnim=anim;
	}
}
    

