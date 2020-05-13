using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    public Skills(float p, float cd, bool r, SkillFunction SkillName) {
        Probabilidade = p;
        CoolDown = cd;
        isReady = r;
        ActiveSkill = SkillName;
    }

    public delegate void SkillFunction();

    private float Probabilidade;

    private float CoolDown;

    private bool isReady;

    private SkillFunction ActiveSkill;

    public float GetProbabilidade() {
        return Probabilidade;
    }

    public float GetCoolDown() {
        return CoolDown;
    }

    public bool IsSkillReady() {
        return isReady;
    }

    public void SwitchReady() {
        if(isReady) {
            isReady = false;
        }
        else {
            isReady = true;
        }
    }

    public void setReady(bool b){
        isReady = b;
    }

    public void ActivateSkill() {
        ActiveSkill();
    }
}
