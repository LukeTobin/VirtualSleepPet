using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routines : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject routine;
    [SerializeField] Transform layoutGroup;

    List<GameObject> objects = new List<GameObject>();

    public void Refresh(){
        if(objects.Count > 0){
            foreach(GameObject obj in objects){
                obj.SetActive(false);
            }
        }

        SaveData.current.Load();
        SleepData data = SaveData.current.SleepData;
        for(int i = 0;i < data.Routines.Count;i++){
            GameObject _r = Instantiate(routine);
            _r.transform.SetParent(layoutGroup, false);
            objects.Add(_r);

            Swipe swipe = _r.GetComponent<Swipe>();
            swipe.CreateBlock(data.Routines[i], true);
        }
    }
}
