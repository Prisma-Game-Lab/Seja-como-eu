using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject PortalCarinho;
    public GameObject PortalTristeza;

    private GeneralCounts Counts;

    // Start is called before the first frame update
    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    // Update is called once per frame
    void Update()
    {
        if(Counts.CarinhoIsMorto)
        {
            PortalCarinho.GetComponent<PortalScript>().CanEnter = false;
        }
        if(Counts.TristezaIsMorto)
        {
            PortalTristeza.GetComponent<PortalScript>().CanEnter = false;
        }
    }
}
