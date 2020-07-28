using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideWave : MonoBehaviour
{
    public Animator anim = null;
    private float dMax = 13.0f;

    private void OnDisable()
    {
        anim=null;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim!=null)
        {
            float t=(transform.localPosition.z-2)/(dMax);
            t = Mathf.Clamp(t,0,1);
            anim.SetFloat("waveTime",t);
            if(t==1)
            {
                anim.SetTrigger("endWave");
                this.anim = null;
            }
        }
        
    }
}
