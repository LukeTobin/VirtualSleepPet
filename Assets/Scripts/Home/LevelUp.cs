using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    Button button;
    int rewardChapter = 0;

    [SerializeField] List<string> comicUrls = new List<string>();

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenReward);
        SaveData.current.Load();
        rewardChapter = SaveData.current.SleepData.rewardChapter;
    }

    void OpenReward(){
        LevelContainer.Instance.Reset();

        SaveData.current.SleepData.rewardChapter++;
        SaveManager.Instance.Save(SaveData.current.SleepData);

        if(comicUrls.Count <= rewardChapter) return;
        Application.OpenURL(comicUrls[rewardChapter]);
    }
}
