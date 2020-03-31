using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float pushSpeed = 5f;

    private Rigidbody _rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3 (Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {

            Vector3 ponitToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, ponitToLook, Color.blue);

            transform.LookAt(new Vector3(ponitToLook.x, transform.position.y, ponitToLook.z)); //o player irá virar para a mesma direção em que o raycast está atingindo o chão 
            
        }


    }

    void FixedUpdate()
    {
        _rb.velocity = moveVelocity;
        Dash(transform.forward);
    }

    private void Dash(Vector3 dir)
    {
       if (Input.GetMouseButtonDown(0))
        {
            _rb.AddForce(dir * pushSpeed, ForceMode.Impulse);
        }
    }

}
