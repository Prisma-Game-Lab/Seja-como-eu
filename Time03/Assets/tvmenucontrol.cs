using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvmenucontrol : MonoBehaviour
{

    public GameObject sejaTV, sejaTVchiado, sejaTVtransicao, menuUI;
    private bool temControle = true;

    void Start()
    {
        sejaTVtransicao.GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
        sejaTVchiado.GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space")&&(temControle))
        {
            StartCoroutine(mudarCanal());
            temControle = false;
        }
    }

    private IEnumerator mudarCanal() 
    {
            sejaTV.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
            sejaTV.SetActive(false);
            sejaTVtransicao.GetComponent<UnityEngine.Video.VideoPlayer>().Play();

            yield return new WaitForSeconds(1);

            sejaTVtransicao.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
            sejaTVtransicao.SetActive(false);
            sejaTVchiado.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            menuUI.SetActive(true); 
    }


}
