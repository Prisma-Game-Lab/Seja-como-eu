using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDM : MonoBehaviour
{
    public GameObject RedBar;
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
    private DisplayFrase DF;

    private Animator anim;
    public GameObject armor1,armor2,armor3,armor4,armor5,armor6;

    void Start()
    {
        DF = GetComponent<DisplayFrase>();

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
        //skills.Add(Prison);

        PRScript = GetComponent<PunchRain>();
        Rain = new Skills(100,0,true,PRScript.Rain);

        PUScript = GetComponent<PunchUltimate>();
        Ultimate = new Skills(100,0,true,PUScript.Ultimate);

        anim = GetComponentInChildren<Animator>();

        StartCoroutine(ResetCooldown());
        Platform();
    }

    
    void Update()
    {
        HpBar.fillAmount = CurrentHP/HitPoints;
        SpecBar.fillAmount = Charger/FullSpecial;

        if(Charger >= FullSpecial) {
            PerdeVida();
        }

        if(!Counts.MDMIsMorto) {
            Counts.MDMCompleteTimer += Time.deltaTime;
        }

        if(Level >= 3) return;

        if(SkillIsReady && !UltimateNow) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }

        if(UltimateExecuteNow) {
            Ultimate.ActivateSkill();
            UltimateExecuteNow = false;
        }

        if(!PRScript.ItsRainingMan() && Level == 2) {
            Rain.ActivateSkill();
        }
    }

    private void PerdeVida() {
        if(Level < 3) {
            UltimateExecuteNow = true;
            UltimateNow = true;
        }
        CurrentHP -= 5;
        Charger = 0;
        DamagePlatforms[Level].SetActive(false);
        if(CurrentHP <= 0) {
            WinGame();
            anim.SetTrigger("death");
            armor1.SetActive(false);
            armor2.SetActive(false);
            armor3.SetActive(false);
            armor4.SetActive(false);
            armor5.SetActive(false);
            armor6.SetActive(false);
        }
    }

    public void WinGame() {
        Counts.MDMIsMorto = true;
        DF.Trigger.TriggerConversation(0,"MDMMorre");
        HpBar.gameObject.SetActive(false);
        SpecBar.gameObject.SetActive(false);
        RedBar.SetActive(false);
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
        if(Level == 3) return;
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
