using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOption : MonoBehaviour
{
    public GameObject PlayerCursor;
    public Image OptionBox;
    public List<Text> Options;
    public PersonagemFraseSO Frases;
    private int CoordenadaPlayer;
    private bool ControlAcess;
    private int NumberOfChoices;
    private DisplayFrase NormalDialogue;
    private GeneralCounts Counts;
    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        NormalDialogue = GetComponent<DisplayFrase>();
        CoordenadaPlayer = 0;
        NumberOfChoices = 0;
        ControlAcess = true;
    }

    void Update()
    {
        Choose();
    }

    private void Choose() {
        if(Frases.Frase[Counts.Index].Options.Count == 0) {
            return;
        }
        OptionBox.gameObject.SetActive(true);
        MovePlayer(Options);
        CheckCoordinateValue(Frases.Frase[Counts.Index].Options.Count);
        GetOption();
    }

    private void GetOption()
    {
        ResetTexto();
        GiveValue();
        if (Input.GetAxisRaw("PressButton") > 0 && ControlAcess)
        {
            PassValueSO(CoordenadaPlayer);
            ControlAcess = false;
            StartCoroutine(GrantAcess());
        }
        
    }

    private void PassValueSO(int Index) {
        Counts.Index++;
        Frases.Frase[Counts.Index].SetTexto(Options[Index].text);
        OptionBox.gameObject.SetActive(false);
        NormalDialogue.ShowFrase(Frases.Frase[Counts.Index].Options[Index].NextChat);
    }

    private void ResetTexto() {
        for(int i=0;i<Options.Count;i++) {
            Options[i].text = "";
        }
    }

    private void GiveValue() {
        for(int i=0;i<Frases.Frase[Counts.Index].Options.Count;i++) {
            Options[i].text = Frases.Frase[Counts.Index].Options[i].Texto;
        }
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

    private void MovePlayer(List<Text> Textos)
    {
        
        PlayerCursor.transform.position = Textos[CoordenadaPlayer].transform.position;
        
        
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

    private IEnumerator GrantAcess()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess = true;
    }
}
