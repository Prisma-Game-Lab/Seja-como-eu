using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOrbit : MonoBehaviour
{

    public float orbitSpeed = 100.0f;
    public float ProbabilidadeOrbit;
    public float CooldownOrbit;
    public Skills Orbit;

    private float radius;
    public float maxRadius = 10.0f;
    public float minRadius = 2.0f;
    public float expansionSpeed;
    public Transform center;
    public Vector3 newOrbit;
    private bool canExpand = true;

    // Start is called before the first frame update
    void Start()
    {
        Orbit = new Skills(ProbabilidadeOrbit, CooldownOrbit, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OrbitAround(Transform[] hearts)
    {
        for(int i = 0; i <= hearts.Length - 1; i++)
        {
            hearts[i].RotateAround(gameObject.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            newOrbit = (hearts[i].position - center.position).normalized * radius + center.position;
            hearts[i].position = Vector3.MoveTowards(hearts[i].position, newOrbit, Time.deltaTime * expansionSpeed);
            if(canExpand)
            {
                StartCoroutine(Expand());
            }
            else
            {
                StartCoroutine(Retract());
            }           
        }
    }

    public IEnumerator Expand()
    {
        radius = maxRadius;     

        yield return new WaitForSeconds(CooldownOrbit);

        canExpand = false;
    }

    public IEnumerator Retract()
    {
        radius = minRadius;

        yield return new WaitForSeconds(CooldownOrbit);

        canExpand = true;
    }
}
