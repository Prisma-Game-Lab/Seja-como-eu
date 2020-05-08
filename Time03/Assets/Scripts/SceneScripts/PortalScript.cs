using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Autor: Nagib Moura Suaid*/
public class PortalScript : MonoBehaviour
{
    public GameObject ChooseUI;

    void Start()
    {
        ChooseUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        ChooseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void YesButton(string SceneName) {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
    }

    public void NoButton() {
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
