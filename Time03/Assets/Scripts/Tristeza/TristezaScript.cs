using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TristezaScript : MonoBehaviour
{
    private SadRain srScript;
    private SadPistol spScript;
    private Skills Rain;
    private Skills Pistol;
    private List<Skills> skills;
    private BossSkillsCD SkillCD;
    public float Cooldown;
    private bool SkillIsReady = false;

    void Start()
    {
        skills = new List<Skills>();

        srScript = GetComponent<SadRain>();
        Rain = new Skills(srScript.getProb(),srScript.getCD(),false,srScript.Rain);
        skills.Add(Rain);

        spScript = GetComponent<SadPistol>();
        Pistol = new Skills(spScript.getProb(),spScript.getCD(),true,spScript.Pistol);
        skills.Add(Pistol);

        SkillCD = GetComponent<BossSkillsCD>();

        StartCoroutine(ResetCooldown());
    }

    
    void Update()
    {
        if(SkillIsReady) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }
    }

    IEnumerator ResetCooldown() {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }
}
