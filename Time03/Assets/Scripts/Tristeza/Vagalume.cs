using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vagalume : MonoBehaviour
{
    public Vector3 center;
    public float orbitSpeed;
    public float radius;
    public GameObject tristeza;
    public Material apagado, acesoMat;
    public ParticleSystem luzinha;
    private MeshRenderer mesh;

    public bool aceso = true;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 orbit = (transform.position - center).normalized * radius + center;
        transform.position = orbit;

        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center, Vector3.up, orbitSpeed * Time.deltaTime);

        if(!aceso)
        {
            mesh.material = apagado;
            luzinha.enableEmission = false;
        }
        else
        {
            mesh.material = acesoMat;
            luzinha.enableEmission = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && aceso)
        {
            aceso = false;
            if(tristeza.tag == "Porta")
            {
                tristeza.GetComponent<PortaTristeza>().OpenCounter += 1;
            }
            else
            {
                tristeza.GetComponent<TristezaScript>().damageCounter += 1;
            }
        }
    }
}
