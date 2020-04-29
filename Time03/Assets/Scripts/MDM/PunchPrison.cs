using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPrison : MonoBehaviour
{
    public float Cooldown;

    public float Delay;

    public GameObject PrefabPrison;
    public GameObject PrefabPrisonWarning;
    public Transform Player;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Prison() {
        StartCoroutine(EPrison());
    }

    public float GetCD() {
        return Cooldown;
    }

    private IEnumerator EPrison() {
        float x = Player.position.x;
        float z = Player.position.z;

        if(x > 15) x = 15;
        if(x < -15) x = -15;
        if(z > 15) z = 15;
        if(z < -15) z = -15;

        GameObject PrisonWarning = Instantiate(PrefabPrisonWarning,new Vector3(x,0,z),Quaternion.identity);

        yield return new WaitForSeconds(Delay);
        
        GameObject prison = Instantiate(PrefabPrison,new Vector3(x,0.75f,z),Quaternion.identity);
        int DeadChild = Random.Range(4,24);
        Destroy(prison.transform.GetChild(DeadChild).gameObject);
    }
}
