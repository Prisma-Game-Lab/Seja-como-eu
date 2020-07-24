using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleManager : MonoBehaviour
{
    public GameObject tentaclePrefab;
    public GameObject paintingTentaclePrefab;
    private GameObject[] allTentacles;
    public Animator[] tentAnims;
    private GameObject paintingTentacle;
    private int currentlyPaintingIndex;
    private Transform currentlyPaintingTransform;
    private Transform paintingTentacleTransform;
    private Vector3 hiddenLocation;
    private Vector3 myPosition;

    private ExpressividadeScript expressividadeScript;

    void Start()
    {
        expressividadeScript = GetComponent<ExpressividadeScript>();
        Transform _T = GetComponent<Transform>();
        myPosition = _T.position;
        hiddenLocation = new Vector3(-20,-20,-20);
        allTentacles = new GameObject[8];
        for(int i=0;i<8;i++)
        {
           GameObject tent = Instantiate(tentaclePrefab, //gameObject a ser instanciado
           myPosition,            //posicao
           Quaternion.LookRotation(new Vector3(Mathf.Sin(i*Mathf.PI*0.25f+Mathf.PI/8),0,Mathf.Cos(i*Mathf.PI*0.25f+Mathf.PI/8)),_T.up)  //rotacao
           ,_T);                 //objeto pai

            allTentacles[i]=tent;
            tent.GetComponent<Tentaculo>().setAnim(tentAnims[i]);
        }
        HitTentacle.tentacleHit += SwapTentacles; //registra a funcao ao evento

        
        currentlyPaintingIndex = (int)Random.Range(0.0f,7.9999f); //escolhe o tentaculo que vai pintar
        currentlyPaintingTransform = allTentacles[currentlyPaintingIndex].GetComponent<Transform>();
        paintingTentacle = Instantiate(paintingTentaclePrefab,currentlyPaintingTransform.position,currentlyPaintingTransform.rotation,_T); //spawna o tentaculo pintando
        paintingTentacleTransform = paintingTentacle.GetComponent<Transform>();
        tentAnims[currentlyPaintingIndex].SetTrigger("prePaint");
        currentlyPaintingTransform.position = hiddenLocation; //esconde o tentaculo original
    }
    void Update(){
        if(expressividadeScript.health==0){
            for(int i=0; i<8; i++){
                tentAnims[i].SetTrigger("deathMesmo");
                //menos esse tentAnims[currentlyPaintingIndex]
            }
        }

    }
    void OnDestroy() 
    {
        HitTentacle.tentacleHit -= SwapTentacles; //remove a funcao do evento
    }

    private void SwapTentacles()
    {
        paintingTentacleTransform.position = hiddenLocation;
        StartCoroutine(ChangeTentacles());
        tentAnims[currentlyPaintingIndex].SetTrigger("death");
    }

    private IEnumerator ChangeTentacles()
    {
        yield return new WaitForSeconds(1);
        currentlyPaintingTransform.position = myPosition;                                       //traz o tentaculo escondido de volta
        currentlyPaintingIndex = (currentlyPaintingIndex + (int)Random.Range(1.0f,6.9999f))%8;            //sorteia novo tentaculo 
        currentlyPaintingTransform = allTentacles[currentlyPaintingIndex].GetComponent<Transform>(); //pega seu Transform
        paintingTentacleTransform.rotation = (currentlyPaintingTransform.rotation);             //traz o tentaculo pintando para sua posicao e rotacao
        paintingTentacleTransform.position = myPosition;
        tentAnims[currentlyPaintingIndex].SetTrigger("prePaint");
        currentlyPaintingTransform.position = hiddenLocation;                                   //esconde o tentaculo sorteado
    }

    public void StopAllTents()
    {
        for(int i=0; i<8; i++)
        {
            allTentacles[i].SetActive(false);
        }
        paintingTentacle.SetActive(false);
    }

}
