using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPrison : MonoBehaviour
{
    public float Cooldown;

    public GameObject PrefabPrison;
    public Transform Player;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Prison() {
        GameObject prison = Instantiate(PrefabPrison,new Vector3(Player.position.x,0.75f,Player.position.z),Quaternion.identity);
        int DeadChild = Random.Range(4,25);
        Destroy(prison.transform.GetChild(DeadChild).gameObject);
    }

    public float GetCD() {
        return Cooldown;
    }
}
