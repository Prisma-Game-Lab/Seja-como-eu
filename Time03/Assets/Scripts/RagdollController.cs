using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;
    public Rigidbody MainRigidbody;
    public Rigidbody[] AllRigidbodies;
    public Camera camera;

    bool rag = false;

    public Collider BoxCollider, SphereCollider;
    private MovimentPlayer movimentPlayer;

    void Awake()
    {
        MainCollider = BoxCollider;
        MainRigidbody = GetComponent<Rigidbody>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        AllRigidbodies = GetComponentsInChildren<Rigidbody>();
        movimentPlayer = GetComponent<MovimentPlayer>();
        DoRagdoll(false);
    }

    void Update(){
        
        //Ativar ou desativar o ragdoll
        if(Input.GetKeyDown(KeyCode.LeftControl) && rag == false)
        {
            DoRagdoll(true);
            rag = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && rag == true)
        {
            DoRagdoll(false);
            rag = false;
        }

        if (movimentPlayer.dashing)
        {
            BoxCollider.enabled = false;
            SphereCollider.enabled = true;
            MainCollider = SphereCollider;
        }
        else
        {
            BoxCollider.enabled = true;
            SphereCollider.enabled = false;
            MainCollider = BoxCollider;
        }
            
    }
    
    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllColliders)
        {
            col.enabled = isRagdoll;
            MainCollider.enabled = !isRagdoll;
            GetComponent<Rigidbody>().useGravity = !isRagdoll;
            GetComponentInChildren<Animator>().enabled = !isRagdoll;
        }
        foreach (var body in AllRigidbodies)
        {
            body.isKinematic = !isRagdoll;
            MainRigidbody.isKinematic = isRagdoll;
        }

    }
}
