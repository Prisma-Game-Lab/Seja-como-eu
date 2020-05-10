using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDM : MonoBehaviour
{
    public float HitPoints;
    public float FullSpecial;
    public float Cooldown;
    public Image HpBar;
    public Image SpecBar;
    public GameObject DamagePlatform;
    private Skills Rain;
    private Skills Floor;
    private Skills Follow;
    private Skills Prison;
    private Skills Ultimate;
    private PunchRain PRScript;
    private PunchFloor PFScript;
    private PunchFollow PFOScript;
    private PunchPrison PPScript;
    private PunchUltimate PUScript;
    private NewBossSkillCD SkillCD;
    private List<Skills> skills;
    private bool SkillIsReady = false;
    private bool UltimateNow = false;
    private bool UltimateExecuteNow;
    private float CurrentHP;
    private float Charger;
    private int Level;
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;

        Level = 1;

        Charger = 0;
        
        CurrentHP = HitPoints;

        skills = new List<Skills>();

        SkillCD = GetComponent<NewBossSkillCD>();

        PRScript = GetComponent<PunchRain>();
        Rain = new Skills(100,PRScript.GetCD(),true,PRScript.Rain);
        skills.Add(Rain);

        PFScript = GetComponent<PunchFloor>();
        Floor = new Skills(100,PFScript.GetCD(),true,PFScript.Floor);
        skills.Add(Floor);

        PFOScript = GetComponent<PunchFollow>();
        Follow = new Skills(100,PFOScript.GetCD(),true,PFOScript.Follow);
        skills.Add(Follow);

        PPScript = GetComponent<PunchPrison>();
        Prison = new Skills(100,PPScript.GetCD(),true,PPScript.Prison);
        skills.Add(Prison);

        PUScript = GetComponent<PunchUltimate>();
        Ultimate = new Skills(100,0,true,PUScript.Ultimate);

        StartCoroutine(ResetCooldown());
        StartCoroutine(ResetPlatform());
    }

    
    void Update()
    {
        if(!Counts.MDMIsMorto) {
            Counts.MDMCompleteTimer += Time.deltaTime;
        }

        if(SkillIsReady && !UltimateNow) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }

        if(UltimateExecuteNow) {
            Ultimate.ActivateSkill();
            UltimateExecuteNow = false;
        }

        HpBar.fillAmount = CurrentHP/HitPoints;
        SpecBar.fillAmount = Charger/FullSpecial;

        if(CurrentHP <= 0) {
            WinGame();
        }

        if(Charger >= FullSpecial) {
            PerdeVida();
        }
    }

    private void PerdeVida() {
        CurrentHP -= 5;
        UltimateExecuteNow = true;
        UltimateNow = true;
        Charger = 0;
        
        Platform();
        StartCoroutine(ResetPlatform());
        
    }

    public void WinGame() {
        Counts.MDMIsMorto = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.AddComponent<Rigidbody>();
    }

    private IEnumerator ResetCooldown() {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }

    public void Platform() {
        float xPosition = Random.Range(-19,19);
        float zPosition = Random.Range(-18,18);
        if(!DamagePlatform.activeSelf) {
            DamagePlatform.transform.position = new Vector3(xPosition,0,zPosition);
            DamagePlatform.SetActive(true);
        }
        else {
            DamagePlatform.SetActive(false);
        }
    }

    private IEnumerator ResetPlatform() {
        yield return new WaitForSeconds(3);
        Platform();
    }

    public void RaiseLevel() {
        Level++;
    }

    public void FinishUltimate() {
        UltimateNow = false;
    }

    public void Charge() {
        Charger++;
    }
}
