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

    private Animator anim;
    public GameObject corpoVagalume;
    private Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 orbit = (transform.position - center).normalized * radius + center;
        transform.position = orbit;

        mesh = GetComponent<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        mats = corpoVagalume.GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {

        if(!aceso)
        {
            //mesh.material = apagado;
            mats[0] = apagado;
            corpoVagalume.GetComponent<Renderer>().materials = mats;
            luzinha.enableEmission = false;
            anim.SetBool("Fly", false);
            transform.RotateAround(center, Vector3.up, 0f);
        }
        else
        {
            //mesh.material = acesoMat;
            mats[0] = acesoMat;
            corpoVagalume.GetComponent<Renderer>().materials = mats;
            luzinha.enableEmission = true;
            anim.SetBool("Fly", true);
            transform.RotateAround(center, Vector3.up, orbitSpeed * Time.deltaTime);
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
