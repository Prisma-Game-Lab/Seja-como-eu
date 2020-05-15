using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenoScript : MonoBehaviour
{
	public bool lancado = false;
	private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb =  GetComponent<Rigidbody>();
    }

    public void Throw()
    {
    	StartCoroutine(trackMovement());
    }

     private IEnumerator trackMovement()
     {
     	lancado = true;
     	yield return new WaitUntil(() => (_rb.velocity == Vector3.zero)&&(_rb.angularVelocity == Vector3.zero));
     	lancado = false;
     	
     }
}
