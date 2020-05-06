using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFrase : MonoBehaviour
{
    public PersonagemFraseSO Frases;
    public DialogueTrigger Trigger;

    public Image ChatBox;

    private Text Chat;

    private bool FraseEnd;

    static private int CurrentFrase = 0;

    void Start()
    {
        Chat = ChatBox.transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) {
            if(Frases.Frase[CurrentFrase].EndDialogue && FraseEnd) {
                HideFrase();
                Trigger.EndConversation();
                return;
            }

            if(ChatBox.gameObject.activeSelf) {
                NextFrase();
            }

            if(!ChatBox.gameObject.activeSelf) {
                ShowFrase();
                Trigger.TriggerConversation();
            }

        }
    }

    private void NextFrase() {
        if(!FraseEnd) {
            FraseEnd = true;
            Chat.text = Frases.Frase[CurrentFrase].Texto;
        }
        else {
            if(CurrentFrase + 1 == Frases.Frase.Count) {
                CurrentFrase = 0;
            }
            else {
                CurrentFrase++;
            }
            StartCoroutine(ShowLetters());
        }
    }

    private void ShowFrase() {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters());

    }

    private void HideFrase() {
        if(CurrentFrase + 1 == Frases.Frase.Count) {
            CurrentFrase = 0;
        }
        else {
            CurrentFrase++;
        }
        ChatBox.gameObject.SetActive(false);
    }

    private IEnumerator ShowLetters() {
        Chat.text = "";
        FraseEnd = false;
        foreach(char c in Frases.Frase[CurrentFrase].Texto) {
            if(FraseEnd) {
                break;
            }
            Chat.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        FraseEnd = true;
    }
}
