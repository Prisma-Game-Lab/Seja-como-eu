using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource SceneTheme;

    public GameObject ContinueButton;

    private SaveSystem save;

    public List<string> EventKeys;

    private GeneralCounts Counts;

    void Start()
    {
        save = SaveSystem.GetInstance();
        SceneTheme.Play();

        if(!SaveSystem.SucessfulLoad) {
            ContinueButton.SetActive(false);
        }
    }


    public void ChangeScene(string SceneName)
    {
        LoadingSceneControl.CurrentScene = SceneName;
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);

        Time.timeScale = 1f;
        GeneralCounts.Kill = false;
    }

    public void QuitGame()
	{
		Application.Quit();
	}

    public void NewGame() {
        save.NewGame();
        Counts = save.generalCounts;
        foreach(string s in EventKeys) {
            Counts.Events.Add(s,true);
        }
        LoadingSceneControl.CurrentScene = "Hub";
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
}
