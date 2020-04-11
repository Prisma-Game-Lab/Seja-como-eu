using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    public List<Image> SelectPlayers;
    public List<Button> MenuButtons;
    public List<int> CoordenadaPlayers = new List<int>();
    private List<bool> ControlAcess = new List<bool>();

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            CoordenadaPlayers.Add(0);
            ControlAcess.Add(true);
        }
        SelectPlayers[0].gameObject.SetActive(true);
    }


    void Update()
    {
        MenuCanvas();
        ChangeSize();
    }

    private void MenuCanvas()
    {
        if (!SelectPlayers[0].transform.parent.gameObject.activeSelf)
        {
            return;
        }
        for (int i = 1; i < 4; i++)
        {
            SelectPlayers[i].gameObject.SetActive(false);
        }
        MovePlayer(MenuButtons);
        CheckCoordinateValue(MenuButtons.Count);
        PressButton(MenuButtons);
    }

    private void CheckCoordinateValue(int Count)
    {
        for (int i = 0; i < CoordenadaPlayers.Count; i++)
        {
            if (CoordenadaPlayers[i] >= Count)
            {
                CoordenadaPlayers[i] = 0;
            }
            if (CoordenadaPlayers[i] < 0)
            {
                CoordenadaPlayers[i] = Count - 1;
            }
        }
    }

    private void PressButton(List<Button> Buttons)
    {
        for (int i = 0; i < CoordenadaPlayers.Count; i++)
        {
            if (Input.GetAxisRaw("ShootPlayer" + (i + 1).ToString()) > 0 && ControlAcess[i] && SelectPlayers[i].gameObject.activeSelf && Buttons[CoordenadaPlayers[i]].gameObject.activeSelf)
            {
                Buttons[CoordenadaPlayers[i]].onClick.Invoke();
                ControlAcess[i] = false;
                StartCoroutine(GrantAcess(i));
            }
        }
    }
    private void MovePlayer(List<Button> Buttons)
    {
        for (int i = 0; i < CoordenadaPlayers.Count; i++)
        {
            SelectPlayers[i].transform.position = Buttons[CoordenadaPlayers[i]].transform.position;
        }
        for (int i = 0; i < CoordenadaPlayers.Count; i++)
        {
            if (Input.GetAxisRaw("Vertical" + (i + 1).ToString()) > 0 && ControlAcess[i])
            {
                CoordenadaPlayers[i]--;
                ControlAcess[i] = false;
                StartCoroutine(GrantAcess(i));
            }
            if (Input.GetAxisRaw("Vertical" + (i + 1).ToString()) < 0 && ControlAcess[i])
            {
                CoordenadaPlayers[i]++;
                ControlAcess[i] = false;
                StartCoroutine(GrantAcess(i));
            }
        }
    }

    private void ChangeSize()
    {
        float largura1 = 340;
        float altura1 = 80;
        float largura2 = 340;
        float altura2 = 80;
        float largura3 = 340;
        float altura3 = 80;
        if (ComparaPos(SelectPlayers[2], SelectPlayers[3]))
        {
            largura3 += 20;
            altura3 += 20;
        }
        SelectPlayers[2].rectTransform.sizeDelta = new Vector2(largura3, altura3);
        for (int i = 2; i < 4; i++)
        {
            if (ComparaPos(SelectPlayers[1], SelectPlayers[i]))
            {
                largura2 += 20;
                altura2 += 20;
            }
        }
        SelectPlayers[1].rectTransform.sizeDelta = new Vector2(largura2, altura2);
        for (int i = 1; i < 4; i++)
        {
            if (ComparaPos(SelectPlayers[0], SelectPlayers[i]))
            {
                largura1 += 20;
                altura1 += 20;
            }
        }
        SelectPlayers[0].rectTransform.sizeDelta = new Vector2(largura1, altura1);
    }
    private bool ComparaPos(Image One, Image Two)
    {
        if (One.transform.position == Two.transform.position && One.gameObject.activeSelf && Two.gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }
    IEnumerator GrantAcess(int Player)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess[Player] = true;
    }
}