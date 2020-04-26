using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDM : MonoBehaviour
{
    public float HitPoints;
    public float Cooldown;
    public Image HpBar;
    private Skills Rain;
    private Skills Floor;
    private PunchRain PRScript;
    private PunchFloor PFScript;
    private NewBossSkillCD SkillCD;
    private List<Skills> skills;
    private bool SkillIsReady = false;
    private float CurrentHP;

    void Start()
    {
        CurrentHP = HitPoints;

        skills = new List<Skills>();

        SkillCD = GetComponent<NewBossSkillCD>();

        PRScript = GetComponent<PunchRain>();
        Rain = new Skills(100,PRScript.GetCD(),false,PRScript.Rain);
        skills.Add(Rain);

        PFScript = GetComponent<PunchFloor>();
        Floor = new Skills(100,PFScript.GetCD(),false,PFScript.Floor);
        skills.Add(Floor);

        StartCoroutine(ResetCooldown());
    }

    
    void Update()
    {
        if(SkillIsReady) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }

        HpBar.fillAmount = CurrentHP/HitPoints;
    }

    public void PerdeVida() {
        CurrentHP--;
    }

    private IEnumerator ResetCooldown() {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }
}
