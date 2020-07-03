using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampChatBox : MonoBehaviour
{
    public Image nameLabel;
    public Camera MainCamera;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 namePos = MainCamera.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;
    }
}
