using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceneFade : MonoBehaviour
{
    public void changeScene(){
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
