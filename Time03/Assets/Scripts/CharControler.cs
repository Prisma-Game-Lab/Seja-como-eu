using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControler : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float pushSpeed = 1.0f;
    Vector3 forward, right;

    public string Horizontal;
    public string Vertical;
    //public string pushButton;

    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis(Horizontal);
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis(Vertical);

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement); //Rotação para qual o personagem irá mover

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;

        if(Input.GetKeyDown("space"))
        {
            _rb.AddForce(heading * pushSpeed, ForceMode.Impulse);
        }
    }
}
