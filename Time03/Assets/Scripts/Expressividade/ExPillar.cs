using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPillar : MonoBehaviour
{
    [Range(0,100)]
    public float probablidade;
    public float cooldown;
    public GameObject PillarPrefab;
    public GameObject prefabHelper;
    public int numPilares;
    public float PillarInterval;
    public float windup;
    public Vector3[] spawnPoints;
    public Transform playerPos;
    public bool FollowPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        prefabHelper.transform.localScale.Set(PillarPrefab.transform.localScale.x, 0.05f, PillarPrefab.transform.localScale.z);
        prefabHelper.GetComponent<Despawner4>().TimetoDespawn = 3.5f;
        PillarPrefab.GetComponent<Despawner4>().TimetoDespawn = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InkPillar()
    {
        StartCoroutine(ShootInk());
    }

    private IEnumerator ShootInk()
    {
        Vector3 spawnPos;
        int index;
        for (int i = 0;i<numPilares;i++)
        {
            if(FollowPlayer)
            {
                spawnPos = new Vector3(playerPos.position.x,0,playerPos.position.z) + new Vector3(Random.insideUnitSphere.x,0,Random.insideUnitSphere.z) * 2;
            }
            else
            {
                index = Random.Range(0, spawnPoints.Length);
                spawnPos = spawnPoints[index];
            }

            Instantiate(prefabHelper, spawnPos + new Vector3(0,0.46f,0), Quaternion.identity);
            yield return new WaitForSeconds(windup);
            Instantiate(PillarPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(PillarInterval);
        }
    }

    public float getProb()
    {
        return probablidade;
    }

    public float getCD()
    {
        return cooldown;
    }
}
