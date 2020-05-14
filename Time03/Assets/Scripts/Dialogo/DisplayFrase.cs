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

    private bool StartIt;

    void Start()
    {
        Chat = ChatBox.transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        if(Trigger.CanChat()) {
            if((Frases.CurrentFrase().Turn != MyTurn) && FraseEnd) {
                HideFrase();
                return;
            }

            if(Frases.CurrentFrase().Options.Count > 0) {
                HideFrase();
                return;
            }

            if(!ChatBox.gameObject.activeSelf && Frases.CurrentFrase().Turn == MyTurn && StartIt) {
                ShowFrase();
            }
        }

        if(Input.GetAxisRaw("PressButton") > 0 && ControlAcess) {
            if(ChatBox.gameObject.activeSelf) {
                Next();
            }
            StartCoroutine(GrantAcess());
        }
    }

    private void Next() {
        if(!FraseEnd) {
            Chat.text = Frases.CurrentFrase().Texto;
            FraseEnd = true;
        }
        else {
            StartCoroutine(ShowLetters());
        }
    }

    public void ShowFrase() {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters());
        StartIt = false;
    }

    private void HideFrase() {
        ChatBox.gameObject.SetActive(false);
        if(Frases.CurrentFrase().Turn == 0) {
            Trigger.EndConversation();
        }
        StartIt = false;
    }

    private IEnumerator ShowLetters() {
        Chat.text = "";
        FraseEnd = false;
        foreach(char c in Frases.CurrentFrase().Texto) {
            if(FraseEnd) {
                break;
            }
            Chat.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        FraseEnd = true;
        Frases.NextFrase();
    }

    private IEnumerator GrantAcess()
    {
        ControlAcess = false;
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess = true;
    }
}
