﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressividadeScript : MonoBehaviour
{

    private int health = 9;
    private Skills Missile;
    private Skills Pillar;
    private List<Skills> skills;
    private BossSkillsCD skillsCD;
    public float cooldown;
    private bool skillIsReady = false;
    private GeneralCounts counts;
    private ExMissile exmScript;
    private ExPillar expScript;

    // Start is called before the first frame update
    void Start()
    {
        counts = SaveSystem.GetInstance().generalCounts;

        skills = new List<Skills>();

        skillsCD = GetComponent<BossSkillsCD>();

        exmScript = GetComponent<ExMissile>();
        Missile = new Skills(exmScript.getProb(), exmScript.getCD(), true, exmScript.Shoot);
        skills.Add(Missile);

        expScript = GetComponent<ExPillar>();
        Pillar = new Skills(expScript.getProb(), expScript.getCD(), true, expScript.InkPillar);
        skills.Add(Pillar);

        StartCoroutine(ResetCooldown());


        HitTentacle.tentacleHit += TakeDamage; //registra a funcao TakeDamage ao evento tentacleHit
    }
    void OnDestroy()
    {
        HitTentacle.tentacleHit -= TakeDamage; //remove a funcao TakeDamage do evento tentacleHit
    }

    // Update is called once per frame
    void Update()
    {
        if(skillIsReady)
        {
            skillsCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }
    }

    private IEnumerator ResetCooldown()
    {
        skillIsReady = false;
        yield return new WaitForSeconds(cooldown);
        skillIsReady = true;
    }

    private void TakeDamage()
    {
        health -=1;
        if (health == 0)
        {
            counts.ExpressividadeIsMorto = true;
            Destroy(this.gameObject);
        }
    }
}
