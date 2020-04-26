using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchRain : MonoBehaviour
{
    public GameObject Player;
    public GameObject PrefabPunches;
    public GameObject PrefabHelper;
    public float Cooldown;
    public float Frequency;
    public float Duration;
    public float Altura;


    void Start()
    {
        Cooldown += Duration; 
    }

    public void Rain() {
        StartCoroutine(ERain());
    }

    public float GetCD() {
        return Cooldown;
    }

    private IEnumerator ERain() {
        for(int i = 0; i < Duration/Frequency; i++) {
            Debug.Log(i);
            Instantiate(PrefabHelper,new Vector3(Player.transform.position.x,0,Player.transform.position.z),Quaternion.identity);
            Instantiate(PrefabPunches, new Vector3(Player.transform.position.x, Altura, Player.transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(Frequency);
        }
    }
}
