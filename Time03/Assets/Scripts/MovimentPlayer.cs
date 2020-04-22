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

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
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

        transform.position += Direction() * Time.deltaTime;
    }

    private void Update()
    {
        if(!dashing)
            Dash(Direction());
    }

    private Vector3 Direction()
    {
        float h = Input.GetAxis("Horizontal") *  MovimentSpeed;
        float v = Input.GetAxis("Vertical") *  MovimentSpeed;
        return new Vector3(h, 0, v);
    }
    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);
    private Vector3 RotationDirection => Vector3.RotateTowards(transform.forward, Direction(), RotationSpeed * Time.deltaTime, 0);

    private void Dash(Vector3 dir)
    {
        if (Input.GetAxis("Dash") == 1)
        {
            GeneralCounts.DashCount++;
            _rb.AddForce(dir * DashSpeed, ForceMode.Impulse);
            dashing = true;
            StartCoroutine(StopTheDash());
        }
    }

    IEnumerator StopTheDash() {
        yield return new WaitForSeconds(tempoDash);
        dashing = false;
    }
}
