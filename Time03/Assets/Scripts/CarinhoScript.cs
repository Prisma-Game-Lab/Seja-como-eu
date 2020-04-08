using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarinhoScript : MonoBehaviour
{
    public Transform PlayerPosition;

    private Rigidbody _rb;

    private Skills[] skills;

    public float orbitCd;
    public float orbitProb;

    public float launchCd;
    public float launchProb;

    public float bombCd;
    public float bombProb;

    void Start()
    {
        skills = new Skills[3];
        skills[0] = new Skills(orbitProb, orbitCd, false);
        skills[1] = new Skills(launchProb, launchCd, false);
        skills[2] = new Skills(bombProb, bombCd, false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerPosition != null){
            //transform.LookAt(PlayerPosition);
            transform.rotation = Quaternion.LookRotation(PlayerPosition.position - transform.position);
        }
    }
}
