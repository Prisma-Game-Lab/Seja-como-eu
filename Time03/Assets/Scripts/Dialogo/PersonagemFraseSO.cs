using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogo", menuName = "ScriptableObjects/PersonagemFraseSO", order = 1)]
public class PersonagemFraseSO : ScriptableObject
{
    public List<Dialogue> Frase;
}
