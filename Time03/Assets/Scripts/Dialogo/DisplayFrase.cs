using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFrase : MonoBehaviour
{
    public int MyTurn;
    public PersonagemFraseSO Frases;
    public DialogueTrigger Trigger;

    public Image ChatBox;

    private Text Chat;

    private bool FraseEnd;

    private bool ControlAcess = true;

    private bool StartIt = true;
    private bool EndChat = false;
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        Chat = ChatBox.transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        if(Trigger.CanChat() && Input.GetAxisRaw("PressButton") > 0 && ControlAcess) {
            if(FraseEnd && Frases.Frase[Counts.Index].Turn != MyTurn) {
                HideFrase();
                return;
            }

            if(Frases.Frase[Counts.Index].Options.Count > 0) {
                HideFrase();
                return;
            }

            if(ChatBox.gameObject.activeSelf) {
                Next();
            }

            if(!ChatBox.gameObject.activeSelf && Frases.Frase[Counts.Index].Turn == MyTurn && StartIt) {
                ShowFrase();
            }

            StartCoroutine(GrantAcess());
        }
    }

    private void Next() {
        if(FraseEnd && Frases.Frase[Counts.Index].Turn != MyTurn ) {
            EndChat = true;
            return;
        }

        if(!FraseEnd) {
            Chat.text = Frases.Frase[Counts.Index].Texto;
            FraseEnd = true;
        }

        else {
            Counts.Index++;
            if(Frases.Frase[Counts.Index].Turn != MyTurn) {
                HideFrase();
                return;
            } 
            StartCoroutine(ShowLetters());
        }
    }

    private void ShowFrase() {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters());
        StartIt = false;
    }

    public void ShowFrase(int NewIndex) {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters(NewIndex));
        StartIt = false;
    }

    private void HideFrase() {
        ChatBox.gameObject.SetActive(false);
        if(Frases.Frase[Counts.Index].Turn == 0) {
            Trigger.EndConversation();
        }
        StartIt = true;
    }

    private IEnumerator ShowLetters() {
        Chat.text = "";
        FraseEnd = false;
        foreach(char c in Frases.Frase[Counts.Index].Texto) {
            if(FraseEnd) {
                break;
            }
            Chat.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        FraseEnd = true;
        //Counts.Index++;
    }

    private IEnumerator ShowLetters(int Index) {
        Chat.text = "";
        FraseEnd = false;
        foreach(char c in Frases.Frase[Counts.Index].Texto) {
            if(FraseEnd) {
                break;
            }
            Chat.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        FraseEnd = true;
        //Counts.Index = Index;
    }

    private IEnumerator GrantAcess()
    {
        ControlAcess = false;
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess = true;
    }

    public bool GetAcess() {
        return ControlAcess;
    }
}
