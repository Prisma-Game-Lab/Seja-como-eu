using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOrbit : MonoBehaviour
{

    public float orbitSpeed = 100.0f;
    [Range(0, 100)]
    public float ProbabilidadeOrbit;
    public float CooldownOrbit;

    private float radius = 2.0f;
    private float expansionTime = 5.0f;
    public float maxRadius = 5.0f;
    public float minRadius = 2.0f;
    public float expansionSpeed;
    public Transform center;
    public GameObject hearts;
    public Vector3 newOrbit;
    public Transform[] carinhoHearts;
    private ParticleSystem.ShapeModule carinhoArea;
    private bool canExpand = true;

    public void Start(){
        //encontra e define carinhoArea, emissor de partículas que demonstra raio dos corações
        ParticleSystem ps = hearts.GetComponentInChildren<ParticleSystem>();
        carinhoArea = ps.shape;
    }
    public void OrbitAround()
    {
        hearts.transform.position = center.position;
        for (int i = 0; i <= carinhoHearts.Length - 1; i++)
        {
            carinhoHearts[i].RotateAround(gameObject.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            newOrbit = (carinhoHearts[i].position - center.position).normalized * radius + center.position;
            carinhoHearts[i].position = Vector3.MoveTowards(carinhoHearts[i].position, newOrbit, Time.deltaTime * expansionSpeed); 
        }
        //Ajeita raio do emissor de particulas de acordo com raio dos corações
        carinhoArea.radius = Mathf.MoveTowards(carinhoArea.radius, radius, Time.deltaTime * expansionSpeed);
    }

    public void Expansion()
    {
        if (canExpand)
        {
            StartCoroutine(Expand());
        }
        else
        {
            StartCoroutine(Retract());
        }
    }

    private IEnumerator Expand()
    {
        radius = maxRadius;     

        yield return new WaitForSeconds(expansionTime);

        canExpand = false;
    }

    private IEnumerator Retract()
    {
        radius = minRadius;

        yield return new WaitForSeconds(expansionTime);

        canExpand = true;
    }

    public float getProb()
    {
    	return ProbabilidadeOrbit;
    }

    public float getCD()
    {
    	return CooldownOrbit;
    }
}
