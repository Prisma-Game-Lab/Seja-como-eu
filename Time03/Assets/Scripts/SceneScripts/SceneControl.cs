using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public AudioSource SceneTheme;

    private bool GameIsPaused = false;
    private bool WaitPause = false;
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        PauseMenuUI.SetActive(false);
        SceneTheme.Play();
    }

    void Update()
    {
        Counts.TotalPlayTime += Time.deltaTime;
        if(Input.GetAxisRaw("Pause") == 1 && WaitPause == false)
        {
            if(GameIsPaused)
            {
                Resume();
                StartCoroutine(KeepPaused());
            }
            else if(!GameIsPaused)
            {
                Pause();
                StartCoroutine(KeepPaused());
            }
        }


    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
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

    IEnumerator KeepPaused() {
        WaitPause = true;
        yield return new WaitForSecondsRealtime(0.5f);
        WaitPause = false;
    }

    public void SaveGame() {
        SaveSystem s = SaveSystem.GetInstance();
        s.SaveState();
    }
}
