using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMissile : MonoBehaviour
{

    public GameObject MissilePrefab;
    [Range(0,100)]
    public float probablidade;
    public float cooldown;
    public Transform target;
    public int NumMissiles;
    public float gap;
    public float speed;
    private Animator headAnim;

    // Start is called before the first frame update
    void Start()
    {
        MissilePrefab.GetComponent<Missil>().target = target;
        MissilePrefab.GetComponent<Missil>().speed = speed;
        headAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        StartCoroutine(MissileLaunch());
        headAnim.SetTrigger("tiro");
    }

    private IEnumerator MissileLaunch()
    {
        for(int i = 0; i<NumMissiles;i++)
        {
            Instantiate(MissilePrefab, transform.position + new Vector3(0,5,0), Quaternion.identity);
            yield return new WaitForSeconds(gap);
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
