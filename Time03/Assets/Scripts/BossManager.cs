using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject PortalCarinho;
    public GameObject PortalTristeza;
    public GameObject portalExpressividade;

    private GeneralCounts Counts;

    // Start is called before the first frame update
    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;

        if (Counts.CarinhoIsMorto)
        {
            PortalCarinho.GetComponent<PortalScript>().CanEnter = false;
        }
        if (Counts.TristezaIsMorto)
        {
            PortalTristeza.GetComponent<PortalScript>().CanEnter = false;
        }
        if(Counts.ExpressividadeIsMorto)
        {
            portalExpressividade.GetComponent<PortalScript>().CanEnter = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
