using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    public Image SelectPlayer;
    public List<Button> MenuButtons;
    private int CoordenadaPlayer;
    private bool ControlAcess;
    
    private SaveSystem save;
    void Start()
    {
        // Isso eh soh pro Main Menu ////// Obs: se vc nao tiver um file com save ele vai bugar com certeza
        save = SaveSystem.GetInstance();
        if(!SaveSystem.SucessfulLoad) {
            Debug.Log("trolei!!!!!!!!!!!!!!!");
            MenuButtons.RemoveAt(0);
        }
        ////////////////////////////////////
        CoordenadaPlayer = 0;
        ControlAcess = true;
        SelectPlayer.gameObject.SetActive(true);
    }


    void Update()
    {
        MenuCanvas();
    }

    private void MenuCanvas()
    {
        if (!SelectPlayer.transform.parent.gameObject.activeSelf)
        {
            return;
        }
        MovePlayer(MenuButtons);
        CheckCoordinateValue(MenuButtons.Count);
        PressButton(MenuButtons);
    }

    private void CheckCoordinateValue(int Count)
    {
        
        if (CoordenadaPlayer >= Count)
        {
            CoordenadaPlayer = 0;
        }
        if (CoordenadaPlayer < 0)
        {
            CoordenadaPlayer = Count - 1;
        }
        
    }

    private void PressButton(List<Button> Buttons)
    {
        
        if (Input.GetAxisRaw("PressButton") > 0 && ControlAcess && Buttons[CoordenadaPlayer].transform.parent.parent.gameObject.activeSelf)
        {
            Buttons[CoordenadaPlayer].onClick.Invoke();
            ControlAcess = false;
            StartCoroutine(GrantAcess());
        }
        
    }
    private void MovePlayer(List<Button> Buttons)
    {
        
        SelectPlayer.transform.position = Buttons[CoordenadaPlayer].transform.position;
        
        
        if (Input.GetAxisRaw("Vertical") > 0 && ControlAcess)
        {
            CoordenadaPlayer--;
            ControlAcess = false;
            StartCoroutine(GrantAcess());
        }
        if (Input.GetAxisRaw("Vertical") < 0 && ControlAcess)
        {
            CoordenadaPlayer++;
            ControlAcess = false;
            StartCoroutine(GrantAcess());
        }
        
    }

    IEnumerator GrantAcess()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess = true;
    }
}