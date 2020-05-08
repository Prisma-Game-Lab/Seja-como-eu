using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(8,12)]
    public string Texto;

    public int Turn;
    [TextArea(2,3)]
    public List<string> Options;

    public void SetTexto(string Choice) {
        Texto = Choice;
        Turn = 1;
    }
}
