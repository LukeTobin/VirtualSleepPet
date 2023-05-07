using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Technique : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    public string Description {get;private set;}

    [SerializeField] Button button;

    void Start(){
        button.onClick.AddListener(() => ShowPopup());
    }

    public void Set(Sprite spr, string txt, string desc){
        image.sprite = spr;
        text.text = txt;
        Description = desc;
    }

    void ShowPopup(){
        PopupModal.Instance.Populate(image.sprite, text.text, Description);
    }
}
