using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoutineBlockBuilder : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Toggle repeatToggle;
    [SerializeField] Button createButton;
    [SerializeField] Button toggleButton;
    [SerializeField] Routines routines;

    void Awake(){
        createButton.onClick.AddListener(() => Create());
    }

    void Create(){
        SaveData.current.Load();

        Block block = new Block();
        block.Title = inputField.text;
        block.Repeating = repeatToggle.isOn;

        SaveData.current.SleepData.Routines.Add(block);
        SaveManager.Instance.Save(SaveData.current.SleepData);

        routines.Refresh();

        toggleButton.onClick.Invoke();
    }

    public void Refresh(bool r){
        if(!r) return;
        inputField.text = string.Empty;
        createButton.interactable = false;
    }

    public void Validation(){
        if(inputField.text != string.Empty)
            createButton.interactable = true;
    }
}
