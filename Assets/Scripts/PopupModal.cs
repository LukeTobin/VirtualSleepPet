using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupModal : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] Button returnButton;
    [SerializeField] Button addButton;
    [SerializeField] GameObject parentObject;

    public static PopupModal Instance;

    void Awake(){
        Instance = this;
    }

    void Start(){
        returnButton.onClick.AddListener(() => parentObject.SetActive(false));
        addButton.onClick.AddListener(() => AddToRoutine());
        parentObject.SetActive(false);
    }

    public void Populate(Sprite spr, string _title, string desc){
        image.sprite = spr;
        title.text = _title;
        description.text = desc;

        parentObject.SetActive(true);
    }

    void AddToRoutine(){
        SaveData.current.Load();

        Block block = new Block();
        block.Title = title.text;
        block.Repeating = false; // TODO

        SaveData.current.SleepData.Routines.Add(block);
        SaveManager.Instance.Save(SaveData.current.SleepData);

        parentObject.SetActive(false);
    }
}
