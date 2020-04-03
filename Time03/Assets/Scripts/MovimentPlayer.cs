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
        if (Input.GetKeyDown("space"))
        {
            _rb.AddForce(dir * DashSpeed, ForceMode.Impulse);
            dashing = true;
            Invoke("StopDashing", tempoDash);
        }
    }

    /*Essa função só existe pra aumentar a janela de duração da condição de dash, 
    porque senão o knockback só funciona em distâncias muito pequenas ou com pushSpeed muito grande.
    Estou pensando em algo mais prático, mas no momento essa foi a solução que encontrei.*/
    private void StopDashing()
    {
        dashing = false;
    }
}
