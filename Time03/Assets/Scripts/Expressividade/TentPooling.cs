using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentPooling : MonoBehaviour
{

    public GameObject regTent;
    public GameObject wavyTent;
    private List<GameObject> regTentList;
    [SerializeField] private int regsInPool = 0;
    private List<GameObject> wavyTentList;
    [SerializeField] private int wavsInPool = 0;
    public static TentPooling instance;
    public int recycles = 0;
    private void Start() 
    {
        if(instance == null)
        {
            regTentList = new List<GameObject>();
            wavyTentList = new List<GameObject>();
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnDestroy() 
    {
        instance = null;
    }
    public GameObject NewTent(Transform parent, bool wavy)
    {
        GameObject prefab;
        List<GameObject> lis;
        int qtd;
        if(wavy)
        {
            prefab = wavyTent;
            lis = wavyTentList;
            qtd=wavsInPool;
        }
        else
        {
            prefab = regTent;
            lis = regTentList;
            qtd=regsInPool;
        }

        if(qtd == 0)
        {
            return Instantiate(prefab,parent);
        }
        else
        {
            if(wavy)
            {
                wavsInPool-=1;
            }
            else
            {
                regsInPool-=1;
            }
            recycles +=1;
            GameObject t = lis[0];
            lis.RemoveAt(0);
            t.SetActive(true);
            Transform tentT= t.GetComponent<Transform>();
            tentT.parent = parent;
            tentT.position = parent.position;
            tentT.rotation = parent.rotation;
            return t;
        }
    }
    public void Dispose(GameObject tent)
    {
        if(tent.CompareTag("TentPiece"))
        {
            regTentList.Add(tent);
            regsInPool+=1;
        }
        else
        {
            wavsInPool+=1;
            wavyTentList.Add(tent);
        }
        tent.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Transform tentT = tent.GetComponent<Transform>();
        tentT.parent = this.transform; 
        tentT.position = new Vector3(16*wavsInPool,-128,16*regsInPool);
        tentT.rotation = Quaternion.identity;
        tent.SetActive(false);
    }
}
