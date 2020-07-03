using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentPieceScript : MonoBehaviour
{

    public float despawnTimer;
    private Rigidbody _rb;
    private Tentaculo parent;
    private Transform _t;
    private float lastForce;
    // Start is called before the first frame update
    void Start()
    {
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
        if(_t == null)
            _t = GetComponent<Transform>();
            
        parent = transform.parent.GetComponent<Tentaculo>();
        Tentaculo.speedChange += Boost;
        StartCoroutine(Despawn());
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

        _rb.AddForce((force-lastForce) * _t.forward);
        lastForce = force;

    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.transform.CompareTag("enemy"))
        {
            parent.spawnTentacle();
        }
    }
    private IEnumerator Despawn()
    {
        yield return new WaitUntil(()=>Vector3.Distance(_t.position,new Vector3(0,0,0) ) >= _t.lossyScale[2]*2);
        parent.spawnTentacle();
        yield return new WaitForSeconds(despawnTimer);
        Destroy(this);
    }
}
