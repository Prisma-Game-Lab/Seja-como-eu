using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillsCD : MonoBehaviour
{
    // Retorna a posicao da Skill que vai ser escolhida
    public void ChooseSkill(List<Skills> skills) {

        float probTotal = 0;
        List<Skills> readySkills = new List<Skills>();
        List<float> skillProbs = new List<float>();
        foreach(Skills s in skills)
        {

            if(s.IsSkillReady() && !(s.IsOnCoolDown()))
            {
                probTotal+=s.GetProbabilidade();
                skillProbs.Add(probTotal);
                readySkills.Add(s);
            }
        }

        if(readySkills.Count == 0)
            return;

        float sorteio = Random.Range(0,probTotal);

        for(int i=0; i<readySkills.Count; i++)
        {
            if(sorteio<=skillProbs[i])
            {
                readySkills[i].ActivateSkill();
                StartCoroutine(ActiveCoolDown(readySkills[i]));
                return;
            }
        }
    }

    private IEnumerator ActiveCoolDown(Skills s) {
        s.SwitchCoolDown();
        yield return new WaitForSeconds(s.GetCoolDown());
        s.SwitchCoolDown();
    }

}
