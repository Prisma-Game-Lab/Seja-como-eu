using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilidadeUI : MonoBehaviour
{
    private GameManager gameManager;
    private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        toggle = GetComponent<Toggle>();
        toggle.isOn = GeneralCounts.PlayerImortal;
    }

    public void ChangeInv(){
        toggle.isOn = !GeneralCounts.PlayerImortal;
        gameManager.ChangeInvincibility(toggle.isOn);
    }

    void OnSubmit (){
        ChangeInv();
    }
}
