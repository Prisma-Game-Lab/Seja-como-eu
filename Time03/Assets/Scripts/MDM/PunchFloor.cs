using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFloor : MonoBehaviour
{
    public GameObject PrefabPunches;
    public GameObject PrefabHelper;
    public int Quantity;
    public float Cooldown;
    public float Delay;
    void Start()
    {
        Cooldown += Delay;
    }

    
    void Update()
    {
        
    }

    public void Floor() {
        StartCoroutine(EFloor());
    }

    public float GetCD() {
        return Cooldown;
    }

    private IEnumerator EFloor() {
        List<float> PosListx = new List<float>();
        List<float> PosListz = new List<float>();
        float xPosition;
        float zPosition;
        for(int i=0; i < Quantity; i++) {
            xPosition = Random.Range(-19,19);
            PosListx.Add(xPosition);
            zPosition = Random.Range(-19,19);
            PosListz.Add(zPosition);
            Instantiate(PrefabHelper,new Vector3(xPosition,0,zPosition),Quaternion.identity);
        }

        yield return new WaitForSeconds(Delay);


        for(int i=0; i < Quantity; i++) {
            Instantiate(PrefabPunches,new Vector3(PosListx[i],0.7f,PosListz[i]),Quaternion.identity);
        }
    }
}
