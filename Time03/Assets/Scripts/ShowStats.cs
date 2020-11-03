using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    public GameObject statsObject;
    private GeneralCounts Counts;
    public StatsObject[] stats;

    private bool CanActivate = false;
    private bool ControlAcess = true;

    private int TotalDashs;
    private int TotalDeaths;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        stats = new StatsObject[5];
        for(int i=0; i<stats.Length; i++){
            stats[i].ValuesText = statsObject.transform.GetChild(i+2).GetChild(0).GetComponent<Text>();
            stats[i].LabelText = statsObject.transform.GetChild(i+2).GetChild(1).GetComponent<Text>();
            stats[i].CoverImage = statsObject.transform.GetChild(i+2).GetChild(3).GetChild(1).gameObject;
        }
    }
    
    void Update()
    {
        if(CanActivate) {
            if(Input.GetAxisRaw("PressButton") == 1 && ControlAcess) {
                if(transform.GetChild(0).gameObject.activeSelf) {
                    StartCoroutine(GrantAcess());
                    transform.GetChild(0).gameObject.SetActive(false);
                    Time.timeScale = 1f;
                    return;
                }
                StartCoroutine(GrantAcess()); 
                DisplayStats();
            }
        }
    }

    public void DisplayStats() {
        TotalDashs = /*Counts.Stats["HubDashCount"]*/ + Counts.Stats["CarinhoDashCount"] + Counts.Stats["TristezaDashCount"] +
        Counts.Stats["ExpressividadeDashCount"] + Counts.Stats["MDMDashCount"];
        TotalDeaths = Counts.Stats["CarinhoDeathCount"] + Counts.Stats["TristezaDeathCount"] + Counts.Stats["MDMDeathCount"] +
        Counts.Stats["ExpressividadeDeathCount"];
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);

        stats[0].ValuesText.text = $"{TotalDashs}\n{TotalDeaths}\n{ConvertToTime(Counts.TotalPlayTime)}";
        stats[0].LabelText.text = "Rolamentos totais\nMortes totais\nTempo de Jogo";

        stats[1].ValuesText.text = Counts.CarinhoIsMorto?$"{Counts.Stats["CarinhoDashCount"]}\n{Counts.Stats["CarinhoDeathCount"]}\n{ConvertToTime(Counts.CarinhoCompleteTimer)}":"";
        stats[1].LabelText.text = Counts.CarinhoIsMorto?"Rolamentos\nMortes\nTempo":"";
        stats[1].CoverImage.SetActive(!Counts.CarinhoIsMorto);

        stats[2].ValuesText.text = Counts.ExpressividadeIsMorto?$"{Counts.Stats["ExpressividadeDashCount"]}\n{Counts.Stats["ExpressividadeDeathCount"]}\n{ConvertToTime(Counts.ExpressividadeCompleteTimer)}":"";
        stats[2].LabelText.text = Counts.ExpressividadeIsMorto?"Rolamentos\nMortes\nTempo":"";
        stats[2].CoverImage.SetActive(!Counts.ExpressividadeIsMorto);

        stats[3].ValuesText.text = Counts.TristezaIsMorto?$"{Counts.Stats["TristezaDashCount"]}\n{Counts.Stats["TristezaDeathCount"]}\n{ConvertToTime(Counts.TristezaCompleteTimer)}":"";
        stats[3].LabelText.text = Counts.TristezaIsMorto?"Rolamentos\nMortes\nTempo":"";
        stats[3].CoverImage.SetActive(!Counts.TristezaIsMorto);

        stats[4].ValuesText.text = Counts.MDMIsMorto?$"{Counts.Stats["MDMDashCount"]}\n{Counts.Stats["MDMDeathCount"]}\n{ConvertToTime(Counts.MDMCompleteTimer)}":"";
        stats[4].LabelText.text = Counts.MDMIsMorto?"Rolamentos\nMortes\nTempo":"";
        stats[4].CoverImage.SetActive(!Counts.MDMIsMorto);
    }

    private string ConvertToTime(float time) {
        float minutes = time/60;
        float seconds = time%60;
        string min = minutes.ToString("00");
        string sec = seconds.ToString("00");
        return $"{min}:{sec}";
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            CanActivate = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            CanActivate = false;
        }
    }

    IEnumerator GrantAcess()
    {
        ControlAcess = false;
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess = true;
    }
}
