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

    void Start()
    {
        Chat = ChatBox.transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) {
            if((Frases.CurrentFrase().Turn != MyTurn) && FraseEnd) {
                HideFrase();
                return;
            }

            if(ChatBox.gameObject.activeSelf) {
                Next();
            }

            if(!ChatBox.gameObject.activeSelf && Frases.CurrentFrase().Turn == MyTurn) {
                ShowFrase();
                Trigger.TriggerConversation();
            }

        }
    }

    private void Next() {
        if(!FraseEnd) {
            Debug.Log("Passei");
            Chat.text = Frases.CurrentFrase().Texto;
            FraseEnd = true;
        }
        else {
            StartCoroutine(ShowLetters());
        }
    }

    private void ShowFrase() {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters());

    }

    private void HideFrase() {
        ChatBox.gameObject.SetActive(false);
        if(Frases.CurrentFrase().Turn == 0) {
            Trigger.EndConversation();
        }
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
        Debug.Log("Passei por aqui");
        Frases.NextFrase();
    }
}
