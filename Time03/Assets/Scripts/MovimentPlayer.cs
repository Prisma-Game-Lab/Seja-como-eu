using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    public float MovimentSpeed;
    public float RotationSpeed;
    public float DashSpeed;
    public bool dashing = false;

    public float tempoDash = 1.0f;

    private Rigidbody _rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float translationV = 0;
        float translationH = 0;

        translationV = Input.GetAxis("Vertical") *  MovimentSpeed;
        translationH = Input.GetAxis("Horizontal") *  MovimentSpeed;

        translationV *= Time.deltaTime;
        translationH *= Time.deltaTime;

        transform.rotation = Rotation;

        transform.position += Vector3.Normalize(Direction())* MovimentSpeed * Time.deltaTime;
        
        if((translationV != 0) || (translationH != 0)){
            anim.SetBool("Idle",false);
        }
        else
        {
            anim.SetBool("Idle",true);
        }
    }

    private void Update()
    {
        if(!dashing)
        {
            Dash(Vector3.Normalize(transform.forward));
        }
    }

    private Vector3 Direction()
    {
        float h = Input.GetAxis("Horizontal") ;
        float v = Input.GetAxis("Vertical") ;
        return new Vector3(h, 0, v);
    }
    
    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);
    private Vector3 RotationDirection => Vector3.RotateTowards(transform.forward, Direction(), RotationSpeed * Time.deltaTime, 0);

    private void Dash(Vector3 dir)
    {
        if (Input.GetAxis("Dash") == 1)
        {
            anim.SetBool("Dash", true);
            GeneralCounts.DashCount++;
            _rb.AddForce(dir * DashSpeed, ForceMode.Impulse);
            dashing = true;
            StartCoroutine(StopTheDash());
        }
    }

    IEnumerator StopTheDash() {
        yield return new WaitForSeconds(tempoDash);
        anim.SetBool("Dash", false);
        dashing = false;
        
    }
}
