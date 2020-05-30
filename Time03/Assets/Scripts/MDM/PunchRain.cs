using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchRain : MonoBehaviour
{
    public GameObject Player;
    public GameObject PrefabPunches;
    public GameObject PrefabHelper;
    public float Frequency;
    public float Altura;
    private bool RainingMan = false;
    private MDM Mestre;


    void Start()
    {
        Mestre = GetComponent<MDM>();
        PrefabHelper.transform.localScale.Set(PrefabPunches.transform.localScale.x,0.05f,PrefabPunches.transform.localScale.z);
    }

    public void Rain() {
        StartCoroutine(ERain());
        RainingMan = true;
    }

    private IEnumerator ERain() {
        while(Mestre.GetHP() != 1) {
            Instantiate(PrefabHelper,new Vector3(Player.transform.position.x,0,Player.transform.position.z),Quaternion.identity);
            Instantiate(PrefabPunches, new Vector3(Player.transform.position.x, Altura, Player.transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(Frequency);
        }
    }

    public bool ItsRainingMan() {
        return RainingMan;
    }
}
