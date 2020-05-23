﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    public float MovimentSpeed;
    public float RotationSpeed;
    public float DashSpeed;
    public float dashCooldown;
    public AudioSource Footsteps;

    [HideInInspector]
    public bool dashing = false;
    
    private bool dashEnabled = true;

    public float tempoDash = 1.0f;

    private Rigidbody _rb;
    private Animator anim;

    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);
    private Vector3 RotationDirection => Vector3.RotateTowards(transform.forward, Vector3.Normalize(Direction()), RotationSpeed * Time.deltaTime, 0.0f);

    //teclado
    private Vector2 kInput;

    private GeneralCounts Counts;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dashing == false)
        {
            float translationV = 0;
            float translationH = 0;

            translationV = Input.GetAxisRaw("Vertical") *  MovimentSpeed;
            translationH = Input.GetAxisRaw("Horizontal") *  MovimentSpeed;

            translationV *= Time.deltaTime;
            translationH *= Time.deltaTime;

            transform.rotation = Rotation;

            transform.position += Vector3.Normalize(Direction())* MovimentSpeed * Time.deltaTime;

    
            if((translationV != 0) || (translationH != 0)){
                anim.SetBool("Idle",false);

                Footsteps.pitch = Random.Range(0.7f, 1.3f);
                if(!Footsteps.isPlaying) {
                Footsteps.Play();
                }
            }
            else
            {
                anim.SetBool("Idle",true);
                Footsteps.Stop();
            }            
        }
    }

    private void Update()
    {
        if(!dashing && dashEnabled)
        {
            Dash(Vector3.Normalize(Direction()));
        }
    }

    private Vector3 Direction()
    {
        float h = Input.GetAxisRaw("Horizontal") ;
        float v = Input.GetAxisRaw("Vertical") ;
        return new Vector3(h, 0, v);
    }


    private void Dash(Vector3 dir)
    {
        if (Input.GetAxis("Dash") == 1)
        {
            anim.SetBool("Dash", true);
            Counts.DashCount++;
            _rb.AddForce(dir * DashSpeed, ForceMode.Impulse);
            dashing = true;
            dashEnabled = false;
            StartCoroutine(StopTheDash());
        }
    }

    IEnumerator StopTheDash() {
        yield return new WaitForSeconds(tempoDash);
        anim.SetBool("Dash", false);
        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashEnabled = true;
    }
}