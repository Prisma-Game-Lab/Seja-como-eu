using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject player;
    public GameObject Camera1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 24)
        {
            Camera1.gameObject.SetActive(false);
        }
        else
        {
            Camera1.gameObject.SetActive(true);
        }
    }
}
