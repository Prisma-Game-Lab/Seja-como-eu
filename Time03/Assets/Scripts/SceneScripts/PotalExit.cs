using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotalExit : MonoBehaviour
{
    public string SceneName;
    

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            NewSceneControl.CurrentScene = SceneName;
            SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        }
    }
}
