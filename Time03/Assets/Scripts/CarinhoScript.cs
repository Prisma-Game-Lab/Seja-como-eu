using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarinhoScript : MonoBehaviour
{
    public float ProbabilidadeLaunch;
    public float CoolDownLaunch;

    private Skills Launch;

    private List<Skills> skills;
    

    public Transform PlayerPosition;
    private Rigidbody _rb;
        void Start()
    {
        Launch = new Skills(ProbabilidadeLaunch,CoolDownLaunch,false);

        skills.Add(Launch);
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
