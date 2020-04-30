using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SadPistol : MonoBehaviour
{
    public int NumeroBullets;

    public float Probabilidade;

    public float Cooldown;

    public GameObject PrefabBulletGota;
    public GameObject Player;
    private NavMeshAgent agent;
    public float telegraph;
    private float bulletDuration;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        bulletDuration = PrefabBulletGota.GetComponent<BulletMove>().bulletDuration;
    }


    void Update()
    {

    }

    public void Pistol()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(0.5f);

        transform.LookAt(Player.transform.position);

        yield return new WaitForSeconds(telegraph);

        GameObject initialShot = Instantiate(PrefabBulletGota, gameObject.transform.position, gameObject.transform.rotation);
        initialShot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(PistolSpread(initialShot));

        agent.isStopped = false;
    }

    private IEnumerator PistolSpread(GameObject splitPoint)
    {
        yield return new WaitForSeconds(bulletDuration);

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
