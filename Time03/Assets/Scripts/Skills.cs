using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    public Skills(float p, float cd, bool r) {
        Probabilidade = p;
        CoolDown = cd;
        isReady = r;
    }

    private float Probabilidade;

    private float CoolDown;

    private bool isReady;

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
}
