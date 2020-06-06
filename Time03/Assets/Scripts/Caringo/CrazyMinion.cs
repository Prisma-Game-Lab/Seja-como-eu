using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyMinion : MonoBehaviour
{
    public float Cooldown;
    public float MinionDuration;
    public GameObject Minions;
    private bool Cloned;

    void Start()
    {
        if(Cloned) return;
        Cooldown += MinionDuration;
    }

    
    void Update()
    {
        
    }

    public void Minion() {

        StartCoroutine(EMinion());
    }

    private IEnumerator EMinion() {
        Minions.SetActive(true);
        yield return new WaitForSeconds(MinionDuration);
        Minions.SetActive(false);
    }

    public void IamCloned() {
        Cloned = true;
    }
}
