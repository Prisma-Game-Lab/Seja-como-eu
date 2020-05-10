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
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
        StatsText.text = $"Dash Count: {Counts.DashCount}\n\nDeath Count: {Counts.DeathCount}\n\n";

        StatsText.text += Counts.CarinhoIsMorto?$"Carinho Completion Time: {ConvertToTime(Counts.CarinhoCompleteTimer)}\n\n"
        :"Carinho is still alive.\n\n";

        StatsText.text += Counts.TristezaIsMorto?$"Tristeza Completion Time: {ConvertToTime(Counts.TristezaCompleteTimer)}\n\n"
        :"Tristeza is still alive.\n\n";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Expressividade Completion Time: {ConvertToTime(Counts.ExpressividadeCompleteTimer)}\n\n"
        :"Expressividade is still alive.\n\n";

        StatsText.text += Counts.MDMIsMorto?$"Mestre dos Machos Completion Time: {ConvertToTime(Counts.MDMCompleteTimer)}\n\n"
        :"Mestre dos Machos is still alive.\n\n";

        StatsText.text += $"Total Play Time: {ConvertToTime(Counts.TotalPlayTime)}";
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
