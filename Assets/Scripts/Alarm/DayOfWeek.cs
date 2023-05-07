using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayOfWeek : MonoBehaviour
{
    public bool Toggled = false;
    public int Day = 0;
    [SerializeField] Color selectedColor = Color.magenta;

    Color defaultColor;
    Button m_Button;
    TMP_Text dayText;

    void Awake() {
        m_Button = GetComponent<Button>();
        dayText = GetComponentInChildren<TMP_Text>();
        defaultColor = dayText.color;

        m_Button.onClick.AddListener(() => Toggle());
    }

    void Toggle(){
        Toggled = !Toggled;

        if(Toggled) dayText.color = selectedColor;
        else dayText.color = defaultColor;
    }


}
