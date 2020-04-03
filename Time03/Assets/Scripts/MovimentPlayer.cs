using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    public float MovimentSpeed;
    public float RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
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

    private Vector3 Direction()
    {
        float h = Input.GetAxis("Horizontal") *  MovimentSpeed;
        float v = Input.GetAxis("Vertical") *  MovimentSpeed;
        return new Vector3(h, 0, v);
    }
    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);
    private Vector3 RotationDirection => Vector3.RotateTowards(transform.forward, Direction(), RotationSpeed * Time.deltaTime, 0);
}
