using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchUltimate : MonoBehaviour
{
    public float NumberOfWaves;
    public float TimeBetweenWaves;
    public GameObject UltimateBarrier;
    public GameObject UltimateWave;
    public Transform Player;
    private MDM Mestre;

    void Start()
    {
        Mestre = GetComponent<MDM>();
    }

    
    void Update()
    {
        
    }

    public void Ultimate() {
        StartCoroutine(EUltimate());
    }

    private IEnumerator EUltimate() {
        GameObject wave;
        int rnd;
        yield return new WaitForSeconds(5);
        Player.position = new Vector3(0,0.5f,0);
        yield return new WaitForSeconds(0.5f);
        UltimateBarrier.SetActive(true);
        yield return new WaitForSeconds(3);
        for(int i=0;i<NumberOfWaves;i++) {
            wave = Instantiate(UltimateWave,new Vector3(0.63f,0,1.6f),Quaternion.identity);
            rnd = Random.Range(0,15);
            Destroy(wave.transform.GetChild(rnd).gameObject);
            yield return new WaitForSeconds(TimeBetweenWaves);
        }
        yield return new WaitForSeconds(5);
        UltimateBarrier.SetActive(false);
        Mestre.RaiseLevel();
        Mestre.FinishUltimate();
    }
}
