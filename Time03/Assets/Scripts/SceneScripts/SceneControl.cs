using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public AudioSource SceneTheme;

    private bool GameIsPaused = false;
    private bool WaitPause = false;

    void Start()
    {
        SceneTheme.Play();
    }

    public void ChangeScene(string SceneName)
    {
        LoadingSceneControl.CurrentScene = SceneName;
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        Time.timeScale = 1f;
        GeneralCounts.Kill = false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GeneralCounts.Kill = false;
    }

    public void ExitGame()
	{
		Application.Quit();
	}


}
