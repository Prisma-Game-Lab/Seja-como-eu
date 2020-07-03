using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    public Text StatsText;
    private GeneralCounts Counts;

    private bool CanActivate = false;
    private bool ControlAcess = true;

    private int TotalDashs;
    private int TotalDeaths;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
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

    private void DisplayStats() {
        TotalDashs = Counts.Stats["HubDashCount"] + Counts.Stats["CarinhoDashCount"] + Counts.Stats["TristezaDashCount"] +
        Counts.Stats["ExpressividadeDashCount"] + Counts.Stats["MDMDashCount"];
        TotalDeaths = Counts.Stats["CarinhoDeathCount"] + Counts.Stats["TristezaDeathCount"] + Counts.Stats["MDMDeathCount"] +
        Counts.Stats["ExpressividadeDeathCount"];
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
        StatsText.text = $"Número de Dashes: {TotalDashs}\n\nNúmero de Mortes: {TotalDeaths}\n\n";

        StatsText.text += $"Número de Dashes no Hub: {Counts.Stats["HubDashCount"]}\n\n";

        StatsText.text += Counts.CarinhoIsMorto?$"Tempo de Batalha do Carinho: {ConvertToTime(Counts.CarinhoCompleteTimer)}\n\n"
        :"Carinho Ainda não foi Derrotado.\n\n";

        StatsText.text += Counts.CarinhoIsMorto?$"Número de Dashes no Carinho: {Counts.Stats["CarinhoDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.CarinhoIsMorto?$"Número de Mortes no Carinho: {Counts.Stats["CarinhoDeathCount"]}\n\n"
        :"";

        StatsText.text += Counts.TristezaIsMorto?$"Tempo de Batalha da Tristeza: {ConvertToTime(Counts.TristezaCompleteTimer)}\n\n"
        :"Fraqueza Ainda não foi Derrotada.\n\n";

        StatsText.text += Counts.TristezaIsMorto?$"Número de Dashes na Tristeza: {Counts.Stats["TristezaDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.TristezaIsMorto?$"Número de Mortes na Tristeza: {Counts.Stats["TristezaDeathCount"]}\n\n"
        :"";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Tempo de Batalha da Expressividade: {ConvertToTime(Counts.ExpressividadeCompleteTimer)}\n\n"
        :"Expressividade Ainda não foi Derrotada.\n\n";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Número de Dashes na Expressividade: {Counts.Stats["ExpressividadeDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Número de Mortes na Expressividade: {Counts.Stats["ExpressividadeDeathCount"]}\n\n"
        :"";

        StatsText.text += Counts.MDMIsMorto?$"Tempo de Batalha do Mestre dos Machos: {ConvertToTime(Counts.MDMCompleteTimer)}\n\n"
        :"????????????????????????????\n\n";

        StatsText.text += Counts.MDMIsMorto?$"Número de Dashes no Mestre dos Machos: {Counts.Stats["MDMDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.MDMIsMorto?$"Número de Mortes no Mestre dos Machos: {Counts.Stats["MDMoDeathCount"]}\n\n"
        :"";

        StatsText.text += $"Tempo Total de Jogo: {ConvertToTime(Counts.TotalPlayTime)}";
    }

    private string ConvertToTime(float time) {
        float minutes = time/60;
        float seconds = time%60;
        string min = minutes.ToString("00");
        string sec = seconds.ToString("00");
        return $"{min}:{sec}";
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) {
            CanActivate = true;
        }
    }

    void OnCollisionExit(Collision other)
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
