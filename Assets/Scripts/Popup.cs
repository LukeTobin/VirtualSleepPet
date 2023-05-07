using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text header;
    [SerializeField] TMP_Text description;
    [Space]
    [SerializeField] GameObject popupArea;
    [SerializeField] GameObject buttonArea;
    [SerializeField] PopupButton adjustTimeButton;
    
    PopupButton activeButton = null;
    
    public void CreatePopup(string _header, string _description, PopupButton displayButton, bool allowAdjust = false){
        // popup animation?
        popupArea.SetActive(true);

        header.text = _header;
        description.text = _description;

        adjustTimeButton.gameObject.SetActive(allowAdjust);
        
        if(activeButton != null) activeButton.gameObject.SetActive(false);
        activeButton = displayButton;
        if(activeButton != null) activeButton.gameObject.transform.SetParent(buttonArea.transform, false);
    }

    public void Close(){
        if(activeButton != null) activeButton.gameObject.SetActive(false);
        activeButton = null;
        popupArea.SetActive(false);
        gameObject.SetActive(false);
    }
}
