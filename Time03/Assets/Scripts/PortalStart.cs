using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalStart : MonoBehaviour
{
    public GameObject enemy;

    private NavMeshAgent agent;

    void Start() {
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            agent.enabled = true;
    }
}
