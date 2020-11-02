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
    public ParticleSystem player_particle;

    private GeneralCounts Counts;
    public GeneralCounts counts {get{return Counts;}} 

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
        player_particle = player.transform.GetChild(0).Find("DeathParticle").GetComponent<ParticleSystem>();
        player_particle.Stop();
    }

    
    void Update()
    {
        Counts.TotalPlayTime += Time.deltaTime;
        if(GeneralCounts.Kill && player.GetComponent<MovimentPlayer>().enabled) {
            canPause = false;
            Death();
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

    public void ChangeInvincibility(bool value){
        GeneralCounts.PlayerImortal = value;
    }

    public void Death() {
        if(GeneralCounts.PlayerImortal) {
            GeneralCounts.Kill = false;
            canPause = true;
        } else {
            player.GetComponent<RagdollController>().DoRagdoll(true);
            player.GetComponent<MovimentPlayer>().enabled = false;
            StartCoroutine("DeathDelay");
        }
        player_particle.Play();
        DeathCounter();

        
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Canvas.SetActive(true);
        DeathScreen.SetActive(true);
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
