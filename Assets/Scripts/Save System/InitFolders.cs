using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InitFolders : MonoBehaviour
{
    [SerializeField] List<string> folderNames = new List<string>();
    [SerializeField] List<Block> routines = new List<Block>();
    public int versionTag = 6;

    [SerializeField] SaveManager manager;

    void Awake() {
        int replace = PlayerPrefs.GetInt("replace", -1);
        if(replace == versionTag) return;

        string filePath = Application.persistentDataPath + "data/SLEEP_DATA.save";

        for (int i = 0; i < folderNames.Count; i++)
        {
            if(!Directory.Exists(Application.persistentDataPath + "/" + folderNames[i] + "/")){
                Directory.CreateDirectory(Application.persistentDataPath + "/" + folderNames[i] + "/");
            }
        }
        
        if(File.Exists(filePath)){
            File.Delete(filePath);
        }

        Debug.Log("creating new file");

        PlayerPrefs.DeleteAll();

        SleepData data = new SleepData();
        data.Routines = new List<Block>(routines);
        manager.Save(data);

        PlayerPrefs.SetInt("replace", versionTag);
        PlayerPrefs.Save();
    }
}
