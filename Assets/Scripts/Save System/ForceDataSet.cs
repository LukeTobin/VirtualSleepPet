using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ForceDataSet : MonoBehaviour
{
    public SleepData data;

    void Start()
    {
        string filePath = Application.persistentDataPath + "data/SLEEP_DATA.save";
        
        if(File.Exists(filePath)){
            File.Delete(filePath);
        }

        SaveManager.Instance.Save(data);
    }
}
