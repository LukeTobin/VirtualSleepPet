using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake() {
        if(!Instance) Instance = this;
    }

    public void Save(SleepData data){
        SaveData.current.SleepData = data;
        SerializationManager.Save(data.ID, SaveData.current);
    }

    // Load a specific data piece
    public void Load(SleepData data){
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/data/" + data.ID + ".save");
    }
}
