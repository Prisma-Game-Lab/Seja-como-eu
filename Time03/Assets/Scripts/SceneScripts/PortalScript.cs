using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Autor: Nagib Moura Suaid*/
public class PortalScript : MonoBehaviour
{
    public GameObject ChooseUI;
    public GameObject DefeatedUI;
    public GameObject PortalCanvas;
    public bool CanEnter = true;
    public Material material;
    public Texture normalTexture, pbTexture;

    void Start()
    {
        PortalCanvas.SetActive(false);
        ChooseUI.SetActive(false);
        DefeatedUI.SetActive(false);
    }
    void Update()
    {
        if(CanEnter){
            material.SetTexture("_MainTex", normalTexture);
        }else{
            material.SetTexture("_MainTex", pbTexture);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PortalCanvas.SetActive(true);

        if(CanEnter)
        {
            ChooseUI.SetActive(true);
        }
        else
        {
            DefeatedUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void YesButton(string SceneName) {
        LoadingSceneControl.CurrentScene = SceneName;
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void NoButton(GameObject ui) {
        ui.SetActive(false);
        Time.timeScale = 1f;
    }
}
