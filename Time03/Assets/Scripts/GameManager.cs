using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject DeathScreen;
    public GameObject GameConfigUI;
    public GameObject player;
    public GameObject Canvas;

    private GeneralCounts Counts;

    private bool GameIsPaused = false;
    private bool WaitPause = false;
    public bool canPause = true;
    private string CurrentScene; 

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        Canvas.SetActive(false);
        DeathScreen.SetActive(false);
        PauseMenuUI.SetActive(false);
        GameConfigUI.SetActive(false);
        CurrentScene = SceneManager.GetActiveScene().name;
    }

    
    void Update()
    {
        Counts.TotalPlayTime += Time.deltaTime;
        if(GeneralCounts.Kill && !DeathScreen.activeSelf) {
            Death();
            canPause = false;
        }

        if(Input.GetAxisRaw("Pause") == 1 && WaitPause == false)
        {
            if(GameIsPaused)
            {
                Resume();
                StartCoroutine(KeepPaused());
            }
            else if(!GameIsPaused && canPause)
            {
                Pause();
                StartCoroutine(KeepPaused());
            }
        }
    }

    public void Death() {
        player.GetComponent<RagdollController>().DoRagdoll(true);
        player.GetComponent<MovimentPlayer>().enabled = false;
        Canvas.SetActive(true);
        DeathScreen.SetActive(true);
        DeathCounter();
    }

    public void Pause()
    {
        Canvas.SetActive(true);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Canvas.SetActive(false);
        PauseMenuUI.SetActive(false);
        GameConfigUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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

    public void SetConfig(){
        PauseMenuUI.SetActive(false);
        GameConfigUI.SetActive(true);
    }

    public void Voltar(){
        PauseMenuUI.SetActive(true);
        GameConfigUI.SetActive(false);
    }

    private void DeathCounter() {
         if(CurrentScene.Contains("Carinho")) {
            Counts.Stats["CarinhoDeathCount"]++;
            return;
        }
        if(CurrentScene.Contains("Tristeza")) {
            Counts.Stats["TristezaDeathCount"]++;
            return;
        }
        if(CurrentScene.Contains("Expressividade")) {
            Counts.Stats["ExpressividadeDeathCount"]++;
            return;
        }
        if(CurrentScene.Contains("MDM")) {
            Counts.Stats["MDMDeathCount"]++;
            return;
        }
    }
}
