using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillsCD : MonoBehaviour
{

    // Retorna a posicao da Skill que vai ser escolhida
    public void ChooseSkill(List<Skills> skills) {
        while(true) {
            int id = Random.Range(0,skills.Count);
            if(skills[id].IsSkillReady()) {
                float prob = Random.Range(0,100f);
                if(prob<= skills[id].GetProbabilidade()) {
                    skills[id].ActivateSkill();
                    StartCoroutine(ActiveCoolDown(skills[id]));
                    return;
                }   
            }
        }
    }

    private IEnumerator ActiveCoolDown(Skills s) {
        s.SwitchReady();
        yield return new WaitForSeconds(s.GetCoolDown());
        s.SwitchReady();
    }

}
