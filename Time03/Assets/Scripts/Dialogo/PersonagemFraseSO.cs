using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogo", menuName = "ScriptableObjects/PersonagemFraseSO", order = 1)]
public class PersonagemFraseSO : ScriptableObject
{
    public List<Dialogue> Frase;

    private int Index = 0;

    public Dialogue CurrentFrase() {
        return Frase[Index];
    }

    public void NextFrase() {
        if(Index + 1 >= Frase.Count) {
            Index = 0;
            return;
        }
        Index++;
    }

    private void OnEnable()
    {
        Index = 0;
    }
}
