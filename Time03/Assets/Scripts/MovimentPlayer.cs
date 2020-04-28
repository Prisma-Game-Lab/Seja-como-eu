using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    public float MovimentSpeed;
    public float RotationSpeed;
    public float DashSpeed;
    public bool dashing = false;

    private float tempoDash = 1.0f;

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

        if(!dashing)
        {
            transform.rotation = Rotation;
        }
        else
        {
            transform.rotation = transform.rotation;
        }

        transform.position += Vector3.Normalize(Direction())* MovimentSpeed * Time.deltaTime;
        
        if((translationV > 0 || translationV < 0) || (translationH > 0 || translationH < 0)){
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
            Dash(Direction());
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
            _rb.maxAngularVelocity = 1000;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddRelativeTorque(Vector3.right * 5f, ForceMode.Force);
            dashing = true;
            StartCoroutine(StopTheDash());
        }
    }

    IEnumerator StopTheDash() {
        yield return new WaitForSeconds(tempoDash);
        anim.SetBool("Dash", false);
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.angularVelocity = Vector3.zero;
        _rb.maxAngularVelocity = 7;
        transform.rotation = Quaternion.identity;
        dashing = false;
        
    }
}
