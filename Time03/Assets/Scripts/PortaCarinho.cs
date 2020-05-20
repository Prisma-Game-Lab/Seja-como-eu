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
    void Update() {

        if(Open == true){
            portaDireita.transform.rotation = Quaternion.Slerp(portaDireita.transform.rotation, RotD, Time.deltaTime);
            portaEsquerda.transform.rotation = Quaternion.Slerp(portaEsquerda.transform.rotation, RotE, Time.deltaTime);
            portalArena.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Feno")){
            Open = true;
            Destroy(other.gameObject);
        }
            
    }
}
