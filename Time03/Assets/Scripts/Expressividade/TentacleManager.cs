using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleManager : MonoBehaviour
{
    public GameObject tentaclePrefab;
    public GameObject paintingTentaclePrefab;
    private GameObject[] allTentacles;
    private GameObject paintingTentacle;
    private int currentlyPainting;
    private Transform currentlyPaintingTransform;
    private Transform paintingTentacleTransform;

    void Start()
    {
        Transform _T = GetComponent<Transform>();
        Vector3 position = _T.position;
        allTentacles = new GameObject[8];
        for(int i=0;i<8;i++)
        {
           GameObject tent = Instantiate(tentaclePrefab, //gameObject a ser instanciado
           position,            //posicao
           Quaternion.LookRotation(new Vector3(Mathf.Sin(i*Mathf.PI*0.25f),0,Mathf.Cos(i*Mathf.PI*0.25f)),_T.up)  //rotacao
           ,_T);                //objeto pai

            allTentacles[i]=tent;
        }
        HitTentacle.tentacleHit += SwapTentacles; //registra a funcao ao evento

        
        currentlyPainting = (int)Random.Range(0.0f,7.9999f); //escolhe o tentaculo que vai pintar
        currentlyPaintingTransform = allTentacles[currentlyPainting].GetComponent<Transform>();
        paintingTentacle = Instantiate(paintingTentaclePrefab,currentlyPaintingTransform.position,currentlyPaintingTransform.rotation,_T); //spawna o tentaculo pintando
        paintingTentacleTransform = paintingTentacle.GetComponent<Transform>();
        currentlyPaintingTransform.position = (currentlyPaintingTransform.up * -20); //esconde o tentaculo original
    }
    void OnDestroy() 
    {
        HitTentacle.tentacleHit -= SwapTentacles; //remove a funcao do evento
    }

    private void SwapTentacles()
    {
        currentlyPaintingTransform.position = (paintingTentacleTransform.position);             //traz o tentaculo escondido de volta
        currentlyPainting = (currentlyPainting + (int)Random.Range(1.0f,6.9999f))%8;            //sorteia novo tentaculo 
        currentlyPaintingTransform = allTentacles[currentlyPainting].GetComponent<Transform>(); //pega seu Transform
        paintingTentacleTransform.rotation = (currentlyPaintingTransform.rotation);             //traz o tentaculo pintando para sua posicao
        currentlyPaintingTransform.position = (currentlyPaintingTransform.up * -20);            //esconde o tentaculo sorteado
    }


}
