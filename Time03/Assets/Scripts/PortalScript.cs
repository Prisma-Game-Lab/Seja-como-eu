using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Autor: Nagib Moura Suaid*/
public class PortalScript : MonoBehaviour
{

    public string sceneName; /*Aqui vai o nome da cena associada ao portal*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("entrou");
        SceneManager.LoadScene(sceneName); /*Quando o personagem entra, muda de cena instantaneamente (bem provisório)*/
    }
}
