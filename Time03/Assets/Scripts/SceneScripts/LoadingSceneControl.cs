using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneControl : MonoBehaviour
{
    private GeneralCounts Counts;
    void Awake()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        Counts.Index = 0;
        LoadScene();
    }

    public static string CurrentScene;

    public static void LoadScene() {
        SceneManager.LoadSceneAsync(CurrentScene,LoadSceneMode.Single);
    }
}
