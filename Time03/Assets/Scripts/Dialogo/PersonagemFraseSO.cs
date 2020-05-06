using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frase", menuName = "ScriptableObjects/PersonagemFraseSO", order = 1)]
public class PersonagemFraseSO : ScriptableObject
{
    [TextArea(15,20)]
    public List<string> Frase;
}
