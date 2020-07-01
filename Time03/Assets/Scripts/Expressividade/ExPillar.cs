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

    // Start is called before the first frame update
    void Start()
    {
        prefabHelper.transform.localScale.Set(PillarPrefab.transform.localScale.x, 0.05f, PillarPrefab.transform.localScale.z);
        prefabHelper.GetComponent<Despawner4>().TimetoDespawn = 3.0f;
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
        float xposition;
        float zposition;
        for (int i = 0;i<numPilares;i++)
        {
            xposition = Random.Range(-19, 19);
            zposition = Random.Range(-19, 19);
            Instantiate(prefabHelper, new Vector3(xposition, 0, zposition), Quaternion.identity);
            yield return new WaitForSeconds(windup);
            Instantiate(PillarPrefab, new Vector3(xposition, 0, zposition), Quaternion.identity);
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
