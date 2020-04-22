using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadPistol : MonoBehaviour
{
    public int NumeroBullets;

    public float Probabilidade;

    public float Cooldown;

    public GameObject PrefabBulletGota;
    public GameObject Reference;
    public GameObject tristeza;
    void Start()
    {

    }


    void Update()
    {

    }

    public void Pistol()
    {
        GameObject initialShot = Instantiate(PrefabBulletGota,tristeza.transform.position,tristeza.transform.rotation);
        initialShot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(PistolSpread(initialShot));
    }

    private IEnumerator PistolSpread(GameObject splitPoint)
    {
        yield return new WaitForSeconds(splitPoint.GetComponent<BulletMove>().bulletDuration);

        int Arc = 360/NumeroBullets;
        for(int i = 0; i < 360; i += Arc)
        {
            Instantiate(PrefabBulletGota,splitPoint.transform.position,Quaternion.AngleAxis(i,Vector3.up));
        }
    }

    public float getProb()
    {
    	return Probabilidade;
    }

    public float getCD()
    {
    	return Cooldown;
    }
}
