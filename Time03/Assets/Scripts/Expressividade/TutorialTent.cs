using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTent : MonoBehaviour
{
    public Animator anim;
    private void OnEnable() 
    {
        HitTentacle.tentacleHit+= RemoveSelf;
    }
    void OnDestroy()
    {
        HitTentacle.tentacleHit-= RemoveSelf;
    }
    private void RemoveSelf()
    {
        anim.SetTrigger("deathMesmo");
        Destroy(this.GetComponent<Collider>());
    }

}
