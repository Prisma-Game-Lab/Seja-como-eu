﻿using System.Collections;
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
        LoadingSceneControl.CurrentScene = SceneName;
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void NoButton() {
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
