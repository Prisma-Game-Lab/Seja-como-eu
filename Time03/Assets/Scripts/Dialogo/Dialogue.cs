using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(15,20)]
    public string Texto;

    public bool EndDialogue;
}
