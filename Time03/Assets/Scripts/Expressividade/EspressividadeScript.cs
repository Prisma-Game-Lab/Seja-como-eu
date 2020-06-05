using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressividadeScript : MonoBehaviour
{

    private int health = 3;
    private Skills Missile;
    private List<Skills> skills;
    private BossSkillsCD skillsCD;
    public float cooldown;
    private bool SkillIsReady = false;
    private GeneralCounts counts;
    private ExMissile exmScript;

    // Start is called before the first frame update
    void Start()
    {
        counts = SaveSystem.GetInstance().generalCounts;

        skills = new List<Skills>();

        skillsCD = GetComponent<BossSkillsCD>();

        exmScript = GetComponent<ExMissile>();
        Missile = new Skills(exmScript.getProb(), exmScript.getCD(), true, exmScript.Shoot);
        skills.Add(Missile);

        StartCoroutine(ResetCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if(SkillIsReady)
        {
            skillsCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }
    }

    private IEnumerator ResetCooldown()
    {
        SkillIsReady = false;
        yield return new WaitForSeconds(cooldown);
        SkillIsReady = true;
    }
}
