using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentPieceScript : MonoBehaviour
{

    public float despawnTimer;
    private Rigidbody _rb;
    private Tentaculo parentTe;
    private Transform parentTr;
    private Transform _t;
    private float lastForce;
    private float size;
    // Start is called before the first frame update
    void Start()
    {
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
        if(_t == null)
            _t = GetComponent<Transform>();

        parentTr = transform.parent;
        parentTe = parentTr.GetComponent<Tentaculo>();
        Tentaculo.speedChange += Boost;
        size = 5.0f;
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

    private IEnumerator Despawn()
    {
        yield return new WaitUntil(()=>Vector3.Distance(_t.position,parentTr.position ) >= size);
        parentTe.spawnTentacle();
        yield return new WaitForSeconds(despawnTimer);
        Destroy(this);
    }
}
