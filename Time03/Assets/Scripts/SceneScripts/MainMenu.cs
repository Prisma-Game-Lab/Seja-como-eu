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
        Counts = save.generalCounts;
        SceneTheme.Play();

        if(!SaveSystem.SucessfulLoad) {
            ContinueButton.SetActive(false);
            foreach(string s in EventKeys) {
                Counts.Events.Add(s,true);
            }
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

    public void NewGame(bool dificuldade) {
        save.NewGame(dificuldade);
        LoadingSceneControl.CurrentScene = "Hub";
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
}
