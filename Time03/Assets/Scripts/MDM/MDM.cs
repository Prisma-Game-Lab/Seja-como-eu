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
    public List<GameObject> DamagePlatforms;
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

        Level = 0;

        Charger = 0;
        
        CurrentHP = HitPoints;

        skills = new List<Skills>();

        SkillCD = GetComponent<NewBossSkillCD>();

        PFScript = GetComponent<PunchFloor>();
        Floor = new Skills(100,PFScript.GetCD(),true,PFScript.Floor);
        skills.Add(Floor);

        PFOScript = GetComponent<PunchFollow>();
        Follow = new Skills(100,PFOScript.GetCD(),true,PFOScript.Follow);
        skills.Add(Follow);

        PPScript = GetComponent<PunchPrison>();
        Prison = new Skills(100,PPScript.GetCD(),true,PPScript.Prison);
        skills.Add(Prison);

        PRScript = GetComponent<PunchRain>();
        Rain = new Skills(100,0,true,PRScript.Rain);

        PUScript = GetComponent<PunchUltimate>();
        Ultimate = new Skills(100,0,true,PUScript.Ultimate);

        StartCoroutine(ResetCooldown());
        Platform();
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

        if(!PRScript.ItsRainingMan() && Level == 2) {
            Rain.ActivateSkill();
        }
    }

    private void PerdeVida() {
        CurrentHP -= 5;
        UltimateExecuteNow = true;
        UltimateNow = true;
        Charger = 0;
        DamagePlatforms[Level].SetActive(false);
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
        DamagePlatforms[Level].SetActive(true);
    }

    public void RaiseLevel() {
        if(Level == 2) return;
        Level++;
    }

    public void FinishUltimate() {
        UltimateNow = false;
        if(CurrentHP != 1) Platform();
    }

    public void Charge() {
        Charger++;
    }

    public int GetLevel() {
        return Level;
    }

    public float GetHP() {
        return CurrentHP;
    }
}
