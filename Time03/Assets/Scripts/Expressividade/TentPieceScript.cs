using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentPieceScript : MonoBehaviour
{

    private Rigidbody _rb;
    private Transform parentTr;
    private Transform _t;
    private float size;
    // Start is called before the first frame update
    void Start()
    {
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
        if(_t == null)
            _t = GetComponent<Transform>();

        parentTr = _t.parent;
        Tentaculo.speedChange += Boost;
        size = 5.0f;
    }
    void OnDestroy()
    {
        Tentaculo.speedChange -= Boost;
    }
    public void Boost(float force)
    {
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
        if(_t == null)
            _t = GetComponent<Transform>();

        _rb.AddForce((force) * _t.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Finish"))
        {
            TentPooling.instance.Dispose(this.gameObject);
        }
    }
}
