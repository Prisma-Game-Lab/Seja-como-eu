using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDM : MonoBehaviour
{
    public float HitPoints;
    public float Cooldown;
    public Image HpBar;
    public GameObject DamagePlatform;
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
        Rain = new Skills(100,PRScript.GetCD(),true,PRScript.Rain);
        skills.Add(Rain);

        PFScript = GetComponent<PunchFloor>();
        Floor = new Skills(100,PFScript.GetCD(),true,PFScript.Floor);
        skills.Add(Floor);

        StartCoroutine(ResetCooldown());
        StartCoroutine(ResetPlatform());
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
        if(CurrentHP % 5 == 0) {
            Platform();
            StartCoroutine(ResetPlatform());
        }
    }

    private IEnumerator ResetCooldown() {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }

    public void Platform() {
        float xPosition = Random.Range(-19,19);
        float zPosition = Random.Range(-19,19);
        if(!DamagePlatform.activeSelf) {
            DamagePlatform.transform.position = new Vector3(xPosition,0,zPosition);
            DamagePlatform.SetActive(true);
        }
        else {
            DamagePlatform.SetActive(false);
        }
    }

    private IEnumerator ResetPlatform() {
        yield return new WaitForSeconds(5);
        Platform();
    }
}
