using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyClone : MonoBehaviour
{
    public float Cooldown;
    public float CloneDuration;
    private Vector3 Pos;
    private bool Cloned = false;

    void Start()
    {
        Pos = transform.position;
        Cooldown += CloneDuration;
    }

    
    void Update()
    {
        
    }

    public void Clone() {
        if(Cloned) return;
        StartCoroutine(EClone());
    }

    private IEnumerator EClone() {
        GameObject CaringoClone = Instantiate(gameObject,new Vector3(-Pos.x,Pos.y,-Pos.z),Quaternion.identity);
        CaringoClone.GetComponent<CrazyClone>().IAmCloned();
        CaringoClone.GetComponent<CrazyMinion>().IamCloned();
        yield return new WaitForSeconds(CloneDuration);
        Destroy(CaringoClone);
    }

    public void IAmCloned() {
        Cloned = true;
    }
}
