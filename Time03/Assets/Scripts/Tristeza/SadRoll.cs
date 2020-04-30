using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SadRoll : MonoBehaviour
{
    public float rollWindup;

    public float rollSpeed;

    public float Probabilidade;

    public float Cooldown;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Roll()
    {
        float posx = Random.Range(-19, 19);
        float posz = Random.Range(-19, 19);
        Vector3 destination = new Vector3(posx, transform.position.y, posz);
        Debug.Log(destination);

        StartCoroutine(RollDestination(destination));
    }

    private IEnumerator RollDestination(Vector3 target)
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(0.5f);

        transform.LookAt(target);

        yield return new WaitForSeconds(rollWindup);

        while(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, rollSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        agent.isStopped = false;
    }

    public float getProb()
    {
        return Probabilidade;
    }

    public float getCD()
    {
        return Cooldown;
    }
}
