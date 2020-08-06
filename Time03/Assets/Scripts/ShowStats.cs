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

    public void DisplayStats() {
        TotalDashs = Counts.Stats["HubDashCount"] + Counts.Stats["CarinhoDashCount"] + Counts.Stats["TristezaDashCount"] +
        Counts.Stats["ExpressividadeDashCount"] + Counts.Stats["MDMDashCount"];
        TotalDeaths = Counts.Stats["CarinhoDeathCount"] + Counts.Stats["TristezaDeathCount"] + Counts.Stats["MDMDeathCount"] +
        Counts.Stats["ExpressividadeDeathCount"];
        Time.timeScale = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
        StatsText.text = $"Total Roll Count . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {TotalDashs}\n\nTotal Death Count . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {TotalDeaths}\n\n";

        StatsText.text += $"Hub Roll Count . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["HubDashCount"]}\n\n";

        StatsText.text += Counts.CarinhoIsMorto?$"Affection's Battle Time . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {ConvertToTime(Counts.CarinhoCompleteTimer)}\n\n"
        :"Affection Has Not Been Defeated.\n\n";

        StatsText.text += Counts.CarinhoIsMorto?$"Affection's Battle Roll Count . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["CarinhoDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.CarinhoIsMorto?$"Death by Affection . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["CarinhoDeathCount"]}\n\n"
        :"";

        StatsText.text += Counts.TristezaIsMorto?$"Depression's Battle Time . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {ConvertToTime(Counts.TristezaCompleteTimer)}\n\n"
        :"Depression Has Not Been Defeated.\n\n";

        StatsText.text += Counts.TristezaIsMorto?$"Depression's Battle Roll Count . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["TristezaDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.TristezaIsMorto?$"Death by Depression . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["TristezaDeathCount"]}\n\n"
        :"";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Expression's Battle Time . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {ConvertToTime(Counts.ExpressividadeCompleteTimer)}\n\n"
        :"Expression Has Not Been Defeated.\n\n";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Expression's Battle Roll Count . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["ExpressividadeDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.ExpressividadeIsMorto?$"Death by Expression . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["ExpressividadeDeathCount"]}\n\n"
        :"";

        StatsText.text += Counts.MDMIsMorto?$"Master Macho's Battle Time . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {ConvertToTime(Counts.MDMCompleteTimer)}\n\n"
        :"????????????????????????????\n\n";

        StatsText.text += Counts.MDMIsMorto?$"Master Macho's Battle Roll Count . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["MDMDashCount"]}\n\n"
        :"";

        StatsText.text += Counts.MDMIsMorto?$"Death by Toxic Masculinity . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {Counts.Stats["MDMDeathCount"]}\n\n"
        :"";

        StatsText.text += $"Total Play Time . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . {ConvertToTime(Counts.TotalPlayTime)}";
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
