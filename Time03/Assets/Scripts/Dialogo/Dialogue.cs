using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(8,12)]
    public string Texto;

    public int Turn;
}
