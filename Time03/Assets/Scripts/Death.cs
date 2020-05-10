using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !GeneralCounts.Kill) {
            GeneralCounts.Kill = true;
        }
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player") && !GeneralCounts.Kill) {
            GeneralCounts.Kill = true;
        }
    }
}
