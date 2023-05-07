using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Settings : MonoBehaviour
{
    [SerializeField] Button settingsButton;
    [SerializeField] Button resetAllButton;
    [SerializeField] Button resetTargetTime;
    [SerializeField] Button visitWebsite;

    bool open = false;

    void Start(){
        settingsButton.onClick.AddListener(() => OpenMenu());
        resetAllButton.onClick.AddListener(() => ResetAll());
        resetTargetTime.onClick.AddListener(() => ResetTargetTime());
        visitWebsite.onClick.AddListener(() => VisitWebsite());
        gameObject.SetActive(false);
    }

    public void OpenMenu(){
        open = !open;
        gameObject.SetActive(open);
        if(open) SideMenu.Instance.CloseMenu();
    }

    void ResetAll() {
        PlayerPrefs.DeleteAll();
        string filePath = Application.persistentDataPath + "data/SLEEP_DATA.save";
        if(File.Exists(filePath)){
            File.Delete(filePath);
        }
        SleepData data = new SleepData();
        SaveManager.Instance.Save(data);
        OpenMenu();
    }

    void ResetTargetTime(){
        GetTime.Instance.ShowTimePicker();
        OpenMenu();
    }

    void VisitWebsite(){
        Application.OpenURL("https://jenniferkalkhoven.com/");
        OpenMenu();
    }
}
