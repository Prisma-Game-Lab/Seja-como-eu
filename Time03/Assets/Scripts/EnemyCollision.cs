using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrenght = 20.0f;
    [SerializeField]
    private float knockbackHeight = 1.0f;

    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if (rb != null && collision.collider.CompareTag("enemy"))
        {
            if (this.gameObject.GetComponent<CharControler>().dashing)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir.y = knockbackHeight;
                rb.AddForce(dir.normalized * knockbackStrenght, ForceMode.Impulse);
            }
            else
            {
                Destroy(this.gameObject);
                deathScreen.SetActive(true);
            }
        }
    }
}
