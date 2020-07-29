using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bloqueia o pause quando o jogador entra num
/// </summary>


public class BlockPause : MonoBehaviour
{
    public GameManager gm;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        gm.canPause=false;
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        gm.canPause=true;
    }
}
