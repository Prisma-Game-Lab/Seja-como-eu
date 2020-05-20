using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaCarinho : MonoBehaviour
{
    public GameObject portaEsquerda;
    public GameObject portaDireita;
    private Quaternion RotE = Quaternion.Euler(0, 90, 0);
    private Quaternion RotD = Quaternion.Euler(0, -90, 0);
    private bool Open = false;
    public GameObject portalArena;

    private void Start()
    {
        portalArena.SetActive(false);
    }

    void Update() 
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Feno"))
        {
            StartCoroutine(AbrePortas());
            GetComponent<Collider>().enabled = false;
        }
    }
    private IEnumerator AbrePortas()
    {
    	while(portaDireita.transform.rotation != RotD)
    	{
	    	portaDireita.transform.rotation = Quaternion.Slerp(portaDireita.transform.rotation, RotD, Time.deltaTime);
	        portaEsquerda.transform.rotation = Quaternion.Slerp(portaEsquerda.transform.rotation, RotE, Time.deltaTime);
	        portalArena.SetActive(true);
	        yield return new WaitForEndOfFrame ();
    	}
    	
    }
}
