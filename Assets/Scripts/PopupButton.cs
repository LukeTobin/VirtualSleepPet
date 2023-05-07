using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupButton : MonoBehaviour
{
    public Button Btn {get;private set;}
    public TMP_Text Text {get;private set;}

    void Awake()
    {
        Btn = GetComponent<Button>();
        Text = Btn.GetComponentInChildren<TMP_Text>();
    }
}