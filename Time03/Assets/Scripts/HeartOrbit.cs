using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOrbit : MonoBehaviour
{

    public float orbitSpeed = 100.0f;
    public float ProbabilidadeOrbit;
    public float CooldownOrbit;
    public Skills Orbit;

    // Start is called before the first frame update
    void Start()
    {
        Orbit = new Skills(ProbabilidadeOrbit, CooldownOrbit, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OrbitAround(Transform[] hearts)
    {
        for(int i = 0; i <= hearts.Length - 1; i++)
        {
            hearts[i].RotateAround(gameObject.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
