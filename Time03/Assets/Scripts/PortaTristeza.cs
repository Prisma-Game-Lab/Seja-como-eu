using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTristeza : MonoBehaviour
{
    public GameObject portaEsquerda;
    public GameObject portaDireita;
    private Quaternion RotE = Quaternion.Euler(0, 90, 0);
    private Quaternion RotD = Quaternion.Euler(0, -90, 0);
    public int OpenCounter = 0;
    void Update()
    {

        if (OpenCounter == 3)
        {
            portaDireita.transform.rotation = Quaternion.Slerp(portaDireita.transform.rotation, RotD, Time.deltaTime);
            portaEsquerda.transform.rotation = Quaternion.Slerp(portaEsquerda.transform.rotation, RotE, Time.deltaTime);
        }
    }


}
