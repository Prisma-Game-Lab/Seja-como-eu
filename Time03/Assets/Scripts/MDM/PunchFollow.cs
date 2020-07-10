using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PunchFollow : MonoBehaviour
{
    public GameObject PrefabPunchesFollow;
    public GameObject PrefabPunchesFollowLevel2;
    public Transform PlayerPosition;
    public float Cooldown;
    private MDM Mestre;

    private Animator anim;

    void Start()
    {
        Mestre = GetComponent<MDM>();
        anim = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        
    }

    public void Follow() {
        anim.SetTrigger("atq2");
        if(Mestre.GetLevel() == 0)
            Level0();
        if(Mestre.GetLevel() == 1)
            Level1();
        if(Mestre.GetLevel() == 2)
            Level2();
    }

    public float GetCD() {
        return Cooldown;
    }

    private void Level0() {
        GameObject one = Instantiate(PrefabPunchesFollow,new Vector3(21,1,19),Quaternion.identity);
        GameObject two = Instantiate(PrefabPunchesFollow,new Vector3(-21,1,19),Quaternion.identity);
        GameObject three = Instantiate(PrefabPunchesFollow,new Vector3(21,1,-19),Quaternion.identity);
        GameObject four = Instantiate(PrefabPunchesFollow,new Vector3(-21,1,-19),Quaternion.identity);

        one.transform.LookAt(PlayerPosition);
        two.transform.LookAt(PlayerPosition);
        three.transform.LookAt(PlayerPosition);
        four.transform.LookAt(PlayerPosition);
    }

    private void Level1() {
        GameObject one = Instantiate(PrefabPunchesFollow,new Vector3(21,1,19),Quaternion.identity);
        GameObject two = Instantiate(PrefabPunchesFollow,new Vector3(-21,1,19),Quaternion.identity);
        GameObject three = Instantiate(PrefabPunchesFollow,new Vector3(21,1,-19),Quaternion.identity);
        GameObject four = Instantiate(PrefabPunchesFollow,new Vector3(-21,1,-19),Quaternion.identity);
        GameObject five = Instantiate(PrefabPunchesFollow,new Vector3(-21,1,0),Quaternion.identity);
        GameObject six = Instantiate(PrefabPunchesFollow,new Vector3(21,1,0),Quaternion.identity);

        one.transform.LookAt(PlayerPosition);
        two.transform.LookAt(PlayerPosition);
        three.transform.LookAt(PlayerPosition);
        four.transform.LookAt(PlayerPosition);
        five.transform.LookAt(PlayerPosition);
        six.transform.LookAt(PlayerPosition);
    }

    private void Level2() {
        Instantiate(PrefabPunchesFollowLevel2,new Vector3(-21,0,0),Quaternion.identity);
        GameObject go = Instantiate(PrefabPunchesFollowLevel2,new Vector3(21,0,0),Quaternion.identity);

        go.transform.Rotate(new Vector3(0,180,0));
    }
}
