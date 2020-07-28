using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchUltimate : MonoBehaviour
{
    public float NumberOfWaves;
    public float TimeBetweenWaves;
    public float TimeBetweenWavesLevel1;
    public GameObject UltimateBarrier;
    public GameObject UltimateWave;
    public Transform Player;
    private MDM Mestre;

    private Animator anim;

    void Start()
    {
        Mestre = GetComponent<MDM>();
        anim = GetComponentInChildren<Animator>();
    }


    public void Ultimate() {
        anim.SetTrigger("atq1");
        if(Mestre.GetLevel() == 0)
            StartCoroutine(Level0());
        if(Mestre.GetLevel() == 1)
            StartCoroutine(Level1());
        if(Mestre.GetLevel() == 2)
            StartCoroutine(Level2());
    }

    private IEnumerator Level0() {
        GameObject wave;
        int rnd;
        yield return new WaitForSeconds(1.5f);
        Player.position = new Vector3(0,0.5f,0);
        yield return new WaitForSeconds(0.5f);
        UltimateBarrier.SetActive(true);
        yield return new WaitForSeconds(1);
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

    private IEnumerator Level1() {
        GameObject wave;
        int rnd;
        yield return new WaitForSeconds(1.5f);
        Player.position = new Vector3(0,0.5f,0);
        yield return new WaitForSeconds(0.5f);
        UltimateBarrier.SetActive(true);
        yield return new WaitForSeconds(1);
        for(int i=0;i<3;i++) {
            wave = Instantiate(UltimateWave,new Vector3(0.63f,0,1.6f),Quaternion.identity);
            rnd = Random.Range(0,15);
            Destroy(wave.transform.GetChild(rnd).gameObject);
            yield return new WaitForSeconds(TimeBetweenWaves);
        }
        for(int i=0;i<14;i++) {
            wave = Instantiate(UltimateWave,new Vector3(0.63f,0,1.6f),Quaternion.identity);
            Destroy(wave.transform.GetChild(i).gameObject);
            Destroy(wave.transform.GetChild(i+1).gameObject);
            yield return new WaitForSeconds(TimeBetweenWavesLevel1);
        }
        yield return new WaitForSeconds(5);
        UltimateBarrier.SetActive(false);
        Mestre.RaiseLevel();
        Mestre.FinishUltimate();
    }

    private IEnumerator Level2() {
        GameObject wave;
        yield return new WaitForSeconds(1.5f);
        Player.position = new Vector3(0,0.5f,0);
        yield return new WaitForSeconds(0.5f);
        UltimateBarrier.SetActive(true);
        yield return new WaitForSeconds(1);
        for(int i=0;i<14;i++) {
            wave = Instantiate(UltimateWave,new Vector3(0.63f,0,1.6f),Quaternion.identity);
            Destroy(wave.transform.GetChild(i).gameObject);
            Destroy(wave.transform.GetChild(i+1).gameObject);
            yield return new WaitForSeconds(TimeBetweenWavesLevel1);
        }
        for(int i=14;i>0;i--) {
            wave = Instantiate(UltimateWave,new Vector3(0.63f,0,1.6f),Quaternion.identity);
            Destroy(wave.transform.GetChild(i).gameObject);
            Destroy(wave.transform.GetChild(i-1).gameObject);
            yield return new WaitForSeconds(TimeBetweenWavesLevel1);
        }
        yield return new WaitForSeconds(6);
        UltimateBarrier.SetActive(false);
        Mestre.RaiseLevel();
        Mestre.FinishUltimate();
    }
}
