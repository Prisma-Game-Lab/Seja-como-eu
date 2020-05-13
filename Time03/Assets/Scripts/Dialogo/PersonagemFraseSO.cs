using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogo", menuName = "ScriptableObjects/PersonagemFraseSO", order = 1)]
public class PersonagemFraseSO : ScriptableObject
{
    public List<Dialogue> Frase;
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    public Dialogue CurrentFrase() {
        return Frase[Counts.Index];
    }

    public void NextFrase() {
        /*if(Counts.Index + 1 >= Frase.Count) {
            Counts.Index = 0;
            return;
        }*/
        Counts.Index++;
    }
}
